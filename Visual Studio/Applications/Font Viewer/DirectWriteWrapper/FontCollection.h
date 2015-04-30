#pragma once

#include "ComReadOnlyList.h"
#include "FontFamily.h"

using namespace System;
using namespace System::Collections::Generic;

namespace DirectWriteWrapper
{
    public ref class FontCollection : ComReadOnlyList < ::IDWriteFontCollection, FontFamily ^ >
    {
    public:
        FontCollection(::IDWriteFontCollection *fontCollection);

        property FontFamily ^default[int]
        {
            virtual FontFamily ^get(int index) override;
        }

        property int Count
        {
            virtual int get() override;
        }
    };
}
