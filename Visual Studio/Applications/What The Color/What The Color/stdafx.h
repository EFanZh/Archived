#ifndef STDAFX_H
#define STDAFX_H

#define STRICT
#define WIN32_LEAN_AND_MEAN

#include <tchar.h>
#include <Windows.h>
#include <WindowsX.h>
#include <strsafe.h>
#include <Uxtheme.h>

#pragma comment(lib, "ComCtl32.Lib")
#pragma comment(lib, "Uxtheme.lib")
#pragma comment(linker, "\"/manifestdependency:type='win32' name='Microsoft.Windows.Common-Controls' version='6.0.0.0' processorArchitecture='*' publicKeyToken='6595b64144ccf1df' language='*'\"")

#endif // STDAFX_H
