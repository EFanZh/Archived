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

        this->GetComObject()->GetFont(index, &font);

        return gcnew Font(font);
    }

    int FontList::Count::get()
    {
        return this->GetComObject()->GetFontCount();
    }
}
