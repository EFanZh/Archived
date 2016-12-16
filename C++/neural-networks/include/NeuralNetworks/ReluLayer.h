#pragma once

#include "Tensor.h"

namespace NeuralNetworks
{
    class ReluLayer
    {
        template <class Input, class Output>
        void Forward(const Input &input, Output &output)
        {
            for (std::size_t i = 0; i < TensorTraits<Input>::template dimensions<0>; ++i)
            {
                for (std::size_t j = 0; j < TensorTraits<Input>::template dimensions<1>; ++j)
                {
                    output[i][j] = max(0, input[i][j]);
                }
            }
        }

        template <class Input, class Gradient, class StepSize, class OutputGradient>
        void Backward(const Input &input,
                      const Gradient &gradient,
                      [[maybe_unused]] StepSize stepSize,
                      OutputGradient &outputGradient)
        {
            for (std::size_t i = 0; i < TensorTraits<Input>::template dimensions<0>; ++i)
            {
                for (std::size_t j = 0; j < TensorTraits<Input>::template dimensions<1>; ++j)
                {
                    outputGradient[i][j] = input[i][j] > 0 ? gradient[i][j] : 0;
                }
            }
        }
    };
}
