#lang eopl

;; Exercise 2.18 [★] We usually represent a sequence of values as a list. In this representation, it is easy to move
;; from one element in a sequence to the next, but it is hard to move from one element to the preceding one without the
;; help of context arguments. Implement non-empty bidirectional sequences of integers, as suggested by the grammar
;;
;; NodeInSequence ::= (Int Listof(Int) Listof(Int))
;;
;; The first list of numbers is the elements of the sequence preceding the current one, in reverse order, and the second
;; list is the elements of the sequence after the current one. For example, (6 (5 4 3 2 1) (7 8 9)) represents the list
;; (1 2 3 4 5 6 7 8 9), with the focus on the element 6.
;;
;; In this representation, implement the procedure number->sequence, which takes a number and produces a sequence
;; consisting of exactly that number. Also implement current-element, move-to-left, move-to-right, insert-to-left,
;; insert-to-right, at-left-end?, and at-right-end?.
;;
;; For example:
;;
;;     > (number->sequence 7)
;;     (7 () ())
;;     > (current-element '(6 (5 4 3 2 1) (7 8 9)))
;;     6
;;     > (move-to-left '(6 (5 4 3 2 1) (7 8 9)))
;;     (5 (4 3 2 1) (6 7 8 9))
;;     > (move-to-right '(6 (5 4 3 2 1) (7 8 9)))
;;     (7 (6 5 4 3 2 1) (8 9))
;;     > (insert-to-left 13 '(6 (5 4 3 2 1) (7 8 9)))
;;     (6 (13 5 4 3 2 1) (7 8 9))
;;     > (insert-to-right 13 '(6 (5 4 3 2 1) (7 8 9)))
;;     (6 (5 4 3 2 1) (13 7 8 9))
;;
;; The procedure move-to-right should fail if its argument is at the right end of the sequence, and the procedure
;; move-to-left should fail if its argument is at the left end of the sequence.

(define number->sequence
  (lambda (num)
    (list num '() '())))

(define current-element car)

(define move-to-left
  (lambda (node)
    (let ([before (cadr node)])
      (if (null? before)
          (eopl:error 'move-to-left "Cannot move to left when at left end.")
          (let ([num (car node)]
                [after (caddr node)])
            (list (car before) (cdr before) (cons num after)))))))

(define move-to-right
  (lambda (node)
    (let ([after (caddr node)])
      (if (null? after)
          (eopl:error 'move-to-right "Cannot move to right when at right end.")
          (let ([num (car node)]
                [before (cadr node)])
            (list (car after) (cons num before) (cdr after)))))))

(define insert-to-left
  (lambda (num node)
    (let ([current (car node)]
          [before (cadr node)]
          [after (caddr node)])
      (list current (cons num before) after))))

(define insert-to-right
  (lambda (num node)
    (let ([current (car node)]
          [before (cadr node)]
          [after (caddr node)])
      (list current before (cons num after)))))

(define at-left-end?
  (lambda (node)
    (null? (cadr node))))

(define at-right-end?
  (lambda (node)
    (null? (caddr node))))

(provide number->sequence
         current-element
         move-to-left
         move-to-right
         insert-to-left
         insert-to-right
         at-left-end?
         at-right-end?)
