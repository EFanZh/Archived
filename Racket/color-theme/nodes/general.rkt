#lang typed/racket

(require "node.rkt")

(provide general)

(define general (Node "General"
                      null
                      '(default
                        selected
                        [selected-inactive selected]
                        line-number
                        visible-white-space
                        keyword
                        comment
                        number
                        string
                        error)))
