; recursive process
(define (factorial-1 n)
  (if (= n 0)
      1
      (* n (factorial-1 (- n 1)))))

; iterative process
(define (factorial-2 n)
  (define (fact-iter product counter)
    (if (> counter n)
        product
        (fact-iter (* counter product) (+ counter 1))))

  (fact-iter 1 1))

(factorial-1 32)
(factorial-2 32)
