#! /usr/bin/racket

#lang typed/racket

; Raw type definition.
(define-type File String)
(define-type Folder (List String (Listof Node)))
(define-type Node (U File Folder))
(define-type Branch (List String (Listof Node)))
(define-type Repository (List String (Listof Branch)))
(define-type User (List String (Listof Repository)))
(define-type Path (Listof String))

(define *config-file* "config.txt")
(define *curl-options* '("--create-dirs"))

(define (read-file [path : String])
  (call-with-input-file path read #:mode 'text))

(define *config*
  (cast (read-file *config-file*) (Listof User)))

(define (generate-url [user-name : String]
                      [repository-name : String]
                      [branch-name : String]
                      [path : Path])
  (string-join `("https://raw.githubusercontent.com"
                 ,user-name
                 ,repository-name
                 ,branch-name
                 ,@path)
               "/"))

(define (generate-local-path [user-name : String]
                             [repository-name : String]
                             [branch-name : String]
                             [path : Path])
  (string-join `(,user-name
                 ,repository-name
                 ,branch-name
                 ,@path)
               "/"))

(define (generate-download-command-line [url : String]
                                        [local-path : String])
  (string-join `("curl"
                 ,@*curl-options*
                 ,url "-o"
                 ,local-path)
               " "))

(define (get-files [node : Node]) : (Listof Path)
  (if (string? node)
      `((,node))
      (let ([folder-name (first node)])
        (apply append
               (for/list : (Listof (Listof Path)) ([child (second node)])
                 (for/list : (Listof Path) ([path (get-files child)])
                   `(,folder-name ,@path)))))))

(define (process-node [user-name : String]
                      [repository-name : String]
                      [branch-name : String]
                      [node : Node])
  (for ([path (get-files node)])
    (displayln (generate-download-command-line (generate-url user-name repository-name branch-name path)
                                               (generate-local-path user-name repository-name branch-name path)))))

(define (process-branch [user-name : String]
                        [repository-name : String]
                        [branch : Branch])
  (let ([branch-name (first branch)])
    (for ([node (second branch)])
      (process-node user-name repository-name branch-name node))))

(define (process-repository [user-name : String]
                            [repository : Repository])
  (let ([repository-name (first repository)])
    (for ([branch : Branch (second repository)])
      (process-branch user-name repository-name branch))))

(define (process-user [user : User])
  (let ([user-name (first user)])
    (for ([repository : Repository (second user)])
      (process-repository user-name repository))))

(for ([user : User *config*])
  (process-user user))
