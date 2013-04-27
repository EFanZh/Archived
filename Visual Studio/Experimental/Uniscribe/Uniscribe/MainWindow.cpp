#include "MainWindow.h"

void MainWindow::OnDestroy()
{
  PostQuitMessage(0);
}

void MainWindow::DoPaint(CDCHandle dc)
{
}
