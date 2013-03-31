#ifndef LEXER_H
#define LEXER_H

#include "LexicalItemType.h"

using std::wstring;

class Lexer
{
  wint_t current_char;

  static bool IsSpace(wchar_t ch);
  static bool IsDigit(wchar_t ch);
  static int ToDigit(wchar_t ch);

public:
  Lexer();
  std::tuple<LexicalItemType, wstring> GetLexicalItem();
};

#endif // LEXER_H
