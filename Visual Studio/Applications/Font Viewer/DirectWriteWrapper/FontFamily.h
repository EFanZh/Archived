#pragma once

#include "ComObject.h"
#include "LocalizedStrings.h"

namespace DirectWriteWrapper
{
    public ref class FontFamily : public ComObject < ::IDWriteFontFamily >
    {
    public:
        FontFamily(::IUnknown *fontFamily);

        LocalizedStrings ^GetFamilyNames();
    };
}
