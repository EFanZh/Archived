(define (square n)
  (* n n))

; Search for divisors.
(define (prime?-1 n)
  (define (smallest-divisor)
    (define (find-iter i)
      (cond ((> (square i) n) n)
            ((= (remainder n i) 0) i)
            (else (find-iter (+ i 1)))))

    (find-iter 2))
  
  (= (smallest-divisor) n))

; The Fermat test.
(define (prime?-2 n times)
  (define (test)
    (define (try-it a)
      (define (expmod exp m)
        (cond ((= exp 0) 1)
              ((even? exp) (remainder (square (expmod (/ exp 2) m)) m))
              (else (remainder (* a (expmod (- exp 1) m)) m))))

      (= (expmod n n) a))

    (try-it (+ 1 (random (- n 1)))))

  (define (prime-iter i)
    (cond ((= i 0) true)
          ((test) (prime-iter (- i 1)))
          (else false)))

  (prime-iter times))

(prime?-1 33331)
(prime?-1 33333)
(prime?-2 33331 3)
(prime?-2 33333 3)
