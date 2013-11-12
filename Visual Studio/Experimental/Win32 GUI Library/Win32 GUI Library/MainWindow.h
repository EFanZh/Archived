#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include "UserWindow.h"
#include "CommonControls.h"

class MainWindow : public Win32GUILibrary::UserWindow<MainWindow>
{
  Win32GUILibrary::Button button;

public:
  static Win32GUILibrary::WindowClass GetWindowClass();
  LRESULT WindowProc(UINT uMsg, WPARAM wParam, LPARAM lParam);
};

#endif // MAINWINDOW_H
