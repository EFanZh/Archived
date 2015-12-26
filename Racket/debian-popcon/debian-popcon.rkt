#! /usr/bin/racket

#lang typed/racket

(define-type Contest-Result (List Symbol String))
(define-type Filter (List Symbol String))

(define *contest-result-sources* '([by-install "source/by_inst"]
                                   [by-vote "source/by_vote"]))

(define *filters* '([tag-editing "?tag(use::editing)"]
                    [tag-editor "?tag(devel::editor)"]
                    [tag-ide "?tag(devel::ide)"]
                    [tag-mathematics "?tag(field::mathematics)"]
                    [tag-rcs "?tag(devel::rcs)"]
                    [tag-virtualization "?tag(admin::virtualization)"]
                    [section-database "?section(database)"]
                    [section-lisp "?section(lisp)"]
                    [section-math "?section(math)"]
                    [yaml "yaml"]
                    [theme "theme"]
                    [desktop "desktop"]))

(define (build-get-package-command-line [filter : String])
  (format "aptitude search -F '%p' '~a'" filter))

(struct Package-Item ([rank : Number]
                      [name : String]
                      [install : Number]
                      [vote : Number]
                      [old : Number]
                      [recent : Number]
                      [no-files : Number]
                      [maintainer : String]))

(struct Package-Column ([header : String]
                        [align : (U 'left 'right)]
                        [get-value : (-> Package-Item String)]))

(define *columns* (list (Package-Column "Rank" 'right (compose number->string Package-Item-rank))
                        (Package-Column "Name" 'left Package-Item-name)
                        (Package-Column "Install" 'right (compose number->string Package-Item-install))
                        (Package-Column "Vote" 'right (compose number->string Package-Item-vote))
                        (Package-Column "Old" 'right (compose number->string Package-Item-old))
                        (Package-Column "Recent" 'right (compose number->string Package-Item-recent))
                        (Package-Column "maintainer" 'left Package-Item-maintainer)))

(define *column-widths* ((inst make-vector Exact-Nonnegative-Integer) (length *columns*) 0))

(define (read-word [input : Input-Port])
  (define last (let loop : (U Char EOF) ([current (read-char input)])
                 (cond [(eof-object? current) current]
                       [(char-whitespace? current) (loop (read-char input))]
                       [else current])))
  (let loop : String ([current last])
    (if (or (eof-object? current) (char-whitespace? current))
        ""
        (string-append (string current) (loop (read-char input))))))

(define (update-column-widths [i : Integer]
                              [value : Integer])
  (vector-set! *column-widths*
               i
               (max (vector-ref *column-widths* i) value)))

(define (update-package-item-column-widths [package-item : Package-Item])
  (for ([i (range 0 (length *columns*))])
    (let ([get-value (Package-Column-get-value (list-ref *columns* i))])
      (update-column-widths i (string-length (get-value package-item))))))

(define (parse-contest-result [line : String])
  (let* ([input (open-input-string line)]
         [package-item (Package-Item (cast (read input) Number)
                                     (read-word input)
                                     (cast (read input) Number)
                                     (cast (read input) Number)
                                     (cast (read input) Number)
                                     (cast (read input) Number)
                                     (cast (read input) Number)
                                     (string-trim (cast (read-line input) String)))])    
    (update-package-item-column-widths package-item)
    package-item))

(define (write-package-item [package-item : Package-Item])
  (for ([i (range 0 (sub1 (length *columns*)))])
    (let* ([column (list-ref *columns* i)]
           [align (Package-Column-align column)]
           [get-value (Package-Column-get-value column)]
           [width (vector-ref *column-widths* i)])
      (display (~a (get-value package-item)
                   #:width width
                   #:align align))
      (display "  ")))
  (displayln ((Package-Column-get-value (last *columns*)) package-item)))

(define *contest-results*
  (for/list : (Listof (List Symbol (HashTable String Package-Item))) ([contest-result-source *contest-result-sources*])
    (let ([sort-id (first contest-result-source)]
          [result-file (second contest-result-source)])
      (list sort-id
            (with-input-from-file result-file #:mode 'text
              (λ ()
                (let loop : (HashTable String Package-Item) ([line (read-line)])
                  (cond [(eof-object? line) (make-immutable-hash)]
                        [(memq (string-ref line 0) '(#\# #\-)) (loop (read-line))]
                        [else (let ([package-item (parse-contest-result line)])
                                (hash-set (loop (read-line))
                                          (Package-Item-name package-item)
                                          package-item))]))))))))

(define *filtered-results*
  (for/list : (Listof (List Symbol (Listof String))) ([filter : Filter *filters*])
    (let* ([filter-id (first filter)]
           [filter-expression (second filter)]
           [command-line (build-get-package-command-line filter-expression)]
           [process (process command-line)]
           [input (first process)]
           [packages (let loop : (Listof String) ([line (read-line input)])
                       (if (eof-object? line)
                           '()
                           (cons (string-trim line)
                                 (loop (read-line input)))))])
      (list filter-id packages))))

(define (get-result [packages : (HashTable String Package-Item)]
                    [package-filter : (Listof String)])
  (let ([my-sort (inst sort String Number)]
        [filtered-package-names (filter (λ (package-name)
                                          (hash-has-key? packages package-name)) package-filter)]
        [extract-key (λ ([package-name : String])
                       (Package-Item-rank (hash-ref packages package-name)))])
    (for/list : (Listof Package-Item) ([package-name (my-sort
                                                      filtered-package-names
                                                      <
                                                      #:key extract-key)])
      (hash-ref packages package-name))))

(vector-set! *column-widths* 1 64)

(make-directory* "result/packages")

(for ([filter : (List Symbol (Listof String)) *filtered-results*])
  (let* ([filter-id (first filter)]
         [filter-packages (second filter)]
         [file-name (format "result/packages/~a.txt" filter-id)])
    ; Write filtered result.
    (with-output-to-file file-name #:mode 'text #:exists 'replace
      (λ ()
        (for ([package filter-packages])
          (displayln package))))
    (for ([contest-result : (List Symbol (HashTable String Package-Item)) *contest-results*])
      (let* ([contest-id (first contest-result)]
             [contest-results (second contest-result)]
             [file-name (format "result/~a-~a.txt" contest-id filter-id)]
             [sorted-filtered-result (get-result contest-results filter-packages)])
        (with-output-to-file file-name #:mode 'text #:exists 'replace
          (λ ()
            (for ([contest-result sorted-filtered-result])
              (write-package-item contest-result))))))))
