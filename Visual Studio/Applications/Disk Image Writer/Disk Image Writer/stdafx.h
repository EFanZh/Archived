#ifndef STDAFX_H
#define STDAFX_H

#define STRICT

#include <strsafe.h>
#include <Windows.h>
#include <windowsx.h>
#include <CommCtrl.h>
#define COBJMACROS
#define CINTERFACE
#undef __cplusplus
#include <ShObjIdl.h>

#pragma comment(lib, "ComCtl32.Lib")
#pragma comment(linker, "\"/manifestdependency:type='win32' name='Microsoft.Windows.Common-Controls' version='6.0.0.0' processorArchitecture='*' publicKeyToken='6595b64144ccf1df' language='*'\"")

#endif // STDAFX_H
