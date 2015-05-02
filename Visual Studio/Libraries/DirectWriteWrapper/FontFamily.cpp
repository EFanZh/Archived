#include "Stdafx.h"
#include "FontFamily.h"

namespace DirectWriteWrapper
{
    FontFamily::FontFamily(::IDWriteFontFamily *fontFamily) : ComObject(fontFamily)
    {
    }

    LocalizedStrings ^FontFamily::FamilyNames::get()
    {
        ::IDWriteLocalizedStrings *localizedStrings;

        this->GetComObject()->GetFamilyNames(&localizedStrings);

        return gcnew LocalizedStrings(localizedStrings);
    }

    Font ^FontFamily::GetFirstMatchingFont(FontWeight weight, FontStretch stretch, FontStyle style)
    {
        ::IDWriteFont *font;

        this->GetComObject()->GetFirstMatchingFont(static_cast<DWRITE_FONT_WEIGHT>(weight),
                                                   static_cast<DWRITE_FONT_STRETCH>(stretch),
                                                   static_cast<DWRITE_FONT_STYLE>(style),
                                                   &font);

        return gcnew Font(font);
    }

    FontList ^FontFamily::GetMatchingFonts(FontWeight weight, FontStretch stretch, FontStyle style)
    {
        ::IDWriteFontList *fontList;

        this->GetComObject()->GetMatchingFonts(static_cast<DWRITE_FONT_WEIGHT>(weight),
                                               static_cast<DWRITE_FONT_STRETCH>(stretch),
                                               static_cast<DWRITE_FONT_STYLE>(style),
                                               &fontList);

        return gcnew FontList(fontList);
    }
}
