#include "message_loop.h"

MSG MessageLoop(void)
{
  BOOL ret;
  MSG msg;

  while (ret = GetMessage(&msg, NULL, 0, 0), ret)
  {
    if (ret == -1)
    {
      PostQuitMessage(EXIT_FAILURE);
    }
    else
    {
      TranslateMessage(&msg);
      DispatchMessage(&msg);
    }
  }
  return msg;
}
