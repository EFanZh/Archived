﻿#include "Stdafx.h"
#include "FontFamily.h"

namespace DirectWriteWrapper
{
    FontFamily::FontFamily(::IDWriteFontFamily *fontFamily) : ComObject(fontFamily)
    {
    }

    LocalizedStrings ^FontFamily::FamilyNames::get()
    {
        ::IDWriteLocalizedStrings *localizedStrings;

        HRESULT hr = this->GetComObject()->GetFamilyNames(&localizedStrings);

        assert(SUCCEEDED(hr));

        return gcnew LocalizedStrings(localizedStrings);
    }

    Font ^FontFamily::GetFirstMatchingFont(FontWeight weight, FontStretch stretch, FontStyle style)
    {
        ::IDWriteFont *font;

        HRESULT hr = this->GetComObject()->GetFirstMatchingFont(static_cast<DWRITE_FONT_WEIGHT>(weight),
                                                                static_cast<DWRITE_FONT_STRETCH>(stretch),
                                                                static_cast<DWRITE_FONT_STYLE>(style),
                                                                &font);

        assert(SUCCEEDED(hr));

        return gcnew Font(font);
    }

    FontList ^FontFamily::GetMatchingFonts(FontWeight weight, FontStretch stretch, FontStyle style)
    {
        ::IDWriteFontList *fontList;

        HRESULT hr = this->GetComObject()->GetMatchingFonts(static_cast<DWRITE_FONT_WEIGHT>(weight),
                                                            static_cast<DWRITE_FONT_STRETCH>(stretch),
                                                            static_cast<DWRITE_FONT_STYLE>(style),
                                                            &fontList);

        assert(SUCCEEDED(hr));

        return gcnew FontList(fontList);
    }
}
