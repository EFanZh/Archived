#include "stdafx.h"
#include "LocalizedStrings.h"

using namespace System::Text;

namespace DirectWriteWrapper
{
    // LocalizedStrings::LocaleNameList members.

    LocalizedStrings::LocaleNameList::LocaleNameList(::IDWriteLocalizedStrings *localizedStrings) : ComReadOnlyList(localizedStrings)
    {
    }

    String ^LocalizedStrings::LocaleNameList::default::get(int index)
    {
        UINT32 length;

        this->GetComObject()->GetLocaleNameLength(index, &length);

        std::vector<WCHAR> buffer(length + 1);

        this->GetComObject()->GetLocaleName(index, buffer.data(), buffer.size());

        return gcnew String(buffer.data());
    }

    int LocalizedStrings::LocaleNameList::Count::get()
    {
        return this->GetComObject()->GetCount();
    }

    // LocalizedStrings::StringList members.

    LocalizedStrings::StringList::StringList(::IDWriteLocalizedStrings *localizedStrings) : ComReadOnlyList(localizedStrings)
    {
    }

    String ^LocalizedStrings::StringList::default::get(int index)
    {
        UINT32 length;

        this->GetComObject()->GetStringLength(index, &length);

        std::vector<WCHAR> buffer(length + 1);

        this->GetComObject()->GetString(index, buffer.data(), buffer.size());

        return gcnew String(buffer.data());
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

        this->GetComObject()->FindLocaleName(p, &index, &exists);

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

        this->GetComObject()->FindLocaleName(p, &index, &exists);

        return exists ? true : false;
    }

    bool LocalizedStrings::TryGetValue(String ^key, [Out] String ^%value)
    {
        UINT32 index;
        BOOL exists;
        pin_ptr<const WCHAR> p = ::PtrToStringChars(key);

        this->GetComObject()->FindLocaleName(p, &index, &exists);

        if (!exists)
        {
            return false;
        }

        value = stringList[index];

        return true;
    }
}
