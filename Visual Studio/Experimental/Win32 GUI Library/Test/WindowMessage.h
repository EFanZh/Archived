#pragma once

enum class WindowMessage : unsigned int
{
    Null = WM_NULL,
    Create = WM_CREATE,
    Destroy = WM_DESTROY,
    Move = WM_MOVE,
    Size = WM_SIZE,
    Activate = WM_ACTIVATE,
    SetFocus = WM_SETFOCUS,
    KillFocus = WM_KILLFOCUS,
    Enable = WM_ENABLE,
    SetRedraw = WM_SETREDRAW,
    SetText = WM_SETTEXT,
    GetText = WM_GETTEXT,
    GetTextLength = WM_GETTEXTLENGTH,
    Paint = WM_PAINT,
    Close = WM_CLOSE
};
