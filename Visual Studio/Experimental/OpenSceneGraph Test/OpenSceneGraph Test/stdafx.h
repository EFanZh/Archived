#ifndef STDAFX_H
#define STDAFX_H

#define STRICT
#define WIN32_LEAN_AND_MEAN

#include <Windows.h>
#include <windowsx.h>

EXTERN_C IMAGE_DOS_HEADER __ImageBase;
#define HINST_THISCOMPONENT reinterpret_cast<HINSTANCE>(&__ImageBase)

#endif // STDAFX_H
