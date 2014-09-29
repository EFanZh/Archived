#lang racket

(define *data* '("root" (("Guide Switch" (("Guide Line")
                                          ("Bounding Indicator")))
                         ("Tool Camera" (("Tool Switch" (("Rotate Tool")
                                                         ("Translate Tool")))))
                         ("Component Instances"))))

(define (get-name node)
  (first node))

(define (get-children node)
  (if (< (length node) 2)
      '()
      (second node)))

(define (generate node)
  (let ([children (get-children node)])
    (if (null? children)
        (list (get-name node)
              (list (list 0
                          (+ (string-length (get-name node)) 4)) ; row begin and end.
              '()) ; 
        (map generate children))))
