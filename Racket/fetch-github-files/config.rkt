#lang typed/racket

(provide config)

(define-type Branch (List String (Listof String)))
(define-type Repository (List String (Listof Branch)))
(define-type User (List String (Listof Repository)))

(define config : (Listof User)
  '(("adobe-fonts" (("source-code-pro" (("release" ("OTF"))))))
    ("adobe-fonts" (("source-sans-pro" (("release" ("OTF"))))))))
