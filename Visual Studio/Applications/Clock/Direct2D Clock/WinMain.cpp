#include "MainWindow.h"
#include "Message.h"

int APIENTRY _tWinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPTSTR lpCmdLine, int nCmdShow)
{
    MainWindow main_window;

    if (!main_window.Create(TEXT("Direct2D Clock")))
    {
        return EXIT_FAILURE;
    }
    main_window.Show(nCmdShow);
    main_window.Update();

    Message message;

    while (BOOL ret = message.Get(NULL, 0, 0))
    {
        if (ret == -1)
        {
            return EXIT_FAILURE;
        }
        message.Translate();
        message.Dispatch();
    }

    return message.GetWParam();
}
