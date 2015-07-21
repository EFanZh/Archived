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

        HRESULT hr = this->GetComObject()->GetFontFamily(&fontFamily);

        assert(SUCCEEDED(hr));

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

        HRESULT hr = this->GetComObject()->GetFaceNames(&faceNames);

        assert(SUCCEEDED(hr));

        return gcnew LocalizedStrings(faceNames);
    }

    LocalizedStrings ^Font::GetInformationalStrings(InformationalStringId informationalStringId)
    {
        IDWriteLocalizedStrings *informationalStrings;
        BOOL exists;

        HRESULT hr = this->GetComObject()->GetInformationalStrings(static_cast<DWRITE_INFORMATIONAL_STRING_ID>(informationalStringId), &informationalStrings, &exists);

        assert(SUCCEEDED(hr));

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

        HRESULT hr = this->GetComObject()->HasCharacter(unicodeValue, &exists);

        assert(SUCCEEDED(hr));

        return exists ? true : false;
    }
}
