#ifndef THUNKWINDOW_H
#define THUNKWINDOW_H

#include "WindowHandle.h"

namespace Win32GUILibrary
{
  class ThunkWindow : public WindowHandle
  {
    ATL::CStdCallThunk thunk;

    typedef std::map<DWORD, std::tuple<ThunkWindow *, void *>> CreateWindowInfoMap;
    typedef std::tuple<ThunkWindow *, void *> CreateWindowInfo;

    static CreateWindowInfoMap &GetCreateWindowInfoMap()
    {
      static CreateWindowInfoMap tid_to_cwi_map;

      return tid_to_cwi_map;
    }

    static const CreateWindowInfo ExtractCreateWindowInfo()
    {
      DWORD tid = GetCurrentThreadId();
      CreateWindowInfoMap &cwi_map = GetCreateWindowInfoMap();
      CreateWindowInfo result = cwi_map[tid];

      cwi_map.erase(tid);

      return std::move(result);
    }

  protected:
    void CreateFromHWndInternal(HWND hWnd, void *proc, int proc_type)
    {
      this->SetHWnd(hWnd);
      this->thunk.Init(reinterpret_cast<DWORD_PTR>(proc), this);
      SetWindowLongPtr(hWnd, proc_type, reinterpret_cast<LONG_PTR>(thunk.GetCodeAddress()));
    }

    static void *InitThunkProc(HWND hWnd, int proc_type)
    {
      using std::get;

      CreateWindowInfo info = ExtractCreateWindowInfo();
      ThunkWindow *p_window = get<0>(info);

      p_window->CreateFromHWndInternal(hWnd, get<1>(info), proc_type);

      return p_window->thunk.GetCodeAddress();
    }

    static void AddCreateWindowInfo(ThunkWindow *p_this, void *static_proc)
    {
      GetCreateWindowInfoMap()[GetCurrentThreadId()] = std::make_tuple(p_this, static_proc);
    }
  };
}

#endif // THUNKWINDOW_H
