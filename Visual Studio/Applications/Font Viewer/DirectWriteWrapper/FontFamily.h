#pragma once

#include "ComObject.h"

namespace DirectWriteWrapper
{
    public ref class FontFamily : public ComObject < ::IDWriteFontFamily >
    {
    public:
        FontFamily(::IUnknown *fontFamily);
    };
}
