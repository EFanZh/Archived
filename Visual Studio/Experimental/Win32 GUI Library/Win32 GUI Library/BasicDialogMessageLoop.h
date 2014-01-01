#ifndef BASICDIALOGMESSAGELOOP_H
#define BASICDIALOGMESSAGELOOP_H

#include "Win32GUILibraryBase.h"

namespace Win32GUILibrary
{
  class BasicDialogMessageLoop
  {
  public:
    static int Run(HWND hWnd)
    {
      MSG msg;

      while (::GetMessage(&msg, NULL, 0, 0))
      {
        if (!IsDialogMessage(hWnd, &msg))
        {
          ::TranslateMessage(&msg);
          ::DispatchMessage(&msg);
        }
      }

      return msg.wParam;
    }
  };
}

#endif // BASICDIALOGMESSAGELOOP_H
