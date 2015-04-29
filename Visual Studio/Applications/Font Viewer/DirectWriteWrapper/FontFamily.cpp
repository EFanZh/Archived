#include "Stdafx.h"
#include "FontFamily.h"

namespace DirectWriteWrapper
{
    FontFamily::FontFamily(::IUnknown *fontFamily) : ComObject(fontFamily)
    {
    }
}
