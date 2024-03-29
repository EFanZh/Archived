#include "Stdafx.h"
#include "FontCollection.h"

namespace DirectWriteWrapper
{
    FontCollection::FontCollection(::IDWriteFontCollection *fontCollection) : ComReadOnlyList(fontCollection)
    {
    }

    FontFamily ^FontCollection::default::get(int index)
    {
        ::IDWriteFontFamily *fontFamily;

        HRESULT hr = this->GetComObject()->GetFontFamily(index, &fontFamily);

        assert(SUCCEEDED(hr));

        return gcnew FontFamily(fontFamily);
    }

    int FontCollection::Count::get()
    {
        return this->GetComObject()->GetFontFamilyCount();
    }
}
