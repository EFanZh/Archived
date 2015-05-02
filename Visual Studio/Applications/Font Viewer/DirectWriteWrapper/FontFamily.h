#pragma once

#include "ComObject.h"
#include "LocalizedStrings.h"
#include "FontList.h"

namespace DirectWriteWrapper
{
    public ref class FontFamily : public ComObject < ::IDWriteFontFamily >
    {
    internal:
        FontFamily(::IDWriteFontFamily *fontFamily);

    public:
        property LocalizedStrings ^FamilyNames
        {
            LocalizedStrings ^get();
        }

        Font ^GetFirstMatchingFont(FontWeight weight, FontStretch stretch, FontStyle style);
        FontList ^GetMatchingFonts(FontWeight weight, FontStretch stretch, FontStyle style);
    };
}
