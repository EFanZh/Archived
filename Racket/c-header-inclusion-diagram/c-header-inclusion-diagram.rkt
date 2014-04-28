#lang racket

(define relation (make-hash))

(define (analyze folder)
  (define (analyze-file file)
    (with-input-from-file file
      (lambda ()
        (let loop ([line (read-line)])
          (unless (eof-object? line)
            (let ([match-result (regexp-match #px"^\\s*#include\\s*(<[^<>]+>|\"[^\"]+\")\\s*$" line)])
              (if (match-result)
                  ))
            (loop))))
      #:mode 'text))
  (for/list ([p (directory-list folder)])
    (let ([full-path (build-path folder p)])
      (if (file-exists full-path)
          (analyze-file )))))
