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
  (cond [(raw-var? exp) (Var exp)]
        [(raw-fun? exp) (Fun (second exp) (parse (third exp)))]
        [(raw-app? exp) (App (parse (first exp)) (map parse (rest exp)))]))

(define (unparse [exp : Exp]) : RawExp
  (match exp
    [(Var x) x]
    [(Fun vars body) `(λ ,vars ,(unparse body))]
    [(App rator rands) `(,(unparse rator) ,@(map unparse rands))]))

(provide Var Fun App Exp parse unparse)
