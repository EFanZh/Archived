#ifndef APPEND_ITERATOR_H
#define APPEND_ITERATOR_H

#include <type_traits>
#include "base.h"

namespace linq
{
    template<class TIterator1, class TIterator2>
    class append_iterator
    {
        TIterator1 iterator1_current;
        TIterator1 iterator1_end;
        TIterator2 iterator2_current;
        bool is_first;

    public:
        append_iterator(TIterator1 iterator1_begin, TIterator1 iterator1_end, TIterator2 iterator2_begin) :
            iterator1_current(iterator1_begin),
            iterator1_end(iterator1_end),
            iterator2_current(iterator2_begin),
            is_first(iterator1_begin != iterator1_end)
        {
        }

        element_type<TIterator1> operator *()
        {
            if (is_first)
            {
                return *iterator1_current;
            }
            else
            {
                return *iterator2_current;
            }
        }

        bool operator !=(const append_iterator &rhs) const
        {
            if (is_first == rhs.is_first)
            {
                if (is_first)
                {
                    return iterator1_current != rhs.iterator1_current;
                }
                else
                {
                    return iterator2_current != rhs.iterator2_current;
                }
            }
            else
            {
                return true;
            }
        }

        append_iterator &operator ++()
        {
            if (is_first)
            {
                ++iterator1_current;
                if (!(iterator1_current != iterator1_end))
                {
                    is_first = false;
                }
            }
            else
            {
                ++iterator2_current;
            }
            return *this;
        }
    };
}

#endif // APPEND_ITERATOR_H
