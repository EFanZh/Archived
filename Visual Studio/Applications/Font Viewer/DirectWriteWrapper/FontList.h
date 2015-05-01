#include "ComReadOnlyList.h"
#include "Font.h"

using namespace System;
using namespace System::Collections::Generic;

namespace DirectWriteWrapper
{
    public ref class FontList : ComReadOnlyList < ::IDWriteFontList, Font ^ >
    {
    public:
        FontList(::IDWriteFontList *fontList);

        property Font ^default[int]
        {
            virtual Font ^get(int index) override;
        }

        property int Count
        {
            virtual int get() override;
        }
    };
}
