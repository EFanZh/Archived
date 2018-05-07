 #lang typed/racket

(define-type RawVar Symbol)
(define-type RawFun (List 'λ (Listof Symbol) RawExp))
(define-type RawApp (Pairof RawExp (Listof RawExp)))
(define-type RawExp (U RawVar RawFun RawApp))

(define-predicate raw-var? RawVar)
(define-predicate raw-fun? RawFun)
(define-predicate raw-app? RawApp)

(provide RawExp raw-var? raw-fun? raw-app?)
