#lang typed/racket

(define-type (Nullable t) (U Null t))

(struct Color ([red : Byte]
               [green : Byte]
               [blue : Byte]
               [alpha : Byte]))

(struct Style ([background : (Nullable Color)]
               [foreground : (Nullable Color)]
               [isBold : (Nullable Boolean)]
               [isItalic : (Nullable Boolean)]))

(struct Node ([styles : (Listof Style)]
              [children : (Listof Node)]))
