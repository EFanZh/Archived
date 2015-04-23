#pragma once

#include <map>
#include <thread>
#include <tchar.h>
#include <Windows.h>
#include <CommCtrl.h>

#include <atlstdthunk.h>

typedef INT_PTR IntPtr;
typedef UINT_PTR UIntPtr;

#define DEFINE_FLAGS_ENUM(T) \
    T operator | (T lhs, T rhs) \
    { \
        return static_cast<T>(static_cast<std::underlying_type_t<T>>(lhs) | static_cast<std::underlying_type_t<T>>(rhs)); \
    }

#pragma comment(lib, "ComCtl32.Lib")
#pragma comment(lib, "uxtheme.Lib")
#pragma comment(lib, "atls.lib")

// Enable visual style.
#pragma comment(linker, "\"/manifestdependency:type='win32' name='Microsoft.Windows.Common-Controls' version='6.0.0.0' processorArchitecture='*' publicKeyToken='6595b64144ccf1df' language='*'\"")
