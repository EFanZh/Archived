#pragma once

#include "ComObject.h"

using namespace System::Collections::Generic;

namespace DirectWriteWrapper
{
    template <class TList, class TItem>
    public ref class ComReadOnlyList abstract : ComObject<TList>, IReadOnlyList < TItem >
    {
        ref class Enumerator : IEnumerator < TItem >
        {
            ComReadOnlyList ^list;
            int index = -1;

        public:
            Enumerator(ComReadOnlyList ^list) : list(list)
            {
            }

            ~Enumerator()
            {
            }

            property TItem Current
            {
                virtual TItem get()
                {
                    return list[index];
                }
            }

            property Object ^Current2
            {
                virtual Object ^get() = System::Collections::IEnumerator::Current::get
                {
                    return Current;
                }
            }

            virtual bool MoveNext()
            {
                ++index;

                return index < list->Count;
            }

            virtual void Reset()
            {
                index = -1;
            }
        };

    protected:
        ComReadOnlyList(TList *list) : ComObject(list)
        {
        }

    public:
        property TItem default[int]
        {
            virtual TItem get(int index) abstract;
        }

        property int Count
        {
            virtual int get() abstract;
        }

        virtual IEnumerator<TItem> ^GetEnumerator()
        {
            return gcnew Enumerator(this);
        }

        virtual System::Collections::IEnumerator ^GetEnumerator2() = System::Collections::IEnumerable::GetEnumerator
        {
            return GetEnumerator();
        }
    };
}
