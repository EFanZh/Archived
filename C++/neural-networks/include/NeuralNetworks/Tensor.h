#pragma once

#include <array>
#include "StaticSize.h"

namespace NeuralNetworks
{
    namespace Details
    {
        template <class T, std::size_t... Dimensions>
        struct TensorHelper
        {
            using ElementType = T;
        };

        template <class T, std::size_t FirstOrderDimensions, std::size_t... RestOrderDimensions>
        struct TensorHelper<T, FirstOrderDimensions, RestOrderDimensions...>
        {
            using ElementType =
                std::array<typename TensorHelper<T, RestOrderDimensions...>::ElementType, FirstOrderDimensions>;
        };
    }

    template <class T, std::size_t... Dimensions>
    using Tensor = typename Details::TensorHelper<T, Dimensions...>::Type;

    template <class T>
    struct TensorTraits
    {
        using ElementType = T;

        static const auto Order = 0;
        static const auto Dimension = 0;
    };

    template <class T, std::size_t DimensionsValue>
    struct TensorTraits<std::array<T, DimensionsValue>>
    {
        using ElementType = typename TensorTraits<T>::ElementType;

        static const auto Order = TensorTraits<T>::Order + 1;
        static const auto Dimensions = DimensionsValue;
    };

    template <class T, std::size_t N>
    using Vector = Tensor<T, N>;

    template <class T, std::size_t Rows, std::size_t Columns>
    using Matrix = Tensor<T, Rows, Columns>;

    template <class T>
    decltype(auto) GetTensorElement(T &tensor)
    {
        return tensor;
    }

    template <class T, class... Indexes>
    decltype(auto) GetTensorElement(T &tensor, std::size_t firstIndex, Indexes... restIndexes)
    {
        return GetTensorElement(tensor[firstIndex], restIndexes...);
    }
}
