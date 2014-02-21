#lang racket

(define (get-contiunation-name params)
  (let ([base 'k])
    (if (memq base params)
        (let loop [(n 1)]
          (let [(k (string->symbol (string-append (symbol->string base)
                                                  (number->string n))))]
            (if (memq k params)
                (loop (add1 n))
                k)))
        base)))

(define (cps exp ctx)
  (cond [(not (pair? exp)) (ctx exp)]
        [(eq? (car exp) 'lambda) (let* [(params (cadr exp))
                                        (k (get-contiunation-name params))]
                                   (ctx `(lambda ,(append params (list k))
                                           ,(cps (caddr exp)
                                                 (lambda (x)
                                                   (list k x))))))]
        [(eq? (car exp) 'if)] (cps (cadr exp)
                                   (lambda (r)
                                     `(lambda (r)
                                        (if r
                                            ,(cps (caddr exp) ctx)
                                            ,(cps (cadddr exp) ctx)))))))

(cps '(lambda (x k)
        x)
     (lambda (x)
       x))
