#ifndef WIN32GUILIBRARYBASE_H
#define WIN32GUILIBRARYBASE_H

// STL header.
#include <map>
#include <tuple>

// Windows header.
#define STRICT
#define WIN32_LEAN_AND_MEAN
#include <Windows.h>
#include <windowsx.h>
#include <CommCtrl.h>
#include <Uxtheme.h>
#include <dwmapi.h>
#include <atlstdthunk.h>

// Libraries.
#ifdef _DEBUG
#ifdef _UNICODE
#pragma comment(lib, "atlsd.lib")
#else
#pragma comment(lib, "atlsnd.lib")
#endif
#else
#ifdef _UNICODE
#pragma comment(lib, "atls.lib")
#else
#pragma comment(lib, "atlsn.lib")
#endif
#endif

// Enable visual style.
#pragma comment(linker, "\"/manifestdependency:type='win32' name='Microsoft.Windows.Common-Controls' version='6.0.0.0' processorArchitecture='*' publicKeyToken='6595b64144ccf1df' language='*'\"")

// Module instance.
EXTERN_C IMAGE_DOS_HEADER __ImageBase;
#define HINST_THISCOMPONENT reinterpret_cast<HINSTANCE>(&__ImageBase)

#endif // WIN32GUILIBRARYBASE_H
