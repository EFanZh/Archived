#ifndef BASE_H
#define BASE_H

namespace linq
{
    template<class T>
    using iterator_type = decltype(std::begin(std::declval<T>()));

    template<class TIterator>
    using element_type = decltype(*std::declval<TIterator>());
}

#endif // BASE_H
