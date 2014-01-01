#include <fstream>
#include <codecvt>
#include "cvts.h"

using namespace std;
using namespace stdext;

template<class T>
void set_converter(wifstream &in_stream)
{
  in_stream.imbue(locale(in_stream.getloc(), new T()));
}

template<class T>
void set_converter(wofstream &out_stream)
{
  out_stream.imbue(locale(out_stream.getloc(), new T()));
}

int main()
{
  _CrtSetDbgFlag(_CRTDBG_ALLOC_MEM_DF | _CRTDBG_LEAK_CHECK_DF);
}
