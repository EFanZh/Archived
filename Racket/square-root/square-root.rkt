(define (square x)
  (* x x))

(define (average x y)
  (/ (+ x y) 2))

(define (square-root x accuracy)  
  (define (good-enough? guess)
    (< (abs (- (square guess) x)) accuracy))

  (define (improve guess)
    (average guess (/ x guess)))

  (define (sqrt-iter guess)
    (if (good-enough? guess)
        guess
        (sqrt-iter (improve guess))))

  (sqrt-iter 1))

; sqrt(2)
(square-root 2 0.000001)
(square-root 2.0 0.000001)

; the golden ratio
(/ (- (square-root 5 0.000001) 1) 2)
(/ (- (square-root 5.0 0.000001) 1) 2)
