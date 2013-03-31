#ifndef STDAFX_H
#define STDAFX_H

#define STRICT
#define WIN32_LEAN_AND_MEAN

#include <math.h>
#include <tchar.h>
#include <Windows.h>
#include <windowsx.h>
#include <Uxtheme.h>

#pragma comment(lib, "Uxtheme.lib")

extern IMAGE_DOS_HEADER __ImageBase;
#define HINST_THISCOMPONENT ((HINSTANCE)&__ImageBase)

/* void Cls_OnPrintClient(HWND hWnd, HDC hdc, UINT uFlags) */
#define HANDLE_WM_PRINTCLIENT(hWnd, wParam, lParam, fn) \
  ((fn)((hWnd), (HDC)(wParam), (UINT)(lParam)), 0L)

#endif // STDAFX_H
