#pragma once

#include "FontFamily.h"

using namespace System;
using namespace System::Collections::Generic;

namespace DirectWriteWrapper
{
    public ref class FontCollection : public ComObject<::IDWriteFontCollection>, public IReadOnlyList < FontFamily ^ >
    {
        ref class Enumerator : public IEnumerator < FontFamily ^ >
        {
            FontCollection ^fontCollection;
            UINT32 index = -1;

        public:
            Enumerator(FontCollection ^fontCollection);
            ~Enumerator();

            property FontFamily ^Current
            {
                virtual FontFamily ^get();
            }

            property Object ^Current2
            {
                virtual Object ^get() = System::Collections::IEnumerator::Current::get;
            }

            virtual bool MoveNext();
            virtual void Reset();
        };

    public:
        FontCollection(::IDWriteFontCollection *fontCollection);

        property FontFamily ^default[int]
        {
            virtual FontFamily ^get(int index);
        }

        property int Count
        {
            virtual int get();
        }

        virtual IEnumerator<FontFamily ^> ^GetEnumerator();
        virtual System::Collections::IEnumerator ^GetEnumerator2() = System::Collections::IEnumerable::GetEnumerator;
    };
}
