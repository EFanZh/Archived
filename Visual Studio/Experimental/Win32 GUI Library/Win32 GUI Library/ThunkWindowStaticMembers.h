#include "ThunkWindow.h"

namespace Win32GUILibrary
{
  std::map<DWORD, ThunkWindow::CreateInfo> ThunkWindow::create_info_map;
  ThunkWindow::SpinLock ThunkWindow::spin_lock;
}
