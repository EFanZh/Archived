#pragma once

#include <algorithm>
#include <array>
#include "static_size.h"

namespace neural_networks
{
    template <class T, std::size_t... Dimensions>
    class tensor
    {
        T value;

    public:
        using size_type = std::size_t;

        using size = static_size<>;
        using element_type = T;
        using element_iterator = element_type *;
        using const_element_iterator = const element_type *;

        static const auto element_count = size_type(1);

        tensor() = default;

        tensor(const T &value) : value(value)
        {
        }

        operator T &()
        {
            return value;
        }

        operator const T &() const
        {
            return value;
        }

        template <class InputElementType>
        tensor &operator=(InputElementType &&other)
        {
            value = std::forward<InputElementType>(other);

            return *this;
        }

        tensor<T, element_count> &as_vector()
        {
            return reinterpret_cast<tensor<T, element_count> &>(*this);
        }

        const tensor<T, element_count> &as_vector() const
        {
            return reinterpret_cast<const tensor<T, element_count> &>(*this);
        }

        element_iterator begin_element()
        {
            return reinterpret_cast<element_iterator>(this);
        }

        const_element_iterator cbegin_element() const
        {
            return reinterpret_cast<const_element_iterator>(this);
        }

        element_iterator end_element()
        {
            return begin_element() + element_count;
        }

        const_element_iterator cend_element() const
        {
            return cbegin_element() + element_count;
        }

        T &get_value()
        {
            return value;
        }

        const T &get_value() const
        {
            return value;
        }
    };

    template <class T, std::size_t FirstDimensions, std::size_t... RestDimensions>
    class tensor<T, FirstDimensions, RestDimensions...>
    {
    public:
        using value_type = tensor<T, RestDimensions...>;
        using reference = value_type &;
        using const_reference = const value_type &;

        value_type __values[FirstDimensions];

        using iterator = decltype(std::begin(__values));
        using const_iterator = decltype(std::cbegin(__values));
        using size_type = typename value_type::size_type;

        using size = static_size<FirstDimensions, RestDimensions...>;
        using element_type = typename value_type::element_type;
        using element_iterator = element_type *;
        using const_element_iterator = const element_type *;

        static const auto element_count = value_type::element_count * FirstDimensions;

        reference operator[](size_type index)
        {
            return __values[index];
        }

        const_reference operator[](size_type index) const
        {
            return __values[index];
        }

        template <std::size_t N>
        static constexpr size_type get_dimensions()
        {
            return size::template get_value<N>();
        }

        iterator begin()
        {
            return std::begin(__values);
        }

        const_iterator cbegin() const
        {
            return std::cbegin(__values);
        }

        iterator end()
        {
            return std::end(__values);
        }

        const_iterator cend() const
        {
            return std::cend(__values);
        }

        tensor<T, element_count> &as_vector()
        {
            return reinterpret_cast<tensor<T, element_count> &>(*this);
        }

        const tensor<T, element_count> &as_vector() const
        {
            return reinterpret_cast<const tensor<T, element_count> &>(*this);
        }

        element_iterator begin_element()
        {
            return reinterpret_cast<element_iterator>(this);
        }

        const_element_iterator cbegin_element() const
        {
            return reinterpret_cast<const_element_iterator>(this);
        }

        element_iterator end_element()
        {
            return begin_element() + element_count;
        }

        const_element_iterator cend_element() const
        {
            return cbegin_element() + element_count;
        }
    };

    template <class T>
    bool operator==(const tensor<T> &lhs, const tensor<T> &rhs)
    {
        return lhs.get_value() == rhs.get_value();
    }

    template <class T, std::size_t FirstDimensions, std::size_t... RestDimensions>
    bool operator==(const tensor<T, FirstDimensions, RestDimensions...> &lhs,
                    const tensor<T, FirstDimensions, RestDimensions...> &rhs)
    {
        return std::equal(lhs.cbegin(), lhs.cend(), rhs.cbegin(), rhs.cend());
    }
}
