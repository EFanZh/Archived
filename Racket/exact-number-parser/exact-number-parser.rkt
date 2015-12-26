#lang typed/racket

(define-type DigitCharacter (U #\0 #\1 #\2 #\3 #\4 #\5 #\6 #\7 #\8 #\9))

(define (char->digit [c : Char])
  (match c
    [#\0 0]
    [#\1 1]
    [#\2 2]
    [#\3 3]
    [#\4 4]
    [#\5 5]
    [#\6 6]
    [#\7 7]
    [#\8 8]
    [#\9 9]))

(: char-digit? (-> Char Boolean))
(define (char-digit? [c : Char])
   (if (memv c (list #\0 #\1 #\2 #\3 #\4 #\5 #\6 #\7 #\8 #\9))
       #t
       #f))

(define (read-fractional-part [input : Input-Port])
  (let loop : (U Exact-Rational #f) ([result #f]
                                     [base 10]
                                     [c (peek-char input)])
    (cond [(eof-object? c) result]
          [(char-digit? c) (loop (+ result (/ (char->digit c) base))
                                 (* base 10)
                                 (peek-char input))]
          [else base])))

(define (parse-nonnegative-integer [input : Input-Port])
  (let loop : (U Exact-Nonnegative-Integer #f) ([base #f]
                                                [c (peek-char input)])
    (cond [(eof-object? c) base]
          [(char-digit? c) (loop (+ (* base 10) (char->digit c))
                                 (peek-char input))]
          [else base])))

(define (parse-nonnegative-exact-number [input : Input-Port])
  (let ([integer-part (parse-nonnegative-integer input)])
    (if integer-part
        (if (eqv? (peek-char input) #\.)
            (begin (read-char input)
                   (let ([fractional-part (read-fractional-part input)])
                     (if fractional-part
                         (+ integer-part fractional-part)
                         #f)))
            integer-part)
        #f)))

(define (parse-exact-rational [input : Input-Port])
  (let loop ([c (peek-char input)])
    (cond [(eof-object? c) #f]
          [(= c #\-) (begin (read-char input)
                            (let ([num (parse-nonnegative-exact-number input)])
                              (if num
                                  (- num)
                                  #f)))]
          [(char-digit? c) (parse-nonnegative-exact-number input)]
          [else #f])))
