#lang typed/racket

(define-type (Maybe t) (U Null t))

(struct Color ([red : Byte]
               [green : Byte]
               [blue : Byte]
               [alpha : Byte]))

(struct Style ([background : (Maybe Color)]
               [foreground : (Maybe Color)]
               [isBold : (Maybe Boolean)]
               [isItalic : (Maybe Boolean)]))

(struct Node ([styles : (Listof Style)]
              [children : (Listof Node)]))

