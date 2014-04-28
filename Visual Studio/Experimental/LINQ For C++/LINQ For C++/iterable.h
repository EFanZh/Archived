#ifndef ITERABLE_H
#define ITERABLE_H

#include "append_iterator.h"
#include "filter_iterator.h"
#include "map_iterator.h"

namespace linq
{
    template<class TIterator>
    class iterable
    {
    protected:
        TIterator iterator_begin;
        TIterator iterator_end;

    public:
        iterable(TIterator iterator_begin, TIterator iterator_end) : iterator_begin(iterator_begin), iterator_end(iterator_end)
        {
        }

        TIterator begin() const
        {
            return iterator_begin;
        }

        TIterator end() const
        {
            return iterator_end;
        }

        template<class T>
        iterable<append_iterator<TIterator, iterator_type<T>>> append(const T &source)
        {
            return iterable<append_iterator<TIterator, iterator_type<T>>>(append_iterator<TIterator, iterator_type<T>>(iterator_begin, iterator_end, std::begin(source)),
                                                                          append_iterator<TIterator, iterator_type<T>>(iterator_end, iterator_end, std::end(source)));
        }

        template<class TSource>
        iterable<append_iterator<TIterator, iterator_type<std::initializer_list<TSource>>>> append(const std::initializer_list<TSource> &source)
        {
            return iterable<append_iterator<TIterator, iterator_type<std::initializer_list<TSource>>>>(append_iterator<TIterator, iterator_type<std::initializer_list<TSource>>>(iterator_begin, iterator_end, std::begin(source)),
                                                                                                       append_iterator<TIterator, iterator_type<std::initializer_list<TSource>>>(iterator_end, iterator_end, std::end(source)));
        }

        template<class TPredicate>
        iterable<filter_iterator<TIterator, TPredicate>> filter(TPredicate predicate) const
        {
            return iterable<filter_iterator<TIterator, TPredicate>>(filter_iterator<TIterator, TPredicate>(iterator_begin, iterator_end, predicate),
                                                                    filter_iterator<TIterator, TPredicate>(iterator_end, iterator_end, predicate));
        }

        template<class TProcedure>
        iterable<map_iterator<TIterator, TProcedure>> map(TProcedure procedure) const
        {
            return iterable<map_iterator<TIterator, TProcedure>>(map_iterator<TIterator, TProcedure>(iterator_begin, procedure),
                                                                 map_iterator<TIterator, TProcedure>(iterator_end, procedure));
        }
    };

    template<class TSource>
    iterable<iterator_type<std::initializer_list<TSource>>> make_iterable(const std::initializer_list<TSource> &source)
    {
        return iterable<iterator_type<std::initializer_list<TSource>>>(std::begin(source), std::end(source));
    }

    template<class T>
    iterable<iterator_type<T>> make_iterable(const T &source)
    {
        return iterable<iterator_type<T>>(std::begin(source), std::end(source));
    }
}

#endif // ITERABLE_H
