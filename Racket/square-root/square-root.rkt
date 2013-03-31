; because "sqrt" is abuilt-in precedure, use "my-sqrt" instead
(define (my-sqrt x accuracy)
  (define (square x)
    (* x x))
  
  (define (average x y)
    (/ (+ x y) 2))
  
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
(my-sqrt 2 0.000001)
(my-sqrt 2.0 0.000001)

; the golden ratio
(/ (- (my-sqrt 5 0.000001) 1) 2)
(/ (- (my-sqrt 5.0 0.000001) 1) 2)
