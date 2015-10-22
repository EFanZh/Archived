#lang typed/racket

(require "node.rkt")
(require "general.rkt")

(provide c)

(define c (Node "C"
                general
                '(preprocessor-keyword
                  preprocessor-text
                  macro)))
