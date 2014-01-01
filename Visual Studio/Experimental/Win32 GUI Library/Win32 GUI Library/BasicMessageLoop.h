#ifndef BASICMESSAGELOOP_H
#define BASICMESSAGELOOP_H

#include "Win32GUILibraryBase.h"

namespace Win32GUILibrary
{
  class BasicMessageLoop
  {
  public:
    static int Run()
    {
      MSG msg;

      while (::GetMessage(&msg, NULL, 0, 0))
      {
        ::TranslateMessage(&msg);
        ::DispatchMessage(&msg);
      }

      return msg.wParam;
    }
  };
}

#endif // BASICMESSAGELOOP_H
