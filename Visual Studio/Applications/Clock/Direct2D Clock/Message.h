#ifndef MESSAGE_H
#define MESSAGE_H

class Message
{
  MSG msg;

public:
  Message()
  {
    memset(&msg, 0, sizeof(msg));
  }

  HWND GetHWnd() const
  {
    return msg.hwnd;
  }

  UINT GetMessage() const
  {
    return msg.message;
  }

  WPARAM GetWParam() const
  {
    return msg.wParam;
  }

  WPARAM GetLParam() const
  {
    return msg.lParam;
  }

  LRESULT Dispatch()
  {
    return DispatchMessage(&msg);
  }

  BOOL Get(HWND hWnd, UINT wMsgFilterMin, UINT wMsgFilterMax)
  {
    return ::GetMessage(&msg, hWnd, wMsgFilterMin, wMsgFilterMax);
  }

  BOOL Peek(HWND hWnd, UINT wMsgFilterMin, UINT wMsgFilterMax, UINT wRemoveMsg)
  {
    return PeekMessage(&msg, hWnd,wMsgFilterMin, wMsgFilterMax, wRemoveMsg);
  }

  BOOL Translate()
  {
    return TranslateMessage(&msg);
  }
};

#endif // MESSAGE_H
