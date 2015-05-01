#pragma once

#include "LocalizedStrings.h"
#include "InformationalStringId.h"
#include "FontWeight.h"
#include "FontStretch.h"
#include "FontStyle.h"

namespace DirectWriteWrapper
{
    ref class FontFamily;

    public ref class Font : ComObject < ::IDWriteFont >
    {
    public:
        Font(::IDWriteFont *font);

        property DirectWriteWrapper::FontFamily ^FontFamily
        {
            DirectWriteWrapper::FontFamily ^get();
        }

        property FontWeight Weight
        {
            FontWeight get();
        }

        property FontStretch Stretch
        {
            FontStretch get();
        }

        property FontStyle Style
        {
            FontStyle get();
        }

        property bool IsSymbolFont
        {
            bool get();
        }

        property LocalizedStrings ^FaceNames
        {
            LocalizedStrings ^get();
        }

        LocalizedStrings ^GetInformationalStrings(InformationalStringId informationalStringId);

        bool HasCharacter(unsigned int unicodeValue);
    };
}
