(define (sum term a next b)
  (define (sum-iter a)
    (if (> a b)
        0
        (+ (term a)
           (sum-iter (next a)))))

  (sum-iter a))

; Sum of the integers from a through b.
(define (sum-integers a b)
  (define (term x)
    x)

  (define (next x)
    (+ x 1))

  (sum term a next b))

; Sum of the cubes of the integers from a through b.
(define (sum-cubes a b)
  (define (term x)
    (* x x x))

  (define (next x)
    (+ x 1))

  (sum term a next b))

; Sum of a sequence of terms in the series
;     1 / (1 * 3) + 1 / (5 * 7) + 1 / (9 * 11) + ...
; which converges to (pi / 8).
(define (pi-sum a b)
  (define (term x)
    (/ 1 (* x (+ x 2))))

  (define (next x)
    (+ x 4))

  (sum term a next b))

; Calculate the integral of f from a to b:
;     (f(a + dx / 2) + f(a + dx / 2) + f(a + 2 * dx + dx / 2) + ...)) * dx
(define (integral f a b dx)
  (define (next x)
    (+ x dx))

  (* (sum f
          (+ a (/ dx 2))
          next
          b)
     dx))

; Tests.
(sum-integers 1 100)
(sum-cubes 1 100)
(pi-sum 1 100)
(integral (lambda (x)
            (* x x x))
          0
          1
          0.0001)
