#pragma once

#include "ComObject.h"
#include "FactoryType.h"
#include "FontCollection.h"

using namespace System;
using namespace System::Collections::Generic;

namespace DirectWriteWrapper
{
    public ref class Factory : ComObject<::IDWriteFactory>
    {
    public:
        Factory(FactoryType factoryType);

        FontCollection ^GetSystemFontCollection(bool checkForUpdates);
    };
}
