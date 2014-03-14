#ifndef STDAFX_H
#define STDAFX_H

// STL headers.
#include <atomic>
#include <memory>
#include <set>
#include <thread>
#include <vector>

// CRT headers.
#include <tchar.h>

#define WIN32_LEAN_AND_MEAN

// WTL headers.
#include <atlbase.h>
#include <atlapp.h>
#include <atlframe.h>
#include <atlcrack.h>
#include <atlctrls.h>

// Windows SDK headers.

#include <threadpoolapiset.h>
#include <MSWSock.h>

#undef BEGIN_MSG_MAP
#define BEGIN_MSG_MAP BEGIN_MSG_MAP_EX

#pragma comment(linker, "/manifestdependency:\"type='win32' name='Microsoft.Windows.Common-Controls' version='6.0.0.0' processorArchitecture='*' publicKeyToken='6595b64144ccf1df' language='*'\"")
#pragma comment(lib, "Ws2_32.lib")

#endif // STDAFX_H
