#ifndef STDAFX_H
#define STDAFX_H

#include <algorithm>
#include <iostream>
#include <map>
#include <set>
#include <string>
#include <vector>

#ifdef _UNICODE

typedef wchar_t tchar;

#define TS(x) L##x
#define TF(x) w##x

#else

typedef char tchar;

#define TS(x) x
#define TF(x) x

#endif // _UNICODE

#endif // STDAFX_H
