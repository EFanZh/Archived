#include "stdafx.h"
#include "Font.h"
#include "FontFamily.h"

namespace DirectWriteWrapper
{
    Font::Font(::IDWriteFont *font) : ComObject(font)
    {
    }

    FontFamily ^Font::FontFamily::get()
    {
        ::IDWriteFontFamily *fontFamily;

        this->GetComObject()->GetFontFamily(&fontFamily);

        return gcnew DirectWriteWrapper::FontFamily(fontFamily);
    }

    FontWeight Font::Weight::get()
    {
        return static_cast<FontWeight>(this->GetComObject()->GetWeight());
    }

    FontStretch Font::Stretch::get()
    {
        return static_cast<FontStretch>(this->GetComObject()->GetStretch());
    }

    FontStyle Font::Style::get()
    {
        return static_cast<FontStyle>(this->GetComObject()->GetStyle());
    }

    bool Font::IsSymbolFont::get()
    {
        return this->GetComObject()->IsSymbolFont() ? true : false;
    }

    LocalizedStrings ^Font::FaceNames::get()
    {
        ::IDWriteLocalizedStrings *faceNames;

        this->GetComObject()->GetFaceNames(&faceNames);

        return gcnew LocalizedStrings(faceNames);
    }

    LocalizedStrings ^Font::GetInformationalStrings(InformationalStringId informationalStringId)
    {
        IDWriteLocalizedStrings *informationalStrings;
        BOOL exists;

        this->GetComObject()->GetInformationalStrings(static_cast<DWRITE_INFORMATIONAL_STRING_ID>(informationalStringId), &informationalStrings, &exists);

        if (exists)
        {
            return gcnew LocalizedStrings(informationalStrings);
        }
        else
        {
            return nullptr;
        }
    }

    bool Font::HasCharacter(unsigned int unicodeValue)
    {
        BOOL exists;

        this->GetComObject()->HasCharacter(unicodeValue, &exists);

        return exists ? true : false;
    }
}
