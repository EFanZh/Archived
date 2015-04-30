#include "Stdafx.h"
#include "FontFamily.h"

namespace DirectWriteWrapper
{
    FontFamily::FontFamily(::IUnknown *fontFamily) : ComObject(fontFamily)
    {
    }

    LocalizedStrings ^FontFamily::GetFamilyNames()
    {
        ::IDWriteLocalizedStrings *localizedStrings;

        this->GetComObject()->GetFamilyNames(&localizedStrings);

        return gcnew LocalizedStrings(localizedStrings);
    }
}
