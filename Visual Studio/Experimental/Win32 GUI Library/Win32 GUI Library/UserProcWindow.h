#ifndef USERPROCWINDOW_H
#define USERPROCWINDOW_H

#include "WindowHandle.h"

#define CreateWindowInfo std::tuple<UserProcWindow *, WNDPROC, void *>

namespace Win32GUILibrary
{
  class UserProcWindow : public WindowHandle
  {
    ATL::CStdCallThunk thunk;

    static std::map<DWORD, CreateWindowInfo> &GetThreadIDToCreateWindowInfoMap()
    {
      static std::map<DWORD, CreateWindowInfo> tid_to_cwi_map;
      return tid_to_cwi_map;
    }

    static CreateWindowInfo ExtractCreateWindowInfo()
    {
      DWORD tid = GetCurrentThreadId();
      auto &tid_to_cwi_map = GetThreadIDToCreateWindowInfoMap();
      auto result = tid_to_cwi_map[tid];
      tid_to_cwi_map.erase(tid);
      return std::move(result);
    }

  protected:
    static LRESULT CALLBACK StartWindowProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
    {
      using std::get;

      CreateWindowInfo info = ExtractCreateWindowInfo();
      UserProcWindow *p_window = get<0>(info);
      p_window->thunk.Init(reinterpret_cast<DWORD_PTR>(get<1>(info)), get<2>(info));
      WNDPROC thunk_proc = static_cast<WNDPROC>(p_window->thunk.GetCodeAddress());
      ::SetWindowLongPtr(hWnd, GWLP_WNDPROC, reinterpret_cast<LONG_PTR>(thunk_proc));
      p_window->SetHWnd(hWnd);
      return thunk_proc(hWnd, uMsg, wParam, lParam);
    }

    static void AddCreateWindowInfo(UserProcWindow *p_user_window, WNDPROC static_proc, void *p_this)
    {
      GetThreadIDToCreateWindowInfoMap()[GetCurrentThreadId()] = std::make_tuple(p_user_window, static_proc, p_this);
    }

    LRESULT DefaultProc(UINT uMsg, WPARAM wParam, LPARAM lParam)
    {
      return ::DefWindowProc(*this, uMsg, wParam, lParam);
    }
  };
}

#endif // USERPROCWINDOW_H
