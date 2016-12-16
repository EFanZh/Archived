#pragma once

#include "Tensor.h"

namespace NeuralNetworks
{
    template <class Input,
              class Weights,
              class BiasType = std::common_type_t<typename TensorTraits<Input>::ElementType,
                                                  typename TensorTraits<Weights>::ElementType>>
    class FullyConnectedLayer
    {
        Weights weights;
        Tensor<BiasType, StaticSize<TensorTraits<Weights>::template dimensions<0>>> bias;

    public:
        using InputType = Input;
        using OutputSize = StaticSize<TensorTraits<Weights>::template dimensions<0>>;

        FullyConnectedLayer(const Weights &weights,
                            const Tensor<BiasType, StaticSize<TensorTraits<Weights>::template dimensions<0>>> &bias)
            : weights(weights), bias(bias)
        {
        }

        template <class Output>
        void Forward(const Input &input, Output &output) const
        {
            for (std::size_t i = 0; i < TensorTraits<Weights>::template dimensions<0>; ++i)
            {
                auto result = typename TensorTraits<Output>::ElementType(bias[i]);

                for (std::size_t j = 0; j < TensorTraits<Input>::template dimensions<0>; ++j)
                {
                    result += input[j] * weights[i][j];
                }

                output[i] = result;
            }
        }

        template <class Gradient, class StepSize, class OutputGradient>
        void Backward(const Input &input, const Gradient &gradient, StepSize stepSize, OutputGradient &outputGradient)
        {
            for (std::size_t i = 0; i < TensorTraits<Weights>::template dimensions<0>; ++i)
            {
                auto outputGradientValue = TensorTraits<OutputGradient>::ElementType(0);

                for (std::size_t j = 0; j < TensorTraits<Weights>::template dimensions<1>; ++j)
                {
                    outputGradientValue += gradient[i] * weights[i][j];
                    weights[i][j] -= gradient[i] * input[i] * stepSize;
                }

                bias[i] -= gradient[i] * stepSize;
                outputGradient[i] = outputGradientValue;
            }
        }
    };
}
