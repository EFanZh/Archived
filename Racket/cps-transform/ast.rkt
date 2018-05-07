#lang typed/racket

(require "./input.rkt")

;; Data structures.

(struct Var ([var : Symbol])
  #:transparent)

(struct Fun ([vars : (Listof Symbol)]
             [body : Exp])
  #:transparent)

(struct App ([rator : Exp]
             [rands : (Listof Exp)])
  #:transparent)

(define-type Exp (U Var Fun App))

;; Parsers.

(define (parse [exp : RawExp]) : Exp
  (match exp
    [(? raw-var? x) (Var x)]
    [(? raw-fun? x) (Fun (second x) (parse (third x)))]
    [(? raw-app? x) (App (parse (first x)) (map parse (rest x)))]))

(define (unparse [exp : Exp]) : RawExp
  (match exp
    [(Var x) x]
    [(Fun vars body) `(λ ,vars ,(unparse body))]
    [(App rator rands) `(,(unparse rator) ,@(map unparse rands))]))

(provide Var Fun App Exp parse unparse)
