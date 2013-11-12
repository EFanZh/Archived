#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include "TestScene.h"

class MainWindow : public Win32GUILibrary::UserWindow<MainWindow>
{
  TestScene scene;

public:
  MainWindow();
  LRESULT WindowProc(UINT uMsg, WPARAM wParam, LPARAM lParam);
  BOOL OnCreate(LPCREATESTRUCT lpCreateStruct);
  void OnDestroy();
  void OnPaint();

  static Win32GUILibrary::WindowClass GetWindowClass();
};

#endif MAINWINDOW_H
