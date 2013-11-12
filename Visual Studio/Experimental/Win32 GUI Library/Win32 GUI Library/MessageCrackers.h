#ifndef MESSAGECRACKER_H
#define MESSAGECRACKER_H

// BOOL OnCreate(LPCREATESTRUCT lpCreateStruct)
#define WGL_HANDLE_WM_CREATE(fn) case WM_CREATE: return fn(reinterpret_cast<LPCREATESTRUCT>(lParam)) ? 0 : -1

// void OnDestroy()
#define WGL_HANDLE_WM_DESTROY(fn) case WM_DESTROY: return fn(), 0

// void OnPaint()
#define WGL_HANDLE_WM_PAINT(fn) case WM_PAINT: return fn(), 0

// BOOL OnSetCursor(HWND hWndCursor, UINT codeHitTest, UINT msg)
#define WGL_HANDLE_WM_SETCURSOR(fn) case WM_SETCURSOR: return fn(reinterpret_cast<HWND>(wParam), LOWORD(lParam), HIWORD(lParam))

#endif // MESSAGECRACKER_H
