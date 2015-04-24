#include "WindowsGUILibraryBase.h"

enum class ShowWindowCommand : int
{
    Hide = SW_HIDE,
    ShowNormal = SW_SHOWNORMAL,
    Normal = SW_NORMAL,
    ShowMinimized = SW_SHOWMINIMIZED,
    ShowMaximized = SW_SHOWMAXIMIZED,
    Maximize = SW_MAXIMIZE,
    ShowNoActivate = SW_SHOWNOACTIVATE,
    Show = SW_SHOW,
    Minimize = SW_MINIMIZE,
    ShowMinNoActive = SW_SHOWMINNOACTIVE,
    ShowNA = SW_SHOWNA,
    Restore = SW_RESTORE,
    ShowDefault = SW_SHOWDEFAULT,
    ForceMinimize = SW_FORCEMINIMIZE,
    Max = SW_MAX
};
