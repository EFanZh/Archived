#pragma once

#include <cstddef>

namespace NeuralNetworks
{
    namespace Details
    {
        template <class T, std::size_t N, T FirstValue, T... RestValues>
        struct GetValueHelper
        {
            static const auto value = GetValueHelper<T, N - 1, RestValues...>::value;
        };

        template <class T, T FirstValue, T... RestValues>
        struct GetValueHelper<T, 0, FirstValue, RestValues...>
        {
            static const auto value = FirstValue;
        };
    }

    template <std::size_t... Values>
    struct StaticSize
    {
        template <std::size_t N>
        static const auto value = Details::GetValueHelper<std::size_t, N, Values...>::value;
    };
}
