#lang racket

(define (insert s i str)
  (append (take s i)
          str
          (drop s i)))

(define (delete s i n)
  (append (take s i)
          (drop s (+ i n))))

(define (modify s i n str)
  (append (take s i)
          str
          (drop s (+ i n))))

(define (move s i n t)
  (insert (delete s i n)
          t
          (take (drop s i) n)))
