#lang racket

(require rackunit)
(require "exercise-1.21.rkt")

(check-equal? (product '(a b c) '(x y)) (reverse '((a x) (a y) (b x) (b y) (c x) (c y))))
