 #lang typed/racket/base

(define-type Identifier Symbol)
(define-type RawVar Identifier)
(define-type RawFun (List 'λ (Listof Identifier) RawExp))
(define-type RawApp (Pairof RawExp (Listof RawExp)))
(define-type RawExp (U RawVar RawFun RawApp))

(define-predicate raw-var? RawVar)
(define-predicate raw-fun? RawFun)
(define-predicate raw-app? RawApp)
(define-predicate raw-exp? RawExp)

(provide RawExp raw-var? raw-fun? raw-app?)
