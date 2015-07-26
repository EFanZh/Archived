#pragma once

#include "ComReadOnlyList.h"

using namespace System;
using namespace System::Collections::Generic;
using namespace System::Runtime::InteropServices;

namespace DirectWriteWrapper
{
    public ref class LocalizedStrings : ComReadOnlyList<::IDWriteLocalizedStrings, KeyValuePair<String ^, String ^>>, IReadOnlyDictionary<String ^, String ^>
    {
        ref class LocaleNameList : ComReadOnlyList<::IDWriteLocalizedStrings, String ^>
        {
        public:
            LocaleNameList(::IDWriteLocalizedStrings *localizedStrings);

            property String ^default[int]
            {
                virtual String ^get(int index) override;
            }

                property int Count
            {
                virtual int get() override;
            }
        };

        ref class StringList : ComReadOnlyList<::IDWriteLocalizedStrings, String ^>
        {
        public:
            StringList(::IDWriteLocalizedStrings *localizedStrings);

            property String ^default[int]
            {
                virtual String ^get(int index) override;
            }

                property int Count
            {
                virtual int get() override;
            }
        };

        LocaleNameList ^localeNameList;
        StringList ^stringList;

    internal:
        LocalizedStrings(::IDWriteLocalizedStrings *localizedStrings);

    public:
        property String ^default[String ^]
        {
            virtual String ^get(String ^key);
        }

            property KeyValuePair<String ^, String ^> default[int ^]
        {
            virtual KeyValuePair<String ^, String ^> get(int index) override;
        }

            property int Count
        {
            virtual int get() override;
        }

        property IEnumerable<String ^> ^Keys
        {
            virtual IEnumerable<String ^> ^get();
        }

        property IEnumerable<String ^> ^Values
        {
            virtual IEnumerable<String ^> ^get();
        }

        virtual bool ContainsKey(String ^key);
        virtual bool TryGetValue(String ^key, [Out] String ^%value);
    };
}
