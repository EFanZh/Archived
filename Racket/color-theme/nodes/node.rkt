#lang typed/racket

(provide (struct-out Node))
(provide root-node)
(provide register-node)

(struct Language ([name : String]
                  [base-on : (U Null Node)]
                  [styles : (Listof (U Symbol (List Symbol Symbol)))]))

(struct Node ([language : Language]
              [children : (Listof Node)] #:mutable))

(define *root-node* undefined)
(define *language->node* (make-hash))

(define (register-language [language : Language])
  (let ([base-on (Language-base-on language)])
    (hash-set! language (Node language null))
    (if (Null? base-on)
        (set! *root-node* (Node language null))
        (let ([node (hash-ref *language->node* language)])
          (Node-styles-set! node
                            (cons language (Node-styles node)))))))
