#lang racket

(require net/url)
(require racket/date)
(require (planet neil/html-parsing:2:0))

; Generate options
(define *update-data* #t)
(define *generate-graph* #f)

(define *generate-levels* '(1 2 3 4 5))
(define *gv-graph-attributes* '((layout "sfdp")
                                (overlap "scalexy")
                                (splines true)
                                (fontname "Calibri")))
(define *gv-additional-attributes* '((node ((fontname "Calibri")
                                            (fontsize 9)))))
(define *gv-set-sep* #f)
(define *gv-sep-start* 16)
(define *gv-sep-end* 64)
(define *gv-sep-interval* (/ (- 64 16) 4))
(define *gv-edge-colors* '("#fb9293"
                           "#c1d08b"
                           "#1dde8e"
                           "#79a6be"
                           "#a385f4"))

; Data source consts.
(define *src-host* "dotabuff.com")
(define *src-heroes-rel-path* "/heroes/")
(define *src-heroes-url-str* (string-append "http://"
                                            *src-host*
                                            *src-heroes-rel-path*))

; Some other consts.
(define *http-user-agent* "User-Agent: Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2243.0 Safari/537.36")
(define *data-file* "data.txt")

; Set date format.
(date-display-format 'iso-8601)

; Fetch & save data from the Internet.
(define (fetch-hero-versus-data)
  ; Fetch data from url.
  (define (get-html-data url)
    (call/input-url (string->url url)
                    get-pure-port
                    html->xexp
                    (list (format "Host: ~a" *src-host*)
                          *http-user-agent*)))
  ; Generate unique hero id.
  (define make-hero-id
    (let ([n -1])
      (lambda ()
        (set! n (add1 n))
        (string->symbol (format "h~a" n)))))
  ; Fetch data.
  (let* ([hero-name-id-dict (make-hash)]
         [hero-list (for/list ([e (drop ((compose sixth third third third third) (get-html-data *src-heroes-url-str*)) 2)])
                      (let ([id (make-hero-id)]
                            [name (third (fourth (third e)))]
                            [path (substring (second (second (second e)))
                                             (string-length *src-heroes-rel-path*))])
                        (hash-set! hero-name-id-dict name id)
                        (list id name path)))]
         [processors (let ([percent-str->number (lambda (s)
                                                  (string->number (substring s 0 (sub1 (string-length s)))))])
                       (list (lambda (n)
                               (hash-ref hero-name-id-dict (last n)))
                             percent-str->number
                             percent-str->number
                             (lambda (s)
                               (string->number (string-replace s "," "")))))]
         [versus-data (for/list ([h hero-list])
                        (printf "Fetching hero data (~a)...\n" (second h))
                        (append (take h 3)
                                (for/list ([v (drop (get-html-data (string-append *src-heroes-url-str* (third h) "/_versus")) 1)])
                                  (for/list ([r (drop ((compose third second third) v) 1)])
                                    (for/list ([c (drop r 2)]
                                               [p processors])
                                      (p (second c)))))))]
         [fetch-time (current-seconds)]
         [hero-versus-data (list fetch-time versus-data)])
    ; Save data.
    (with-output-to-file *data-file*
      (lambda ()
        (write hero-versus-data))
      #:mode 'text
      #:exists 'replace)
    hero-versus-data))

; Get data from local file.
(define (read-local-hero-versus-data)
  (call-with-input-file *data-file*
    read
    #:mode 'text))

; Generate hero versus data.
(define *hero-versus-data* (if *update-data*
                               (fetch-hero-versus-data)
                               (read-local-hero-versus-data)))

; Utilities.
(define (get-get-hero-indegree get-data)
  (lambda (id level)
    (apply + (for/list ([hero (second *hero-versus-data*)])
               (count (lambda (v)
                        (eq? (first v) id))
                      (take (get-data hero) level))))))
(define *get-hero-indegrees* (list (get-get-hero-indegree fourth)
                                   (get-get-hero-indegree fifth)))
(define *hero-id-name-dict*
  (make-hasheq (for/list ([hero (second *hero-versus-data*)])
                 (cons (first hero)
                       (second hero)))))

; Generate graph file.
(define (generate-graph-file)
  (displayln "Generating graph files...")
  (for ([level *generate-levels*])
    (let* ([types '("best" "worst")]
           [ports (for/list ([t types])
                    (open-output-file (format "~a (Level ~a).gv" (string-titlecase t) level)
                                      #:mode 'text
                                      #:exists 'replace))]
           [sep (format "+~a" (+ *gv-sep-start* (* *gv-sep-interval* (sub1 level))))])
      ; Header.
      (for ([type types]
            [port ports])
        (fprintf port "digraph ~a_level_~a\n{\n" type level)
        ; Graph attributes.
        (let ([other-graph-attributes (append (list (list 'label (format "DotA Hero ~a Versus Statistics (Level ~a) [Update Time: ~a]"
                                                                         (string-titlecase type)
                                                                         level
                                                                         (string-replace (date->string (seconds->date (first *hero-versus-data*)) #t) "T" " "))))
                                              (if *gv-set-sep*
                                                  (list (list 'sep sep))
                                                  '()))])
          (for ([a (append *gv-graph-attributes* other-graph-attributes)])
            (fprintf port "    ~s = ~s;\n" (first a) (second a)))
          ; Additional attributes.
          (for ([a *gv-additional-attributes*])
            (unless (null? (second a))
              (fprintf port
                       "    ~s [~a];\n"
                       (first a)
                       (string-join (for/list ([attr (second a)])
                                      (format "~s = ~s"
                                              (first attr)
                                              (second attr)))
                                    ", "))))))
      ; Ralations.
      (for ([hero (second *hero-versus-data*)])
        (for ([data (drop hero 3)]
              [port ports]
              [get-hero-indegree *get-hero-indegrees*])
          (fprintf port
                   "    ~a [href = ~s, label = ~s];\n"
                   (first hero)
                   (string-append *src-heroes-url-str* (third hero))
                   (format "~a [~a]"
                           (second hero)
                           (get-hero-indegree (first hero) level)))
          (for ([h (take data level)]
                [c (take *gv-edge-colors* level)]
                [r (range 1 (add1 level))])
            (fprintf port
                     "    ~a -> ~a [color = ~s, tooltip = ~s];\n"
                     (first hero)
                     (first h)
                     c
                     (format "~a → ~a [~a]&#10;Advantage = ~a%&#10;Win Rate = ~a%&#10;Matches = ~a"
                             (hash-ref *hero-id-name-dict* (first hero))
                             (hash-ref *hero-id-name-dict* (first h))
                             r
                             (second h)
                             (third h)
                             (fourth h))))))
      ; Footer
      (for ([port ports])
        (displayln "}" port))
      (for-each close-output-port ports)))
  (displayln "Done."))

(when (or *update-data* *generate-graph*)
  (generate-graph-file))

; Some tests.

; Sort heroes and return result with key.
(define (sort-hero less-than? extract-key [heroes (second *hero-versus-data*)])
  (let ([dict (make-hasheq)])
    (define (get-key h)
      (hash-ref! dict h (thunk (extract-key h))))
    (for/list ([h (sort heroes
                        less-than?
                        #:key get-key)])
      (list (second h)
            (hash-ref dict h)))))

; Best worst advantage.
(define (best-worst-advantage)
  (sort-hero > (compose second first fifth)))

; Worst best advantage.
(define (worst-best-advantage)
  (sort-hero < (compose second first fourth)))

; Best total advantage.
(define (best-total-advantage)
  (sort-hero > (lambda (h)
                 (apply +
                        (map second
                             (append (fourth h)
                                     (fifth h)))))))

; Most fear.
(define (most-fear)
  (let ([get-worst-indegree (second *get-hero-indegrees*)])
    (sort-hero > (lambda (h)
                   (get-worst-indegree (first h) 5)))))

; Best worst win rate.
(define (best-worst-win-rate)
  (sort-hero > (lambda (h)
                 (apply min
                        (map third
                             (append (fourth h)
                                     (fifth h)))))))

; Worst best win rate.
(define (worst-best-win-rate)
  (sort-hero < (lambda (h)
                 (apply max
                        (map third
                             (append (fourth h)
                                     (fifth h)))))))

; Best total win rate.
(define (best-total-win-rate)
  (sort-hero > (lambda (h)
                 (apply +
                        (map third
                             (append (fourth h)
                                     (fifth h)))))))

; Tough ones.
(define (tough-ones)
  (let ([get-best-indegree (first *get-hero-indegrees*)]
        [get-worst-indegree (second *get-hero-indegrees*)])
    (sort-hero >
               (lambda (h)
                 (get-worst-indegree (first h) 5))
               (filter (lambda (h)
                         (zero? (get-best-indegree (first h) 5)))
                       (second *hero-versus-data*)))))

; Predators.
(define (predators)
  (let ([level 1]
        [versus-data (second *hero-versus-data*)])
    (apply append
           (for/list ([h versus-data])
             (for/list ([v (filter-map (lambda (v)
                                         (let ([v-data (assq (first v)
                                                             versus-data)])
                                           (if (assq (first h)
                                                     (take (fifth v-data) level))
                                               (second v-data)
                                               #f)))
                                       (take (fourth h) level))])
               (list (second h) v))))))
