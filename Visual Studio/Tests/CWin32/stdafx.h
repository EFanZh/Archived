#ifndef STDAFX_H
#define STDAFX_H

#define STRICT
#define WIN32_LEAN_AND_MEAN

#include <Windows.h>

#pragma comment(linker, "\"/manifestdependency:type='win32' name='Microsoft.Windows.Common-Controls' version='6.0.0.0' processorArchitecture='*' publicKeyToken='6595b64144ccf1df' language='*'\"")

#define HINST_THISCOMPONENT reinterpret_cast<HINSTANCE>(&__ImageBase)

EXTERN_C IMAGE_DOS_HEADER __ImageBase;

#endif // STDAFX_H
