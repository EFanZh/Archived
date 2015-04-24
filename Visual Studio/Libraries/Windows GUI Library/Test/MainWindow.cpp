#include "MainWindow.h"

WindowClass<MainWindow> MainWindow::windowClass(TEXT("Main Window"));

MainWindow::MainWindow() : Window(windowClass.GetId(), TEXT("Test"))
{
}
