#ifndef STDAFX_H
#define STDAFX_H

#include <tchar.h>
#include <Windows.h>
#include <windowsx.h>
#include <CommCtrl.h>
#include <dwmapi.h>
#include <usp10.h>
#include "resource.h"

#pragma comment(lib, "ComCtl32.Lib")
#pragma comment(lib, "Uxtheme.lib")
#pragma comment(lib, "dwmapi.lib")

#pragma comment(linker, "\"/manifestdependency:type='win32' name='Microsoft.Windows.Common-Controls' version='6.0.0.0' processorArchitecture='*' publicKeyToken='6595b64144ccf1df' language='*'\"")

#endif // STDAFX_H
