(define (flatmap proc seq)
  (apply append (map proc seq)))

(define (permutations s)
  (if (null? s)
      '(())
      (flatmap (lambda (x)
                 (map (lambda (p)
                        (cons x p))
                      (permutations (remove x s))))
               s)))

(permutations '(1 2 3))
