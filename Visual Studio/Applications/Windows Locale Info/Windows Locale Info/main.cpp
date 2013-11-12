#include <iostream>

#define STRICT
#define WIN32_LEAN_AND_MEAN

#include <Windows.h>

int main()
{
  using namespace std;

  TCHAR locale_name[LOCALE_NAME_MAX_LENGTH];

  wcout << "== System Default ==\n";
  wcout << L"Language Identifier: " << GetSystemDefaultLangID() << L'\n';
  wcout << L"Locale Identifier: " << GetSystemDefaultLCID() << L'\n';
  GetSystemDefaultLocaleName(locale_name, ARRAYSIZE(locale_name));
  wcout << L"Locale Name: " << locale_name << L'\n';
  wcout << L"UI Language Identifier: " << GetSystemDefaultUILanguage() << L'\n';

  wcout << L'\n';

  wcout << "== User Default ==\n";
  wcout << L"Language Identifier: " << GetUserDefaultLangID() << L'\n';
  wcout << L"Locale Identifier: " << GetUserDefaultLCID() << L'\n';
  GetUserDefaultLocaleName(locale_name, ARRAYSIZE(locale_name));
  wcout << L"Locale Name: " << locale_name << L'\n';
  wcout << L"UI Language Identifier: " << GetUserDefaultUILanguage();
  
  wcout << endl;
}
