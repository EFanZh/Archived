#pragma once

class MainWindow : public Window
{
    static WindowClass<MainWindow> windowClass;

    friend class WindowClass < MainWindow > ;

protected:
    IntPtr WindowProc(WindowMessage message, UIntPtr wParam, IntPtr lParam)
    {
        switch (message)
        {
            case WindowMessage::Destroy:
                ::PostQuitMessage(0);
                break;

            default:
                return Window::WindowProc(message, wParam, lParam);
        }

        return 0;
    }

public:
    MainWindow();
};
