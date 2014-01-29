#include "ThunkWindow.h"

namespace Win32GUILibrary
{
  std::map<DWORD, std::tuple<ThunkWindow *, void *>> ThunkWindow::create_info_map;
  ThunkWindow::SpinLock ThunkWindow::spin_lock;
}
