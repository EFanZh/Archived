#include "stdafx.h"
#include "LocalizedStrings.h"

namespace DirectWriteWrapper
{
    LocalizedStrings::LocalizedStrings(::IDWriteLocalizedStrings *localizedStrings) : ComObject(localizedStrings)
    {
    }
}
