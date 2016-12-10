#pragma once

#include <array>
#include "StaticSize.h"

namespace NeuralNetworks
{
    namespace Details
    {
        template <class T, class Dimensions>
        struct TensorHelper
        {
            using Type = T;
            using Size = StaticSize<>;
        };

        template <class T, std::size_t FirstDimensions, std::size_t... RestDimensions>
        struct TensorHelper<T, StaticSize<FirstDimensions, RestDimensions...>>
        {
            using Type = std::array<typename TensorHelper<T, StaticSize<RestDimensions...>>::Type, FirstDimensions>;
            using Size = StaticSize<FirstDimensions, RestDimensions...>;
        };

        template <class T, std::size_t N>
        struct TensorDimensionsHelper;

        template <class T, std::size_t DimensionsValue>
        struct TensorDimensionsHelper<std::array<T, DimensionsValue>, 0>
        {
            static const auto dimensions = DimensionsValue;
        };

        template <class T, std::size_t DimensionsValue, std::size_t N>
        struct TensorDimensionsHelper<std::array<T, DimensionsValue>, N>
        {
            static const auto dimensions = TensorDimensionsHelper<T, N - 1>::dimensions;
        };
    }

    template <class T, class Dimensions>
    using Tensor = typename Details::TensorHelper<T, Dimensions>::Type;

    template <class T>
    struct TensorTraits
    {
        using ElementType = T;

        static const auto order = 0;
    };

    template <class T, std::size_t Dimensions>
    struct TensorTraits<std::array<T, Dimensions>>
    {
        using ElementType = typename TensorTraits<T>::ElementType;

        static const auto order = TensorTraits<T>::order + 1;

        template <std::size_t N>
        static const auto dimensions = Details::TensorDimensionsHelper<std::array<T, Dimensions>, N>::dimensions;
    };
}
