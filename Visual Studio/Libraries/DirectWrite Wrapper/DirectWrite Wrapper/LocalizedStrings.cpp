#include "stdafx.h"
#include "LocalizedStrings.h"
#include "Buffer.h"

using namespace System::Text;

namespace DirectWriteWrapper
{
    // LocalizedStrings::LocaleNameList members.

    LocalizedStrings::LocaleNameList::LocaleNameList(::IDWriteLocalizedStrings *localizedStrings) : ComReadOnlyList(localizedStrings)
    {
        // TODO:
        localizedStrings->AddRef();
    }

    String ^LocalizedStrings::LocaleNameList::default::get(int index)
    {
        UINT32 length;

        HRESULT hr = this->GetComObject()->GetLocaleNameLength(index, &length);

        assert(SUCCEEDED(hr));

        Buffer<WCHAR, UINT32> buffer(length + 1);

        hr = this->GetComObject()->GetLocaleName(index, buffer.GetBuffer(), buffer.GetSize());

        assert(SUCCEEDED(hr));

        return gcnew String(buffer.GetBuffer());
    }

    int LocalizedStrings::LocaleNameList::Count::get()
    {
        return this->GetComObject()->GetCount();
    }

    // LocalizedStrings::StringList members.

    LocalizedStrings::StringList::StringList(::IDWriteLocalizedStrings *localizedStrings) : ComReadOnlyList(localizedStrings)
    {
        // TODO:
        localizedStrings->AddRef();
    }

    String ^LocalizedStrings::StringList::default::get(int index)
    {
        UINT32 length;

        HRESULT hr = this->GetComObject()->GetStringLength(index, &length);

        assert(SUCCEEDED(hr));

        Buffer<WCHAR, UINT32> buffer(length + 1);

        hr = this->GetComObject()->GetString(index, buffer.GetBuffer(), buffer.GetSize());

        assert(SUCCEEDED(hr));

        return gcnew String(buffer.GetBuffer());
    }

    int LocalizedStrings::StringList::Count::get()
    {
        return this->GetComObject()->GetCount();
    }

    // LocalizedStrings members.

    LocalizedStrings::LocalizedStrings(::IDWriteLocalizedStrings *localizedStrings) :
        ComReadOnlyList(localizedStrings),
        localeNameList(gcnew LocaleNameList(localizedStrings)),
        stringList(gcnew StringList(localizedStrings))
    {
    }

    KeyValuePair<String ^, String ^> LocalizedStrings::default::get(int index)
    {
        return KeyValuePair<String ^, String ^>(localeNameList[index], stringList[index]);
    }

    String ^LocalizedStrings::default::get(String ^key)
    {
        UINT32 index;
        BOOL exists;
        pin_ptr<const WCHAR> p = ::PtrToStringChars(key);
        HRESULT hr = this->GetComObject()->FindLocaleName(p, &index, &exists);

        assert(SUCCEEDED(hr));

        return stringList[index];
    }

    int LocalizedStrings::Count::get()
    {
        return this->GetComObject()->GetCount();
    }

    IEnumerable<String ^> ^LocalizedStrings::Keys::get()
    {
        return localeNameList;
    }

    IEnumerable<String ^> ^LocalizedStrings::Values::get()
    {
        return stringList;
    }

    bool LocalizedStrings::ContainsKey(String ^key)
    {
        UINT32 index;
        BOOL exists;
        pin_ptr<const WCHAR> p = ::PtrToStringChars(key);
        HRESULT hr = this->GetComObject()->FindLocaleName(p, &index, &exists);

        assert(SUCCEEDED(hr));

        return exists ? true : false;
    }

    bool LocalizedStrings::TryGetValue(String ^key, [Out] String ^%value)
    {
        UINT32 index;
        BOOL exists;
        pin_ptr<const WCHAR> p = ::PtrToStringChars(key);
        HRESULT hr = this->GetComObject()->FindLocaleName(p, &index, &exists);

        assert(SUCCEEDED(hr));

        if (!exists)
        {
            return false;
        }

        value = stringList[index];

        return true;
    }
}
