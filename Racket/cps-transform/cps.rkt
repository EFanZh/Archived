#lang typed/racket

(require "./ast.rkt")

;; Data structures.

(struct CpsVar ([var : Symbol])
  #:transparent)

(struct CpsFun ([vars : (Listof Symbol)]
                [body : CpsExp])
  #:transparent)

(struct CpsApp ([rator : SimpleExp]
                [rands : (Listof SimpleExp)])
  #:transparent)

(define-type SimpleExp (U CpsVar CpsFun))
(define-type CpsExp (U CpsVar CpsFun CpsApp))

(struct EndCont ()
  #:transparent)

(struct TailCont ([var : Symbol])
  #:transparent)

(struct RatorCont ([rands : (Listof Exp)]
                   [saved-cont : Cont])
  #:transparent)

(struct RandCont ([rator : SimpleExp]
                  [vals : (Listof SimpleExp)]
                  [rands : (Listof Exp)]
                  [saved-cont : Cont])
  #:transparent)

(define-type Cont (U EndCont TailCont RatorCont RandCont))

;; Transformer.

(define (apply-cont [cont : Cont] [val : SimpleExp]) : CpsExp
  (match cont
    [(EndCont) val]
    [(TailCont var) (CpsApp (CpsVar var) (list val))]
    [(RatorCont rands saved-cont) (if (null? rands)
                                      (CpsApp val (list (CpsFun '(v) (apply-cont saved-cont (CpsVar 'v)))))
                                      (cps-helper (car rands) (RandCont val '() (cdr rands) saved-cont)))]
    [(RandCont rator vals rands saved-cont) (if (null? rands)
                                                (CpsApp rator
                                                        (reverse (cons (CpsFun '(v) (apply-cont saved-cont (CpsVar 'v)))
                                                                       (cons val vals))))
                                                (cps-helper (car rands) (RandCont val '() (cdr rands) saved-cont)))]))

(define (cps-helper [exp : Exp] [cont : Cont]) : CpsExp
  (match exp
    [(Var x) (apply-cont cont (CpsVar x))]
    [(Fun vars body) (apply-cont cont (CpsFun (append vars '(k)) (cps-helper body (TailCont 'k))))]
    [(App rator rands) (cps-helper rator (RatorCont rands cont))]))

(define (cps [exp : Exp]) : CpsExp
  (cps-helper exp (EndCont)))
