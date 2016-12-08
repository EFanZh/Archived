#pragma once

#include "StaticSize.h"
#include "Tensor.h"
#include <type_traits>
#include <tuple>

namespace NeuralNetworks
{
    template <class Input,
              class Filter,
              class Stride,
              class BiasType = std::common_type_t<typename TensorTraits<Input>::ElementType,
                                                  typename TensorTraits<Filter>::ElementType>>
    class ConvolutionalLayer
    {
        static const auto outputWidth =
            (MatrixTraits<Input>::Size::width - MatrixTraits<Filter>::Size::width) / Stride::width + 1;

        static const auto outputHeight =
            (MatrixTraits<Input>::Size::height - MatrixTraits<Filter>::Size::height) / Stride::height + 1;

        Filter filter;
        BiasType bias;

        template <class Output, class... OutputIndexes, class... FilterIndexes>
        auto GetValue(const Input &input, Output &output, std::tuple<OutputIndexes...> outputIndexes, FilterIndexes ...filterIndexes, )
        {

        }

        template <std::size_t FirstDimensions,
                  std::size_t... RestDimensions,
                  class Output,
                  class... OutputIndexes,
                  class... FilterIndexes>
        auto GetValue(const Input &input,
                      Output &output,
                      std::tuple<OutputIndexes...> outputIndexes,
                      FilterIndexes... filterIndexes)
        {
        }

        template <std::size_t Dimensions, class Output, class... Indexes>
        void Convolve(const Input &input, Output &output, Indexes... indexes)
        {
            for (auto i = 0; i < Dimensions; ++i)
            {
                auto result = TensorTraits<Output>::ElementType(bias);

                GetValue(input, output, std::make_tuple(indexes...), result);

                GetTensorElement(output, indexes...) = result;
            }
        }

        template <std::size_t FirstDimensions, std::size_t... RestDimensions, class Output, class... Indexes>
        void Convolve(const Input &input, Output &output, Indexes... indexes)
        {
            for (auto i = std::size_t(0); i < FirstDimensions; ++i)
            {
                Convolve<RestDimensions...>(input, output, indexes..., i);
            }
        }

    public:
        using InputType = Input;
        using OutputSize = StaticSize<outputWidth, outputHeight>;

        ConvolutionalLayer(const Filter &filter, BiasType bias) : filter(filter), bias(bias)
        {
        }

        template <class Output>
        void Forward(const Input &input, Output &output) const
        {
            Convolve(input, output);
        }
    };
}
