#ifndef MAP_ITERATOR_H
#define MAP_ITERATOR_H

#include <type_traits>

namespace linq
{
    template<class TIterator, class TProcedure>
    class map_iterator
    {
        TIterator iterator_current;
        TProcedure procedure;

    public:
        map_iterator(TIterator iterator_current, TProcedure &procedure) : iterator_current(iterator_current), procedure(procedure)
        {
        }

        std::result_of_t<TProcedure(decltype(*std::declval<TIterator>()))> operator *()
        {
            return procedure(*iterator_current);
        }

        bool operator !=(const map_iterator &rhs) const
        {
            return iterator_current != rhs.iterator_current;
        }

        map_iterator &operator ++()
        {
            ++iterator_current;
            return *this;
        }
    };
}

#endif // MAP_ITERATOR_H
