; A simple interpreter for a minimal Scheme-like language.
; Inspired by Yin Wang's Y Programming Language: https://github.com/yinwang0/ylang.

#lang racket

; Structures for AST representation.
(struct Const (value) #:transparent)
(struct Lambda (params body) #:transparent)
(struct Variable (name) #:transparent)
(struct Define (name value) #:transparent)
(struct Assign (name value) #:transparent)
(struct If (cond then else) #:transparent)
(struct Begin (statements) #:transparent)
(struct Application (func args) #:transparent)

; Fundamental structures.
(struct Env (table parent) #:transparent)
(struct Closure (func env) #:transparent)

; Predicate for const forms.
(define (const-form? sexp)
  (or (boolean? sexp)
      (number? sexp)
      (string? sexp)))

; Predicates for special forms. Notice that sexp should be a list.
(define (define-form? sexp)
  (eq? (first sexp) 'define))

(define (lambda-form? sexp)
  (eq? (first sexp) 'lambda))

(define (begin-form? sexp)
  (eq? (first sexp) 'begin))

(define (assign-form? sexp)
  (eq? (first sexp) 'set!))

(define (if-form? sexp)
  (eq? (first sexp) 'if))

; Parse from s-expression.
(define (parse sexp)
  (cond [(not (pair? sexp)) (cond [(const-form? sexp) (Const sexp)]
                                  [(symbol? sexp) (Variable sexp)]
                                  [else (error 'parse "illegal atomic expression ~s." sexp)])]
        ; Only accept sexp as a list.
        [(not (list? sexp)) (error 'parse "expression ~s is a pair but not a list." sexp)]
        ; TODO: special forms syntax checking.
        [(lambda-form? sexp) (Lambda (second sexp)
                                     (map parse (drop sexp 2)))]
        [(define-form? sexp) (Define (second sexp)
                                     (parse (third sexp)))]
        [(assign-form? sexp) (Assign (second sexp)
                                     (parse (third sexp)))]
        [(if-form? sexp) (If (parse (second sexp))
                             (parse (third sexp))
                             (parse (fourth sexp)))]
        [(begin-form? sexp) (Begin (map parse (rest sexp)))]
        [else (Application (parse (first sexp))
                           (map parse (rest sexp)))]))

; From AST to s-expression.
(define (unparse node)
  (cond [(Const? node) (Const-value node)]
        [(Variable? node) (Variable-name node)]
        [(Lambda? node) `(lambda ,(Lambda-params node) ,@(map unparse (Lambda-body node)))]
        [(Define? node) `(define ,(Define-name node) ,(unparse (Define-value node)))]
        [(Assign? node) `(set! ,(Assign-name node) ,(unparse (Assign-value node)))]
        [(If? node) `(if ,(unparse (If-cond node))
                         ,(unparse (If-then node))
                         ,(unparse (If-else node)))]
        [(Begin? node) `(begin ,@(map unparse (Begin-statements node)))]
        [(Application? node) `(,(unparse (Application-func node)) ,@(map unparse (Application-args node)))]
        [else (error 'unparse "invalid AST node ~s." node)]))

; Create a new environment with its parent environment.
(define (make-env name-value-pairs [parent-env null])
  (Env (make-hasheq name-value-pairs) parent-env))

; Interpret.
(define (interp node env)
  ; Find the first environment table who has name in it.
  (define (find-env-table env name)
    (if (null? env)
        null
        (let ([table (Env-table env)])
          (if (hash-has-key? table name)
              table
              (find-env-table (Env-parent env) name)))))
  ; Actual interpretion process.
  (cond [(Const? node) (Const-value node)]
        [(Lambda? node) (Closure node env)]
        [(Variable? node) (let* ([name (Variable-name node)]
                                 [table (find-env-table env name)])
                            (if (null? table)
                                (error 'interp "unbound variable ~s." name)
                                (hash-ref table name)))]
        [(Define? node) (let ([name (Define-name node)]
                              [table (Env-table env)])
                          (if (hash-has-key? table name)
                              (error 'interp "variable ~s is already defined." name)
                              (hash-set! table name (interp (Define-value node) env))))]
        [(Assign? node) (let* ([name (Assign-name node)]
                               [table (find-env-table env name)])
                          (if (null? table)
                              (error 'interp "unbound variable ~s." name)
                              (hash-set! table name (interp (Assign-value node) env))))]
        ; Only boolean value can be used as condition. Different from Scheme.
        [(If? node) (let ([cond-value (interp (If-cond node) env)])
                      (if (boolean? cond-value)
                          (if cond-value
                              (interp (If-then node) env)
                              (interp (If-else node) env))
                          (error 'interp "condition doesn't produce a boolean value.")))]
        ; (begin statements ...) is transformed to ((lambda () statements ...)) so that it has its own variable scope. Different form Scheme.
        [(Begin? node) (interp (Application (Lambda null (Begin-statements node)) null) env)]
        [(Application? node) (let ([first-value (interp (Application-func node) env)])
                               (define (get-arg-values)
                                 (for/list ([arg (Application-args node)])
                                   (interp arg env)))
                               (cond [(Closure? first-value) (let* ([closure-lambda (Closure-func first-value)]
                                                                    ; The function body is in a fresh new environment.
                                                                    [env (make-env null
                                                                                   ; The paramenter environment.
                                                                                   (make-env (for/list ([param (Lambda-params closure-lambda)]
                                                                                                        [arg-value (get-arg-values)])
                                                                                               (cons param arg-value))
                                                                                             ; The closure environment.
                                                                                             (Closure-env first-value)))])
                                                               ; Interpret function body sequentially.
                                                               (let loop ([statements (Lambda-body closure-lambda)])
                                                                 (define (interp-first-statement)
                                                                   (interp (first statements) env))
                                                                 (if (= (length statements) 1)
                                                                     (interp-first-statement)
                                                                     (begin (interp-first-statement)
                                                                            (loop (rest statements))))))]
                                     ; Primitive procedures.
                                     [(procedure? first-value) (apply first-value (get-arg-values))]
                                     [else (error 'interp "expression ~s produces ~s which isn't a procedure." (unparse (Application-func node)) first-value)]))]
        [else (error 'interp "unknown node type.")]))

; Global environment.
(define *global-env*
  (make-env `((* . ,*)
              (+ . ,+)
              (- . ,-)
              (/ . ,/)
              (< . ,<)
              (<= . ,<=)
              (= . ,=)
              (> . ,>)
              (>= . ,>=)
              (add1 . ,add1)
              (sub1 . ,sub1)
              (zero? . ,zero?))))

; User interface.
(define (run sexp)
  (interp (parse sexp) *global-env*))
