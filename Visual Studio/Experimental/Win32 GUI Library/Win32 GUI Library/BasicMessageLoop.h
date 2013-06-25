#ifndef BASICMESSAGELOOP_H
#define BASICMESSAGELOOP_H

namespace Win32GUILibrary
{
  class BasicMessageLoop
  {
  public:
    int Run()
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
