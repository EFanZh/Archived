#pragma once

#include "ComObject.h"

namespace DirectWriteWrapper
{
    ref class LocalizedStrings : public ComObject < ::IDWriteLocalizedStrings >
    {
    public:
        LocalizedStrings(::IDWriteLocalizedStrings *localizedStrings);
    };
}
