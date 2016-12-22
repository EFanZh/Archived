#lang typed/racket

(require/typed yaml
               [read-yaml (-> Input-Port Any)]
               [write-yaml* (->* ((Listof Any))
                                 (#:style (U 'block 'flow 'best)
                                  #:sort-mapping (U (-> Any Any Boolean) False)
                                  #:sort-mapping-key (-> (Pairof Any Any) Any))
                                 Void)])

(define *clang-format-executable* "clang-format")

(define *my-options* : (Listof Option)
  '((AccessModifierOffset . -4)
    (AlignAfterOpenBracket . Align)
    (AlignConsecutiveAssignments . #f)
    (AlignConsecutiveDeclarations . #f)
    (AlignEscapedNewlinesLeft . #t)
    (AlignOperands . #t)
    (AlignTrailingComments . #f)
    (AllowAllParametersOfDeclarationOnNextLine . #f) ; Later.
    (AllowShortBlocksOnASingleLine . #f)
    (AllowShortCaseLabelsOnASingleLine . #f)
    (AllowShortFunctionsOnASingleLine . None)
    (AllowShortIfStatementsOnASingleLine . #f)
    (AllowShortLoopsOnASingleLine . #f)
    #;(AlwaysBreakAfterDefinitionReturnType . None) ; Deprecated.
    (AlwaysBreakAfterReturnType . None)
    #;(AlwaysBreakBeforeMultilineStrings . #t) ; Later.
    (AlwaysBreakTemplateDeclarations . #t)
    (BinPackArguments . #t)
    (BinPackParameters . #t)
    #;(BraceWrapping . ()) ; Ignored.
    #;(BreakAfterJavaFieldAnnotations . #t) ; Don't care.
    (BreakBeforeBinaryOperators . None) ; Later.
    (BreakBeforeBraces . Allman)
    (BreakBeforeTernaryOperators . #f)
    (BreakConstructorInitializersBeforeComma . #f)
    (BreakStringLiterals . #t)
    (ColumnLimit . 120)
    #;(CommentPragmas . "") ; Later.
    (ConstructorInitializerAllOnOneLineOrOnePerLine . #t)
    (ConstructorInitializerIndentWidth . 4)
    (ContinuationIndentWidth . 4)
    (Cpp11BracedListStyle . #f)
    (DerivePointerAlignment . #f)
    (DisableFormat . #f)
    (ExperimentalAutoDetectBinPacking . #f)
    #;(ForEachMacros . "") ; Later.
    #;(IncludeCategories . "") ; Later.
    #;(IncludeIsMainRegex . "") ; Later.
    (IndentCaseLabels . #t)
    (IndentWidth . 4)
    (IndentWrappedFunctionNames . #t)
    #;(JavaScriptQuotes . Leave) ; Don't care.
    (KeepEmptyLinesAtTheStartOfBlocks . #f)
    (Language . Cpp)
    #;(MacroBlockBegin . "") ; Later.
    #;(MacroBlockEnd . "") ; Later.
    (MaxEmptyLinesToKeep . 1)
    (NamespaceIndentation . All)
    #;(ObjCBlockIndentWidth . 4) ; Don't care.
    #;(ObjCSpaceAfterProperty . 0) ; Don't care.
    #;(ObjCSpaceBeforeProtocolList . 0) ; Don't care.
    (PenaltyBreakBeforeFirstCallParameter . 800) ; Later.
    (PenaltyBreakComment . 100) ; Later.
    (PenaltyBreakFirstLessLess . 600) ; Later.
    (PenaltyBreakString . 500) ; Later.
    (PenaltyExcessCharacter . 200) ; Later.
    (PenaltyReturnTypeOnItsOwnLine . 1000) ; Later.
    (PointerAlignment . Right)
    (ReflowComments . #t)
    (SortIncludes . #t)
    (SpaceAfterCStyleCast . #f)
    (SpaceAfterTemplateKeyword . #t)
    (SpaceBeforeAssignmentOperators . #t)
    (SpaceBeforeParens . ControlStatements)
    (SpaceInEmptyParentheses . #f)
    (SpacesBeforeTrailingComments . 1)
    (SpacesInAngles . #f)
    (SpacesInCStyleCastParentheses . #f)
    #;(SpacesInContainerLiterals . #t) ; Don't care.
    (SpacesInParentheses . #f)
    (SpacesInSquareBrackets . #f)
    (Standard . Cpp11)
    (TabWidth . 4)
    (UseTab . Never)))

(define-type BuiltInStyle (U 'LLVM 'Google 'Chromium 'Mozilla 'WebKit))

(define *built-in-styles* '(LLVM Google Chromium Mozilla WebKit))

(define-type Option (U (Pairof 'BasedOnStyle BuiltInStyle)
                       (Pairof 'AccessModifierOffset Integer)
                       (Pairof 'AlignAfterOpenBracket (U 'Align 'DontAlign 'AlwaysBreak))
                       (Pairof 'AlignConsecutiveAssignments Boolean)
                       (Pairof 'AlignConsecutiveDeclarations Boolean)
                       (Pairof 'AlignEscapedNewlinesLeft Boolean)
                       (Pairof 'AlignOperands Boolean)
                       (Pairof 'AlignTrailingComments Boolean)
                       (Pairof 'AllowAllParametersOfDeclarationOnNextLine Boolean)
                       (Pairof 'AllowShortBlocksOnASingleLine Boolean)
                       (Pairof 'AllowShortCaseLabelsOnASingleLine Boolean)
                       (Pairof 'AllowShortFunctionsOnASingleLine (U 'None 'Empty 'Inline 'All))
                       (Pairof 'AllowShortIfStatementsOnASingleLine Boolean)
                       (Pairof 'AllowShortLoopsOnASingleLine Boolean)
                       (Pairof 'AlwaysBreakAfterDefinitionReturnType (U 'None 'All 'TopLevel))
                       (Pairof 'AlwaysBreakAfterReturnType (U 'None 'All 'TopLevel 'AllDefinitions 'TopLevelDefinitions))
                       (Pairof 'AlwaysBreakBeforeMultilineStrings Boolean)
                       (Pairof 'AlwaysBreakTemplateDeclarations Boolean)
                       (Pairof 'BinPackArguments Boolean)
                       (Pairof 'BinPackParameters Boolean)
                       (Pairof 'BraceWrapping (Listof (U (Pairof 'AfterClass Boolean)
                                                         (Pairof 'AfterControlStatement Boolean)
                                                         (Pairof 'AfterEnum Boolean)
                                                         (Pairof 'AfterFunction Boolean)
                                                         (Pairof 'AfterNamespace Boolean)
                                                         (Pairof 'AfterObjCDeclaration Boolean)
                                                         (Pairof 'AfterStruct Boolean)
                                                         (Pairof 'AfterUnion Boolean)
                                                         (Pairof 'BeforeCatch Boolean)
                                                         (Pairof 'BeforeElse Boolean)
                                                         (Pairof 'IndentBraces Boolean))))
                       (Pairof 'BreakAfterJavaFieldAnnotations Boolean)
                       (Pairof 'BreakBeforeBinaryOperators (U 'None 'NonAssignment 'All))
                       (Pairof 'BreakBeforeBraces (U 'Attach 'Linux 'Mozilla 'Stroustrup 'Allman 'GNU 'WebKit 'Custom))
                       (Pairof 'BreakBeforeTernaryOperators Boolean)
                       (Pairof 'BreakConstructorInitializersBeforeComma Boolean)
                       (Pairof 'BreakStringLiterals Boolean)
                       (Pairof 'ColumnLimit Nonnegative-Integer)
                       (Pairof 'CommentPragmas String)
                       (Pairof 'ConstructorInitializerAllOnOneLineOrOnePerLine Boolean)
                       (Pairof 'ConstructorInitializerIndentWidth Nonnegative-Integer)
                       (Pairof 'ContinuationIndentWidth Nonnegative-Integer)
                       (Pairof 'Cpp11BracedListStyle Boolean)
                       (Pairof 'DerivePointerAlignment Boolean)
                       (Pairof 'DisableFormat Boolean)
                       (Pairof 'ExperimentalAutoDetectBinPacking Boolean)
                       (Pairof 'ForEachMacros (Listof String))
                       (Pairof 'IncludeCategories (Listof (List (Pairof 'Regex String)
                                                                (Pairof 'Priority Integer))))
                       (Pairof 'IncludeIsMainRegex String)
                       (Pairof 'IndentCaseLabels Boolean)
                       (Pairof 'IndentWidth Nonnegative-Integer)
                       (Pairof 'IndentWrappedFunctionNames Boolean)
                       (Pairof 'JavaScriptQuotes (U 'Leave 'Single 'Double))
                       (Pairof 'JavaScriptWrapImports Boolean) ; Not documented.
                       (Pairof 'KeepEmptyLinesAtTheStartOfBlocks Boolean)
                       (Pairof 'Language (U 'None 'Cpp 'Java 'JavaScript 'Proto 'TableGen))
                       (Pairof 'MacroBlockBegin String)
                       (Pairof 'MacroBlockEnd String)
                       (Pairof 'MaxEmptyLinesToKeep Nonnegative-Integer)
                       (Pairof 'NamespaceIndentation (U 'None 'Inner 'All))
                       (Pairof 'ObjCBlockIndentWidth Nonnegative-Integer)
                       (Pairof 'ObjCSpaceAfterProperty Boolean)
                       (Pairof 'ObjCSpaceBeforeProtocolList Boolean)
                       (Pairof 'PenaltyBreakBeforeFirstCallParameter Nonnegative-Integer)
                       (Pairof 'PenaltyBreakComment Nonnegative-Integer)
                       (Pairof 'PenaltyBreakFirstLessLess Nonnegative-Integer)
                       (Pairof 'PenaltyBreakString Nonnegative-Integer)
                       (Pairof 'PenaltyExcessCharacter Nonnegative-Integer)
                       (Pairof 'PenaltyReturnTypeOnItsOwnLine Nonnegative-Integer)
                       (Pairof 'PointerAlignment (U 'Left 'Right 'Middle))
                       (Pairof 'ReflowComments Boolean)
                       (Pairof 'SortIncludes Boolean)
                       (Pairof 'SpaceAfterCStyleCast Boolean)
                       (Pairof 'SpaceAfterTemplateKeyword Boolean)
                       (Pairof 'SpaceBeforeAssignmentOperators Boolean)
                       (Pairof 'SpaceBeforeParens (U 'Never 'ControlStatements 'Always))
                       (Pairof 'SpaceInEmptyParentheses Boolean)
                       (Pairof 'SpacesBeforeTrailingComments Nonnegative-Integer)
                       (Pairof 'SpacesInAngles Boolean)
                       (Pairof 'SpacesInCStyleCastParentheses Boolean)
                       (Pairof 'SpacesInContainerLiterals Boolean)
                       (Pairof 'SpacesInParentheses Boolean)
                       (Pairof 'SpacesInSquareBrackets Boolean)
                       (Pairof 'Standard (U 'Cpp03 'Cpp11 'Auto))
                       (Pairof 'TabWidth Nonnegative-Integer)
                       (Pairof 'UseTab (U 'Never 'ForIndentation 'Always))))

(define-type (Attribute V) (Pairof Symbol V))
(define-type (Object V) (Listof (Attribute V)))

(define (get-option [style : BuiltInStyle])
  (define (make-option [key : String] value)
    (define (get-value [value : Any]) : Any
      (match value
        [(? string?) (string->symbol value)]
        [(? list?) (for/list : (Listof Any) ([item value])
                     (get-value item))]
        [_ value]))
    (let* ([symbol-key (string->symbol key)]
           [value (match symbol-key
                    [(or 'CommentPragmas
                         'ForEachMacros
                         'IncludeIsMainRegex
                         'MacroBlockBegin
                         'MacroBlockEnd) value]
                    ['BraceWrapping (for/list : (Object Boolean) ([(key value) (in-hash (cast value (HashTable String Boolean)))])
                                      (cons (string->symbol key) value))]
                    ['IncludeCategories (for/list : (Listof (Object Any)) ([item (cast value (Listof (HashTable String Any)))])
                                          (for/list : (Object Any) ([(key value) (in-hash item)])
                                            (cons (string->symbol key) value)))]
                    [_ (get-value value)])])
      (cast (cons symbol-key value) Option)))
  (let* ([command-line (format "~a -style=~a -dump-config" *clang-format-executable* style)]
         [clang-format-process (process command-line)]
         [stdout (first clang-format-process)]
         [content (cast (read-yaml stdout) (HashTable String Any))])
    (for/list : (Listof Option) ([(key value) (in-hash content)])
      (make-option key value))))

(define (normalize-options [options : (Listof Option)] [base-style : BuiltInStyle])
  (define (compare-object-value [value : (Object Any)] [base-value : (Object Any)]) : (Object Any)
    (let loop : (Object Any) ([attributes value])
      (if (null? attributes) null
          (let* ([attribute (first attributes)]
                 [attribute-name (car attribute)]
                 [attribute-value (cdr attribute)]
                 [base-attribute (assq attribute-name base-value)])
            (if base-attribute
                (let ([compare-value-result (compare-value attribute-value (cdr base-attribute))])
                  (if (void? compare-value-result)
                      (loop (rest attributes))
                      (cons (cons attribute-name compare-value-result) (loop (rest attributes)))))
                (loop (rest attributes)))))))
  (define (compare-value [value : Any] [base-value : Any]) : Any
    (define-predicate object? (Object Any))
    (cond [(object? value) (let ([object-compare-result (compare-object-value value (assert base-value object?))])
                             (unless (null? object-compare-result)
                               object-compare-result))]
          [(equal? value base-value) (void)]
          [else value]))
  (let* ([raw-result (compare-object-value options (get-option base-style))]
         [result (cast raw-result (Listof Option))])
    (#{sort @ Option Symbol} result symbol<? #:key car)))

(define (to-yaml [x : Any]) : Any
  (match x
    [(? symbol?) (symbol->string x)]
    [(? (make-predicate (Listof (Pairof Symbol Any)))) (make-hash (for/list : (Listof (Pairof String Any)) ([item x])
                                                                    (cons (symbol->string (car item))
                                                                          (to-yaml (cdr item)))))]
    [(? list?) (map to-yaml x)]
    [_ x]))

(time (let* ([all-results (for/list : (Listof (Listof Option)) ([base-style : BuiltInStyle *built-in-styles*])
                            (cons (cons 'BasedOnStyle base-style)
                                  (normalize-options *my-options* base-style)))]
             [sorted-all-results (#{sort @ (Listof Option) Index} all-results < #:key length #:cache-keys? true)]
             [sorted-all-yaml-results (cast (map to-yaml sorted-all-results) (Listof (HashTable String Any)))])
        (for ([result sorted-all-yaml-results])
          (display "# Total option count: ")
          (displayln (hash-count result))
          (write-yaml* (list result)
                       #:style 'block
                       #:sort-mapping (cast string<? (-> Any Any Boolean))
                       #:sort-mapping-key (λ ([x : (Pairof Any Any)])
                                            (car x)))
          (newline))))
