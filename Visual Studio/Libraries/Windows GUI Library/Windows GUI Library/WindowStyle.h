#pragma once

#include "WindowsGUILibraryBase.h"

enum class WindowStyle : unsigned int
{
    Overlapped = WS_OVERLAPPED,
    PopUp = WS_POPUP,
    Child = WS_CHILD,
    Minimize = WS_MINIMIZE,
    Visible = WS_VISIBLE,
    Disabled = WS_DISABLED,
    ClipSiblings = WS_CLIPSIBLINGS,
    ClipChildren = WS_CLIPCHILDREN,
    Maximize = WS_MAXIMIZE,
    Caption = WS_CAPTION,
    Border = WS_BORDER,
    DlgFrame = WS_DLGFRAME,
    VScroll = WS_VSCROLL,
    HScroll = WS_HSCROLL,
    SysMenu = WS_SYSMENU,
    ThickFrame = WS_THICKFRAME,
    Group = WS_GROUP,
    TabStop = WS_TABSTOP,
    MinimizeBox = WS_MINIMIZEBOX,
    MaximizeBox = WS_MAXIMIZEBOX,
    Tiled = WS_TILED,
    Iconic = WS_ICONIC,
    SizeBox = WS_SIZEBOX,
    TiledWindow = WS_TILEDWINDOW,
    OverlappedWindow = WS_OVERLAPPEDWINDOW,
    PopUpWindow = WS_POPUPWINDOW,
    ChildWindow = WS_CHILDWINDOW
};

DEFINE_FLAGS_ENUM(WindowStyle)
