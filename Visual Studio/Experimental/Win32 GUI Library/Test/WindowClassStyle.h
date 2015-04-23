#pragma once

enum class WindowClassStyle : unsigned int
{
    None = 0,
    VRedraw = CS_VREDRAW,
    HRedraw = CS_HREDRAW,
    DblClks = CS_DBLCLKS,
    OwnDC = CS_OWNDC,
    ClassDC = CS_CLASSDC,
    ParentDC = CS_PARENTDC,
    NoClose = CS_NOCLOSE,
    SaveBits = CS_SAVEBITS,
    ByteAlignClient = CS_BYTEALIGNCLIENT,
    ByteAlignWindow = CS_BYTEALIGNWINDOW,
    GlobalClass = CS_GLOBALCLASS,
    IME = CS_IME,
    DropShadow = CS_DROPSHADOW
};

DEFINE_FLAGS_ENUM(WindowClassStyle)
