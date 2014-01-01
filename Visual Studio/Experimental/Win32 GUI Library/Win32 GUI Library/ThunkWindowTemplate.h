#ifndef THUNKWINDOWTEMPLATE_H
#define THUNKWINDOWTEMPLATE_H

#include "ThunkWindow.h"

namespace Win32GUILibrary
{
  template<class T>
  class ThunkWindowTemplate : public ThunkWindow
  {
  protected:
    // Create from existing window handle.
    void CreateFromHWnd(HWND hWnd)
    {
      this->CreateFromHWndInternal(hWnd, StartWindowProc, T::PROC_TYPE);
    }

    static void ProcessWindowClass(WNDCLASSEX &wcex)
    {
      ThunkWindow::ProcessWindowClass(wcex);
      wcex.lpfnWndProc = StartWindowProc;
    }

    static typename T::ReturnType CALLBACK StartWindowProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
    {
      return static_cast<T::ProcType>(ThunkWindow::InitThunkProc(hWnd, T::PROC_TYPE))(hWnd, uMsg, wParam, lParam);
    }
  };

  template<class TProcType, class TReturnType, int proc_type>
  class ThunkWindowTemplateTrait
  {
  public:
    typedef TProcType ProcType;
    typedef TReturnType ReturnType;

    enum {
      PROC_TYPE = proc_type
    };
  };

  class ThunkWindowTemplateTraitUserWindow : public ThunkWindowTemplateTrait<WNDPROC, LRESULT, GWLP_WNDPROC>
  {
  };

  class ThunkWindowTemplateTraitUserDialogBox : public ThunkWindowTemplateTrait<DLGPROC, INT_PTR, DWLP_DLGPROC>
  {
  };
}

#endif // THUNKWINDOWTEMPLATE_H
