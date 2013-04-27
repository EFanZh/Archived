#ifndef WINDOW_H
#define WINDOW_H

#define CREATE_FUNC_PARAM_LIST DWORD dwExStyle, LPCTSTR lpWindowName, DWORD dwStyle, int x, int y, int nWidth, int nHeight, HWND hWndParent, HMENU hMenu, LPVOID lpParam
#define CREATE_FUNC_PARAMS dwExStyle, lpWindowName, dwStyle, x, y, nWidth, nHeight, hWndParent, hMenu, lpParam

namespace Win32GUILibrary
{
  template<class T>
  class Window
  {
  public:
    HWND Create(CREATE_FUNC_PARAM_LIST)
    {
      return (static_cast<T *>(this)::Create(CREATE_FUNC_PARAMS));
    }
  };
}

#endif // WINDOW_H
