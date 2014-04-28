#ifndef STDAFX_H
#define STDAFX_H

#include <tchar.h>

#define WIN32_LEAN_AND_MEAN

#include <Windows.h>
#include <windowsx.h>
#include <CommCtrl.h>

#pragma comment(lib, "ComCtl32.Lib")
#pragma comment(linker, "\"/manifestdependency:type='win32' name='Microsoft.Windows.Common-Controls' version='6.0.0.0' processorArchitecture='*' publicKeyToken='6595b64144ccf1df' language='*'\"")

EXTERN_C IMAGE_DOS_HEADER __ImageBase;
#define HINST_THISCOMPONENT reinterpret_cast<HINSTANCE>(&__ImageBase)

#endif // STDAFX_H
