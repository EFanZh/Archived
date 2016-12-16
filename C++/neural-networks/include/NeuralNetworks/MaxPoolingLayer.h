#pragma once

#include <limits>
#include "Tensor.h"

namespace NeuralNetworks
{
    template <class Input, class FilterSize, class Stride>
    class MaxPoolingLayer
    {
        template <std::size_t N>
        struct OutputSizeHelper
        {
            static const auto value = (TensorTraits<Input>::template dimensions<N> - FilterSize::template value<N>) /
                    Stride::template value<N> +
                1;
        };

    public:
        using InputType = Input;
        using OutputSize =
            StaticSize<OutputSizeHelper<0>::value, OutputSizeHelper<1>::value, OutputSizeHelper<2>::value>;

        template <class Output>
        void Forward(const Input &input, const Output &output) const
        {
            for (std::size_t outputRow = 0; outputRow < OutputSize::template value<0>; ++outputRow)
            {
                for (std::size_t outputColumn = 0; outputColumn < OutputSize::template value<1>; ++outputColumn)
                {
                    for (std::size_t outputChannel = 0; outputChannel < OutputSize::template value<2>; ++outputChannel)
                    {
                        auto value = std::numeric_limits<typename TensorTraits<Output>::ElementType>::min();

                        for (std::size_t filterRow = 0; filterRow < FilterSize::template value<0>; ++filterRow)
                        {
                            for (std::size_t filterColumn = 0; filterColumn < FilterSize::template value<1>;
                                 ++filterColumn)
                            {
                                for (std::size_t filterChannel = 0; filterChannel < FilterSize::template value<2>;
                                     ++filterChannel)
                                {
                                    value = max(value,
                                                input[Stride::template value<0> * outputRow + filterRow]
                                                     [Stride::template value<1> * outputColumn + filterColumn]
                                                     [Stride::template value<2> * outputChannel + filterChannel]);
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
            for (std::size_t outputRow = 0; outputRow < OutputSize::template value<0>; ++outputRow)
            {
                for (std::size_t outputColumn = 0; outputColumn < OutputSize::template value<1>; ++outputColumn)
                {
                    for (std::size_t outputChannel = 0; outputChannel < OutputSize::template value<2>; ++outputChannel)
                    {
                        for (std::size_t filterRow = 0; filterRow < FilterSize::template value<0>; ++filterRow)
                        {
                            for (std::size_t filterColumn = 0; filterColumn < FilterSize::template value<1>;
                                 ++filterColumn)
                            {
                                for (std::size_t filterChannel = 0; filterChannel < FilterSize::template value<2>;
                                     ++filterChannel)
                                {
                                }
                            }
                        }
                    }
                }
            }
        }
    };
}
