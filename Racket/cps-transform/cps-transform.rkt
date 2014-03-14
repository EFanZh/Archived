#lang racket

(define (get-contiunation-name params)
  (let ([base 'k])
    (if (memq base params)
        (let loop ([n 1])
          (let ([k (string->symbol (string-append (symbol->string base)
                                                  (number->string n)))])
            (if (memq k params)
                (loop (add1 n))
                k)))
        base)))



(define (cps exp ctx)
  (let* ([simple-ctx (list identity)]
         [add-simple-ctx (lambda (ctx)
                           (set! simple-ctx (cons ctx simple-ctx)))])
    (match exp
      [x #:when (not (pair? x)) (ctx x)]
      [`(lambda ,formals ,body) (let ([k (get-contiunation-name formals)])
                                  (ctx `(lambda ,(append formals (list k))
                                          ,(cps body
                                                (let ([ctx (lambda (x)
                                                             (list k x))])
                                                  (add-simple-ctx (lambda (x)
                                                                    (list k x)))
                                                  ctx)))))]
      [`(if ,test ,consequent ,alternate) (cps test (if (memq ctx simple-ctx)
                                                        (lambda (r)
                                                          `(lambda (r)
                                                             (if r
                                                                 ,(cps (caddr exp) ctx)
                                                                 ,(cps (cadddr exp) ctx))))))]
      [x ]))
  
(cps '(lambda (x k)
        x)
     identity)
