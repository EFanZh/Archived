#ifndef STDAFX_H
#define STDAFX_H

#define STRICT
#define WIN32_LEAN_AND_MEAN

#include <math.h>
#include <tchar.h>
#include <Windows.h>
#include <windowsx.h>
#include <CommCtrl.h>
#include <Uxtheme.h>

#pragma comment(lib, "ComCtl32.Lib")
#pragma comment(lib, "Uxtheme.lib")
#pragma comment(linker, "\"/manifestdependency:type='win32' name='Microsoft.Windows.Common-Controls' version='6.0.0.0' processorArchitecture='*' publicKeyToken='6595b64144ccf1df' language='*'\"")

extern IMAGE_DOS_HEADER __ImageBase;
#define HINST_THISCOMPONENT ((HINSTANCE)&__ImageBase)

/* void Cls_OnPrintClient(HWND hWnd, HDC hdc, UINT uFlags) */
#define HANDLE_WM_PRINTCLIENT(hWnd, wParam, lParam, fn) \
  ((fn)((hWnd), (HDC)(wParam), (UINT)(lParam)), 0L)

#endif // STDAFX_H
