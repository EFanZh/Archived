; tree recursion
(define (fibonacci-1 n)
  (cond ((= n 0) 0)
        ((= n 1) 1)
        (else (+ (fibonacci-1 (- n 1))
                 (fibonacci-1 (- n 2))))))

; iterative process
(define (fibonacci-2 n)
  (define (fib-iter a b count)
    (if (= count 0)
        b
        (fib-iter (+ a b) a (- count 1))))

  (fib-iter 1 0 n))

; This is EXTREMELY slow, uncomment this to experience. It'll need O(n!) time to compute
; (fibonacci-1 32)

; This is fast.
(fibonacci-2 32)
