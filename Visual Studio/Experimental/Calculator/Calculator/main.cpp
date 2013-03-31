#include "Lexer.h"

int main()
{
  Lexer lexer;
  using std::get;
  decltype(lexer.GetLexicalItem()) li;
  while (get<0>(li = lexer.GetLexicalItem()) != END)
  {
    if (get<0>(li) == NUMBER)
    {
      std::wcout << get<1>(li).c_str() << std::endl;
    }
    else
    {
      std::wcout << static_cast<wchar_t>(get<0>(li)) << std::endl;
    }
  }
}
