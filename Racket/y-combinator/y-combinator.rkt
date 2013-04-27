; Y combinator.
(define (y f)
  ((lambda (u)
     (u u))
   (lambda (x)
     (f (x x)))))

; Z combinator is used for call-by-value language.
(define (z f)
  ((lambda (u)
     (u u))
   (lambda (x)
     (f (lambda (v)
          ((x x) v))))))

(define fact
  (z
   (lambda (f)
     (lambda (n)
       (if (= n 0) 1 (* n (f (- n 1))))))))

(fact 10)
