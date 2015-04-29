#include "stdafx.h"
#include "FontCollection.h"

namespace DirectWriteWrapper
{
    FontCollection::Enumerator::Enumerator(FontCollection ^fontCollection) : fontCollection(fontCollection)
    {
    }

    FontCollection::Enumerator::~Enumerator()
    {
    }

    FontFamily ^FontCollection::Enumerator::Current::get()
    {
        return fontCollection[index];
    }

    Object ^FontCollection::Enumerator::Current2::get()
    {
        return Current;
    }

    bool FontCollection::Enumerator::MoveNext()
    {
        ++index;

        return index < fontCollection->GetComObject()->GetFontFamilyCount();
    }

    void FontCollection::Enumerator::Reset()
    {
        index = -1;
    }

    FontCollection::FontCollection(::IDWriteFontCollection *fontCollection) : ComObject(fontCollection)
    {
    }

    FontFamily ^FontCollection::default::get(int index)
    {
        ::IDWriteFontFamily *fontFamily;

        this->GetComObject()->GetFontFamily(index, &fontFamily);

        return gcnew FontFamily(fontFamily);
    }

    int FontCollection::Count::get()
    {
        return this->GetComObject()->GetFontFamilyCount();
    }

    IEnumerator<FontFamily ^> ^FontCollection::GetEnumerator()
    {
        return gcnew Enumerator(this);
    }

    System::Collections::IEnumerator ^FontCollection::GetEnumerator2()
    {
        return GetEnumerator();
    }
}
