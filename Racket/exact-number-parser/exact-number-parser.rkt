#lang typed/racket

(define-type Digit (U 0 1 2 3 4 5 6 7 8 9))
(define-type DigitCharacter (U #\0 #\1 #\2 #\3 #\4 #\5 #\6 #\7 #\8 #\9))

(define-predicate digit? Digit)
(define-predicate char-digit? DigitCharacter)

(define (char->digit [c : DigitCharacter])
  (assert (- (char->integer c)
             (char->integer #\0)) digit?))

(define (read-digit [input : Input-Port])
  (let ([char (peek-char input)])
    (if (char-digit? char)
        (begin (read-char input)
               (char->digit char))
        #f)))

(define (read-nonnegative-integer-part [input : Input-Port])
  (let ([first-digit (read-digit input)])
    (if first-digit
        (let loop : Nonnegative-Integer ([result : Nonnegative-Integer first-digit])
          (let ([digit (read-digit input)])
            (if digit
                (loop (+ (* result 10) digit))
                result)))
        #f)))

(define (read-fractional-part [input : Input-Port])
  (let ([first-digit (read-digit input)])
    (if first-digit
        (let loop : Nonnegative-Exact-Rational ([base : Positive-Integer 100]
                                                [result : Nonnegative-Exact-Rational (/ first-digit 10)])
          (let ([digit (read-digit input)])
            (if digit
                (loop (* base 10)
                      (+ result (/ digit base)))
                result)))
        #f)))
