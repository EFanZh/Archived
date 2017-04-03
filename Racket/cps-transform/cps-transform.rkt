#lang racket

(require racket/generator)
(require rackunit)

;; Original version: https://github.com/yinwang0/lightsabers/blob/master/cps.ss.

(define (cps expression)
  (define (tail-context x)
    `(k ,x))
  (define make-formal (generator ()
                        (let loop ([i 0])
                          (yield (string->symbol (format "v~a" i)))
                          (loop (add1 i)))))
  (let helper ([expression expression]
               [context identity])
    (match expression
      [(? (disjoin number? symbol?) value) (context value)]
      [`(λ (,(? symbol? formal)) ,body) (context `(λ (,formal k)
                                                    ,(helper body tail-context)))]
      [`(if ,test ,consequent ,alternate) (helper test (λ (x)
                                                         `(if ,x
                                                              ,(helper consequent context)
                                                              ,(helper alternate context))))]
      [`(,operator ,operand) (helper operator (λ (x)
                                                (helper operand (λ (y)
                                                                  (if (eq? context tail-context)
                                                                      `(,x ,y k)
                                                                      `(,x ,y ,(let ([v (make-formal)])
                                                                                 `(λ (,v)
                                                                                    ,(context v)))))))))])))

(define (check-cps expression result)
  (check-equal? (cps expression) result))

(check-cps 'x 'x)

(check-cps '(λ (x)
              x)
           '(λ (x k)
              (k x)))

(check-cps '(λ (x)
              (x 1))
           '(λ (x k)
              (x 1 k)))

(check-cps '(if (f x)
                a
                b)
           '(f x (λ (v0)
                   (if v0 a b))))

(check-cps '(if x
                (f a)
                b)
           '(if x
                (f a (λ (v0) v0))
                b))

(check-cps '(λ (x)
              (if (f x)
                  a
                  b))
           '(λ (x k)
              (f x (λ (v0)
                     (if v0
                         (k a)
                         (k b))))))

(check-cps '(λ (x)
              (if (if x
                      (f a)
                      b)
                  c
                  d))
           '(λ (x k)
              (if x
                  (f a (λ (v0)
                         (if v0
                             (k c)
                             (k d))))
                  (if b
                      (k c)
                      (k d)))))

(check-cps '(λ (x)
              (if t
                  (if x
                      (f a)
                      b)
                  c))
           '(λ (x k)
              (if t
                  (if x
                      (f a k)
                      (k b))
                  (k c))))

(check-cps '(λ (x)
              (if (if t
                      (if x
                          (f a)
                          b)
                      c)
                  e
                  w))
           '(λ (x k)
              (if t
                  (if x
                      (f a (λ (v0)
                             (if v0
                                 (k e)
                                 (k w))))
                      (if b
                          (k e)
                          (k w)))
                  (if c
                      (k e)
                      (k w)))))

(check-cps '(λ (x)
              (h (if x
                     (f a)
                     b)))
           '(λ (x k)
              (if x
                  (f a (λ (v0)
                         (h v0 k)))
                  (h b k))))

(check-cps '(λ (x)
              ((if x
                   (f g)
                   h) c))
           '(λ (x k)
              (if x
                  (f g (λ (v0)
                         (v0 c k)))
                  (h c k))))

(check-cps '(((f a) (g b)) ((f c) (g d)))
           '(f a (λ (v0)
                   (g b (λ (v1)
                          (v0 v1 (λ (v2)
                                   (f c (λ (v3)
                                          (g d (λ (v4)
                                                 (v3 v4 (λ (v5)
                                                          (v2 v5 (λ (v6)
                                                                   v6)))))))))))))))
