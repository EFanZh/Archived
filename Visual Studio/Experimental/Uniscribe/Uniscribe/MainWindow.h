#ifndef MAINWINDOW_H
#define MAINWINDOW_H

class MainWindow : public CDoubleBufferWindowImpl<MainWindow>
{
public:
  DECLARE_WND_CLASS(TEXT("MainWindow"))

  BEGIN_MSG_MAP(MainWindow)
    CHAIN_MSG_MAP(CDoubleBufferWindowImpl<MainWindow>)
    MSG_WM_DESTROY(OnDestroy)
  END_MSG_MAP()

  void OnDestroy();
  void MainWindow::DoPaint(CDCHandle dc);
};

#endif // MAINWINDOW_H
