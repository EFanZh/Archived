#pragma once

#include <cstddef>

namespace neural_networks
{
    namespace details
    {
        template <class T, std::size_t N, T FirstValue, T... RestValues>
        struct static_size_helper
        {
            static const auto value = static_size_helper<T, N - 1, RestValues...>::value;
        };

        template <class T, T FirstValue, T... RestValues>
        struct static_size_helper<T, 0, FirstValue, RestValues...>
        {
            static const auto value = FirstValue;
        };
    }

    template <std::size_t... Values>
    struct static_size
    {
        using value_type = std::size_t;

        static const auto dimensions = sizeof...(Values);

        template <std::size_t N>
        static constexpr value_type get_value()
        {
            static_assert(N < dimensions, "N shall be less than dimensions.");

            return details::static_size_helper<value_type, N, Values...>::value;
        }
    };
}
