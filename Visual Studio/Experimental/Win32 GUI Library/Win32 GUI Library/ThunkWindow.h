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

    static CRITICAL_SECTION *GetCriticalSection()
    {
      static CRITICAL_SECTION critical_section;
      bool need_to_initialize = true;

      if (need_to_initialize)
      {
        InitializeCriticalSection(&critical_section);
        need_to_initialize = false;
      }

      return &critical_section;
    }

    static const CreateWindowInfo ExtractCreateWindowInfo()
    {
      DWORD tid = GetCurrentThreadId();
      CreateWindowInfoMap &cwi_map = GetCreateWindowInfoMap();
      CRITICAL_SECTION *p_critical_section = GetCriticalSection();
      CreateWindowInfo result;

      EnterCriticalSection(p_critical_section);

      result = cwi_map[tid];
      cwi_map.erase(tid);

      LeaveCriticalSection(p_critical_section);

      return std::move(result);
    }

  protected:
    void CreateFromHWndInternal(HWND hWnd, void *proc, int proc_type)
    {
      this->SetHWnd(hWnd);
      this->thunk.Init(reinterpret_cast<DWORD_PTR>(proc), this);
      ::SetWindowLongPtr(hWnd, proc_type, reinterpret_cast<LONG_PTR>(thunk.GetCodeAddress()));
    }

    static void *InitThunkProc(HWND hWnd, int proc_type)
    {
      ThunkWindow *p_window;
      void *proc;

      std::tie(p_window, proc) = ExtractCreateWindowInfo();
      p_window->CreateFromHWndInternal(hWnd, proc, proc_type);

      return p_window->thunk.GetCodeAddress();
    }

    static void AddCreateWindowInfo(ThunkWindow *p_this, void *static_proc)
    {
      CRITICAL_SECTION *p_critical_section = GetCriticalSection();

      EnterCriticalSection(p_critical_section);

      GetCreateWindowInfoMap()[GetCurrentThreadId()] = std::make_tuple(p_this, static_proc);

      LeaveCriticalSection(p_critical_section);
    }
  };
}

#endif // THUNKWINDOW_H
