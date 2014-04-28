#ifndef FILTER_ITERATOR_H
#define FILTER_ITERATOR_H

#include <type_traits>

namespace linq
{
    template<class TIterator, class TPredicate>
    class filter_iterator
    {
        TIterator iterator_current;
        TIterator iterator_end;
        TPredicate predicate;
        std::decay_t<element_type<TIterator>> current_value;

        void validate()
        {
            for (; iterator_current != iterator_end; ++iterator_current)
            {
                current_value = *iterator_current;
                if (predicate(current_value))
                {
                    break;
                }
            }
        }

    public:
        filter_iterator(TIterator iterator_begin, TIterator iterator_end, TPredicate predicate) : iterator_current(iterator_begin), iterator_end(iterator_end), predicate(predicate)
        {
            validate();
        }

        std::decay_t<element_type<TIterator>> operator *()
        {
            return current_value;
        }

        bool operator !=(const filter_iterator &rhs) const
        {
            return iterator_current != rhs.iterator_current;
        }

        filter_iterator &operator ++()
        {
            ++iterator_current;
            validate();
            return *this;
        }
    };
}

#endif // FILTER_ITERATOR_H
