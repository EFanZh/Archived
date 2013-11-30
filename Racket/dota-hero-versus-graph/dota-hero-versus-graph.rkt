#lang racket

(require net/url)
(require racket/date)
(require (planet neil/html-parsing:2:0))

; Generate options
(define *update-data* #f)
(define *generate-graph* #f)

(define *generate-levels* '(1 2 3 4 5))
(define *gv-graph-attributes* '((layout neato)
                                (overlap false)
                                (splines true)
                                (fontname "Calibri")))
(define *gv-additional-attributes* '((node ((fontname "Calibri")
                                            (fontsize 9)
                                            (fixedsize true)
                                            (width 1.4)
                                            (height 1.4)))))
(define *gv-set-sep* #t)
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
(define *http-user-agent* "User-Agent: Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.48 Safari/537.36")
(define *data-file* "data.txt")

; Set date format.
(date-display-format 'iso-8601)

; Fetch & save data from the Internet.
(define (fetch-hero-versus-data)
  ; Fetch data from url.
  (define (get-html-data url)
    (let* ([header-host (string-append "Host: " *src-host*)]
           [header (list header-host *http-user-agent*)]
           [port (get-pure-port (string->url url) header)]
           [result (html->xexp port)])
      (close-input-port port)
      result))
  (define make-hero-id
    (let ([n -1])
      (lambda ()
        (set! n (add1 n))
        (string->symbol (format "h~a" n)))))
  (let* ([hero-name-id-dict (make-hash)]
         [hero-list (let ([hero-list-data (drop ((compose sixth third third third third get-html-data) *src-heroes-url-str*) 2)])
                      (map (lambda (e)
                             (let ([id (make-hero-id)]
                                   [name (third (fourth (third e)))]
                                   [path (substring (second (second (second e)))
                                                    (string-length *src-heroes-rel-path*))])
                               (hash-set! hero-name-id-dict name id)
                               (list id name path)))
                           hero-list-data))]
         [fetch-time (current-seconds)]
         [hero-versus-data (list fetch-time
                                 (map (lambda (h)
                                        (printf "Fetching hero data (~a)...\n" (second h))
                                        (let* ([data (get-html-data (string-append *src-heroes-url-str*
                                                                                   (third h)
                                                                                   "/_versus"))]
                                               [versus-data (map (lambda (v)
                                                                   (let ([percent-str->number (compose string->number
                                                                                                       (lambda (s)
                                                                                                         (substring s 0 (- (string-length s) 1))))]
                                                                         [comma-str->number (compose string->number
                                                                                                     (lambda (s)
                                                                                                       (string-replace s "," "")))])
                                                                     (map (lambda (r)
                                                                            (map (lambda (c p)
                                                                                   (p (second c)))
                                                                                 (drop r 2)
                                                                                 (list (lambda (n)
                                                                                         (hash-ref hero-name-id-dict (last n)))
                                                                                       percent-str->number
                                                                                       percent-str->number
                                                                                       comma-str->number)))
                                                                          (drop ((compose third second third) v) 1))))
                                                                 (drop data 1))])
                                          (append (list (first h)
                                                        (second h)
                                                        (third h))
                                                  versus-data)))
                                      hero-list))]
         [port (open-output-file *data-file*
                                 #:mode 'text
                                 #:exists 'replace)])
    (write hero-versus-data port)
    (close-output-port port)
    hero-versus-data))

; Get data from local file.
(define (read-local-hero-versus-data)
  (let* ([port (open-input-file *data-file*
                                #:mode 'text)]
         [data (read port)])
    (close-input-port port)
    data))

; Generate hero versus data.
(define *hero-versus-data* (if *update-data*
                               (fetch-hero-versus-data)
                               (read-local-hero-versus-data)))

; Utilities.
(define (get-get-hero-indegree get-data)
  (lambda (id level)
    (apply + (map (lambda (hero)
                    (count (lambda (v)
                             (eq? (first v) id))
                           (take (get-data hero) level)))
                  (second *hero-versus-data*)))))
(define *get-hero-indegrees* (list (get-get-hero-indegree fourth)
                                   (get-get-hero-indegree fifth)))
(define *hero-id-name-dict*
  (make-hash (map (lambda (hero)
                    (cons (first hero)
                          (second hero)))
                  (second *hero-versus-data*))))

; Generate graph file.
(define (generate-graph-file)
  (for-each (lambda (level)
              (let* ([types '("best" "worst")]
                     [ports (map (lambda (t)
                                   (open-output-file (format "~a (Level ~a).gv" (string-titlecase t) level)
                                                     #:mode 'text
                                                     #:exists 'replace))
                                 types)]
                     [sep (format "+~a" (+ *gv-sep-start* (* *gv-sep-interval* (sub1 level))))])
                ; Header.
                (for-each (lambda (type port)
                            (fprintf port "digraph ~a_level_~a\n{\n" type level)
                            ; Graph attributes.
                            (let ([other-graph-attributes (append (list (list 'label (format "DotA Hero ~a Versus Statistics (Level ~a) [Update Time: ~a]"
                                                                                             (string-titlecase type)
                                                                                             level
                                                                                             (string-replace (date->string (seconds->date (first *hero-versus-data*)) #t) "T" " "))))
                                                                  (if *gv-set-sep*
                                                                      (list (list 'sep sep))
                                                                      (void)))])
                              (for-each (lambda (a)
                                          (fprintf port "    ~s = ~s;\n" (first a) (second a)))
                                        (append *gv-graph-attributes* other-graph-attributes)))
                            ; Additional attributes.
                            (for-each (lambda (a)
                                        (if (not (null? (second a)))
                                            (fprintf port
                                                     "    ~s [~a];\n"
                                                     (first a)
                                                     (string-join (map (lambda (attr)
                                                                         (format "~s = ~s"
                                                                                 (first attr)
                                                                                 (second attr)))
                                                                       (second a))
                                                                  ", "))
                                            (void)))
                                      *gv-additional-attributes*))
                          types
                          ports)
                ; Ralations.
                (for-each (lambda (hero)
                            (for-each (lambda (data port get-hero-indegree)
                                        (fprintf port
                                                 "    ~a [href = ~s, label = ~s];\n"
                                                 (first hero)
                                                 (string-append *src-heroes-url-str* (third hero))
                                                 (format "~a [~a]"
                                                         (second hero)
                                                         (get-hero-indegree (first hero) level)))
                                        (for-each (lambda (h c r)
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
                                                                     (fourth h))))
                                                  (take data level)
                                                  (take *gv-edge-colors* level)
                                                  (range 1 (+ level 1))))
                                      (drop hero 3)
                                      ports
                                      *get-hero-indegrees*))
                          (second *hero-versus-data*))
                ; Footer
                (for-each (lambda (port)
                            (displayln "}" port))
                          ports)
                (for-each close-output-port ports)))
            *generate-levels*))

(if (or *update-data* *generate-graph*)
    (generate-graph-file)
    (void))

; Some tests.

; Sort heroes and return result with key.
(define (sort-hero less-than? extract-key [heroes (second *hero-versus-data*)])
  (let ([dict (make-hash)])
    (define (get-key h)
      (if (hash-has-key? dict h)
          (hash-ref dict h)
          (let ([key (extract-key h)])
            (hash-set! dict h key)
            key)))
    (map (lambda (h)
           (list (second h)
                 (hash-ref dict h)))
         (sort heroes
               less-than?
               #:key get-key))))

; Best worst advantage.
(define (best-worst-advantage)
  (sort-hero > (compose second first fifth)))

; Worst best advantage.
(define (worst-best-advantage)
  (sort-hero < (compose second first fourth)))

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
