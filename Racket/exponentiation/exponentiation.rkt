(define (square n)
  (* n n))

(define (exponentiation a n)
  (define (exp-recursive n)
    (cond ((= n 0) 1)
          ((even? n) (square (exp-recursive (/ n 2))))
          (else (* a (exp-recursive (- n 1))))))

  (exp-recursive n))

(exponentiation 4 11)
