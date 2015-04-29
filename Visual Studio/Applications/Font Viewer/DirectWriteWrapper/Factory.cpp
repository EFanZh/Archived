#include "stdafx.h"
#include "Factory.h"

using namespace System::Linq;

namespace DirectWriteWrapper
{
    Factory::Factory(FactoryType factoryType)
    {
        ::IUnknown *unknown;

        ::DWriteCreateFactory(static_cast<DWRITE_FACTORY_TYPE>(factoryType), __uuidof(IDWriteFactory), &unknown);

        this->SetComObject(unknown);
    }

    FontCollection ^Factory::GetSystemFontCollection(bool checkForUpdates)
    {
        ::IDWriteFontCollection *fontCollection;

        this->GetComObject()->GetSystemFontCollection(&fontCollection, checkForUpdates);

        return gcnew FontCollection(fontCollection);
    }
}
