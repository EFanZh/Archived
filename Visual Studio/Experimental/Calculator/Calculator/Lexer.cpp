#include "Lexer.h"

Lexer::Lexer()
{
}

bool Lexer::IsSpace(wchar_t ch)
{
  return ch == L' ' || ch == L'\t';
}

bool Lexer::IsDigit(wchar_t ch)
{
  return iswdigit(ch);
}

int Lexer::ToDigit(wchar_t ch)
{
  switch (ch)
  {
  case L'0':
    return 0;
  case L'1':
    return 1;
  case L'2':
    return 2;
  case L'3':
    return 3;
  case L'4':
    return 4;
  case L'5':
    return 5;
  case L'6':
    return 6;
  case L'7':
    return 7;
  case L'8':
    return 8;
  case L'9':
    return 9;
  default:
    return -1;
  }
}

std::tuple<LexicalItemType, wstring> Lexer::GetLexicalItem()
{
  using std::make_tuple;

  while (IsSpace(current_char = getwchar()))
  {
    continue;
  }

  switch (current_char)
  {
  case L'\n':
    return make_tuple(END, wstring());
  case L'(':
    return make_tuple(LEFT_PARENTHESIS, wstring());
  case L')':
    return make_tuple(RIGHT_PARENTHESIS, wstring());
  case L'*':
    return make_tuple(ASTERISK, wstring());
  case L'+':
    return make_tuple(PLUS_SIGN, wstring());
  case L'-':
    return make_tuple(MINUS_SIGN, wstring());
  case L'/':
    return make_tuple(SLASH, wstring());
  default:
    if (IsDigit(current_char))
    {
      wstring value;
      value += current_char;
      while (IsDigit(current_char = getwchar()))
      {
        value += current_char;
      }
      if (current_char == L'.')
      {
        value += L'.';
        while (IsDigit(current_char = getwchar()))
        {
          value += current_char;
        }
      }
      ungetwc(current_char, stdin);
      return make_tuple(NUMBER, std::move(value));
    }
    return make_tuple(OTHER, wstring());
    break;
  }
}
