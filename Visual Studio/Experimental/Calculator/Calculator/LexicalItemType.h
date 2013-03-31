#ifndef LEXICALITEMTYPE_H
#define LEXICALITEMTYPE_H

enum LexicalItemType
{
  END = '\n',
  LEFT_PARENTHESIS = L'(',
  RIGHT_PARENTHESIS = L')',
  ASTERISK = L'*',
  PLUS_SIGN = L'+',
  MINUS_SIGN = L'-',
  SLASH = L'/',
  NUMBER,
  OTHER
};

#endif // LEXICALITEMTYPE_H
