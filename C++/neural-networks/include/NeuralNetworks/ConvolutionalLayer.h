#pragma once

#include "Tensor.h"

namespace NeuralNetworks
{
    template <class Input,
              class Filter,
              class Stride,
              class BiasType = std::common_type_t<typename TensorTraits<Input>::ElementType,
                                                  typename TensorTraits<Filter>::ElementType>>
    class ConvolutionalLayer
    {
        // // This code crashes Clang Backend in Qt Creator.
        //
        // template <std::size_t N>
        // static const auto outputSize =
        //     (TensorTraits<Input>::template dimensions<N> - TensorTraits<Filter>::template dimensions<N>) / Stride::template value<N> + 1;

        template <std::size_t N>
        struct OutputSizeHelper
        {
            static const auto value =
                (TensorTraits<Input>::template dimensions<N> - TensorTraits<Filter>::template dimensions<N>) /
                    Stride::template value<N> +
                1;
        };

        Filter filter;
        BiasType bias;

    public:
        using InputType = Input;
        using OutputSize =
            StaticSize<OutputSizeHelper<0>::value, OutputSizeHelper<1>::value, OutputSizeHelper<2>::value>;

        ConvolutionalLayer(const Filter &filter, BiasType bias) : filter(filter), bias(bias)
        {
        }

        template <class Output>
        void Forward(const Input &input, Output &output) const
        {
            for (std::size_t outputRow = 0; outputRow < OutputSize::template value<0>; ++outputRow)
            {
                for (std::size_t outputColumn = 0; outputColumn < OutputSize::template value<1>; ++outputColumn)
                {
                    for (std::size_t outputChannel = 0; outputChannel < OutputSize::template value<2>; ++outputChannel)
                    {
                        auto value = typename TensorTraits<Output>::ElementType(bias);

                        for (std::size_t filterRow = 0; filterRow < TensorTraits<Filter>::template dimensions<0>;
                             ++filterRow)
                        {
                            for (std::size_t filterColumn = 0;
                                 filterColumn < TensorTraits<Filter>::template dimensions<1>;
                                 ++filterColumn)
                            {
                                for (std::size_t filterChannel = 0;
                                     filterChannel < TensorTraits<Filter>::template dimensions<2>;
                                     ++filterChannel)
                                {
                                    value += input[Stride::template value<0> * outputRow + filterRow]
                                                  [Stride::template value<1> * outputColumn + filterColumn]
                                                  [Stride::template value<2> * outputChannel + filterChannel] *
                                        filter[filterRow][filterColumn][filterChannel];
                                }
                            }
                        }

                        output[outputRow][outputColumn][outputChannel] = value;
                    }
                }
            }
        }

        template <class Gradient, class StepSize, class OutputGradient>
        void Backward(const Input &input, const Gradient &gradient, StepSize stepSize, OutputGradient &outputGradient)
        {
            Filter filterGradient = {};
            auto biasGradient = BiasType(0);

            for (std::size_t outputRow = 0; outputRow < OutputSize::template value<0>; ++outputRow)
            {
                for (std::size_t outputColumn = 0; outputColumn < OutputSize::template value<1>; ++outputColumn)
                {
                    for (std::size_t outputChannel = 0; outputChannel < OutputSize::template value<2>; ++outputChannel)
                    {
                        for (std::size_t filterRow = 0; filterRow < TensorTraits<Filter>::template dimensions<0>;
                             ++filterRow)
                        {
                            for (std::size_t filterColumn = 0;
                                 filterColumn < TensorTraits<Filter>::template dimensions<1>;
                                 ++filterColumn)
                            {
                                for (std::size_t filterChannel = 0;
                                     filterChannel < TensorTraits<Filter>::template dimensions<2>;
                                     ++filterChannel)
                                {
                                    outputGradient[Stride::template value<0> * outputRow + filterRow]
                                                  [Stride::template value<1> * outputColumn + filterColumn]
                                                  [Stride::template value<2> * outputChannel + filterChannel] +=
                                        gradient[outputRow][outputColumn][outputChannel] *
                                        filter[filterRow][filterColumn][filterChannel];

                                    filterGradient[filterRow][filterColumn][filterChannel] +=
                                        gradient[outputRow][outputColumn][outputChannel] *
                                        input[Stride::template value<0> * outputRow + filterRow]
                                             [Stride::template value<1> * outputColumn + filterColumn]
                                             [Stride::template value<2> * outputChannel + filterChannel];
                                }
                            }
                        }

                        biasGradient += gradient[outputRow][outputColumn][outputChannel];
                    }
                }
            }

            for (std::size_t filterRow = 0; filterRow < TensorTraits<Filter>::template dimensions<0>; ++filterRow)
            {
                for (std::size_t filterColumn = 0; filterColumn < TensorTraits<Filter>::template dimensions<1>;
                     ++filterColumn)
                {
                    for (std::size_t filterChannel = 0; filterChannel < TensorTraits<Filter>::template dimensions<2>;
                         ++filterChannel)
                    {
                        filter[filterRow][filterColumn][filterChannel] -=
                            filterGradient[filterRow][filterColumn][filterChannel] * stepSize;
                    }
                }
            }

            bias -= biasGradient * stepSize;
        }
    };
}
