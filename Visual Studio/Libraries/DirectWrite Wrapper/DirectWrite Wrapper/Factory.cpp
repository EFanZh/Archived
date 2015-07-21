#include "stdafx.h"
#include "Factory.h"

using namespace System::Linq;

namespace DirectWriteWrapper
{
    Factory::Factory(FactoryType factoryType)
    {
        ::IUnknown *unknown;

        HRESULT hr = ::DWriteCreateFactory(static_cast<DWRITE_FACTORY_TYPE>(factoryType), __uuidof(IDWriteFactory), &unknown);

        assert(SUCCEEDED(hr));
        
        this->SetComObject(unknown);
    }

    FontCollection ^Factory::GetSystemFontCollection(bool checkForUpdates)
    {
        ::IDWriteFontCollection *fontCollection;

        HRESULT hr = this->GetComObject()->GetSystemFontCollection(&fontCollection, checkForUpdates);

        assert(SUCCEEDED(hr));

        return gcnew FontCollection(fontCollection);
    }
}
