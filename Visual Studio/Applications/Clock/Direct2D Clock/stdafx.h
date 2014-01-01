#ifndef STDAFX_H
#define STDAFX_H

#define STRICT
#define WIN32_LEAN_AND_MEAN

#include <map>
#include <tchar.h>
#include <Windows.h>
#include <CommCtrl.h>
#include <dwmapi.h>
#include <d2d1.h>

#pragma comment(lib, "ComCtl32.Lib")
#pragma comment(lib, "dwmapi.lib")
#pragma comment(lib, "d2d1.lib")
#pragma comment(linker, "\"/manifestdependency:type='win32' name='Microsoft.Windows.Common-Controls' version='6.0.0.0' processorArchitecture='*' publicKeyToken='6595b64144ccf1df' language='*'\"")

#endif // STDAFX_H
