#include "Stdafx.h"
#include "FontList.h"

namespace DirectWriteWrapper
{
    FontList::FontList(::IDWriteFontList *fontList) : ComReadOnlyList(fontList)
    {
    }

    Font ^FontList::default::get(int index)
    {
        ::IDWriteFont *font;

        HRESULT hr = this->GetComObject()->GetFont(index, &font);

        assert(SUCCEEDED(hr));

        return gcnew Font(font);
    }

    int FontList::Count::get()
    {
        return this->GetComObject()->GetFontCount();
    }
}
