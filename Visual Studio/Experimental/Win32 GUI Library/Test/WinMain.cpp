#include "TestWindow.h"

int WINAPI _tWinMain(_In_ HINSTANCE hInstance, _In_opt_ HINSTANCE hPrevInstance, _In_ LPTSTR lpCmdLine, _In_ int nCmdShow)
{
    TestWindow testWindow;

    testWindow.Show();

    Application::GetCurrent().Run();
}
