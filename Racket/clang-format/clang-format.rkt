#lang typed/racket

(define *clang-format-executable* "clang-format")
(define *built-in-styles* '(LLVM Google Chromium Mozilla WebKit))

(define (get-option [style : Symbol])
  (define (parse-option-line [line : String])
    (define r (string-split line ":"))
    (list (string->symbol (string-trim (first r)))
          (string-trim (second r))))
  (define p (process (format "~a -style=~a -dump-config"
                             *clang-format-executable*
                             style)))
  (define input (first p))   
  (let loop : (HashTable Symbol Any) ([line (read-line input)])
    (cond [(eof-object? line) (make-immutable-hash)]
          [(regexp-match #rx"^[^#].*:.*" line) (let ([option (parse-option-line line)])
                                                 (hash-set (loop (read-line input))
                                                           (first option)
                                                           (second option)))]
          [else (loop (read-line input))])))

(define *style-options* (for/hash : (HashTable Symbol (HashTable Symbol Any)) ([style *built-in-styles*])
                                  (values style
                                          (get-option style))))

(define (generate [options : (Listof (List Symbol Any))])
  (define sorted-options ((inst sort (List Symbol Any) Symbol) options
                                                               symbol<?
                                                               #:key first))
  (define (based-on [style : (HashTable Symbol Any)])
    (filter (λ ([option : (List Symbol Any)])
              (and (hash-has-key? style (first option))
                   (not (equal? (hash-ref style (first option))
                                (second option)))))
            sorted-options))
  ((inst sort (Listof (List Symbol Any)) Index) (map (λ ([style : Symbol])
                                                       (cons (list 'BasedOnStyle style)
                                                             (based-on (hash-ref *style-options* style))))
                                                     *built-in-styles*)
                                                <
                                                #:key length))

(define (show-result [result : (Listof (Listof (List Symbol Any)))])
  (for ([style result])
       (displayln "---")
       (for ([option style])
            (displayln (format "~a: ~a"
                               (first option)
                               (second option))))
       (displayln "...")))

(show-result (generate '((AccessModifierOffset "-4")
                         (AlignAfterOpenBracket "Align")
                         (AlignConsecutiveAssignments "false")
                         (AlignConsecutiveDeclarations "false")
                         (AlignEscapedNewlinesLeft "true")
                         (AlignOperands "false")
                         (AlignTrailingComments "false")
                         (AllowAllParametersOfDeclarationOnNextLine "false")
                         (AllowShortBlocksOnASingleLine "false")
                         (AllowShortCaseLabelsOnASingleLine "false")
                         (AllowShortFunctionsOnASingleLine "None")
                         (AllowShortIfStatementsOnASingleLine "false")
                         (AllowShortLoopsOnASingleLine "false")
                         (AlwaysBreakAfterDefinitionReturnType "None") ; Deprecated.
                         (AlwaysBreakAfterReturnType "None")
                         (AlwaysBreakBeforeMultilineStrings "true")
                         (AlwaysBreakTemplateDeclarations "true")
                         (BinPackArguments "false")
                         (BinPackParameters "false")
                         #;(BraceWrapping "") ; How to set this?
                         #;(BreakAfterJavaFieldAnnotations "true") ; Don't care.
                         (BreakBeforeBinaryOperators "None")
                         (BreakBeforeBraces "Allman")
                         (BreakBeforeTernaryOperators "false")
                         (BreakConstructorInitializersBeforeComma "false")
                         (ColumnLimit "120")
                         #;(CommentPragmas "") ; Later.
                         (ConstructorInitializerAllOnOneLineOrOnePerLine "true")
                         (ConstructorInitializerIndentWidth "4")
                         (ContinuationIndentWidth "4")
                         (Cpp11BracedListStyle "false")
                         (DerivePointerAlignment "false")
                         (DisableFormat "false")
                         (ExperimentalAutoDetectBinPacking "false")
                         #;(ForEachMacros "")  ; Later.
                         #;(IncludeCategories "") ; Later.
                         (IndentCaseLabels "true")
                         (IndentWidth "4")
                         #;(IndentWrappedFunctionNames "") ; Later.
                         (KeepEmptyLinesAtTheStartOfBlocks "false")
                         #;(Language "") ; Later.
                         #;(MacroBlockBegin "") ; Later.
                         #;(MacroBlockEnd "") ; Later.
                         (MaxEmptyLinesToKeep "1")
                         (NamespaceIndentation "All")
                         #;(ObjCBlockIndentWidth "4") ; Don't care.
                         #;(ObjCSpaceAfterProperty "0") ; Don't care.
                         #;(ObjCSpaceBeforeProtocolList "0") ; Don't care.
                         (PenaltyBreakBeforeFirstCallParameter "800")
                         (PenaltyBreakComment "100")
                         (PenaltyBreakFirstLessLess "600")
                         (PenaltyBreakString "500")
                         (PenaltyExcessCharacter "200")
                         (PenaltyReturnTypeOnItsOwnLine "1000")
                         (PointerAlignment "Right")
                         (SpaceAfterCStyleCast "false")
                         (SpaceBeforeAssignmentOperators "true")
                         (SpaceBeforeParens "ControlStatements")
                         (SpaceInEmptyParentheses "false")
                         (SpacesBeforeTrailingComments "1")
                         (SpacesInAngles "false")
                         (SpacesInContainerLiterals "true")
                         (SpacesInCStyleCastParentheses "false")
                         (SpacesInParentheses "false")
                         (SpacesInSquareBrackets "false")
                         (Standard "Cpp11")
                         (TabWidth "4")
                         (UseTab "Never"))))
