#ifndef MAINWINDOW_H
#define MAINWINDOW_H

class MainWindow : public Win32GUILibrary::UserWindow<MainWindow>
{
  friend UserWindow;

  static ATOM class_atom;

  LRESULT WindowProc(UINT uMsg, WPARAM wParam, LPARAM lParam);

public:
  MainWindow();

  void OnDestroy();

  static void Initialize();
};

#endif // MAINWINDOW_H
