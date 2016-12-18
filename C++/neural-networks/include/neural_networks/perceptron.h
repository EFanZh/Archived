#pragma once

#include "tensor.h"

namespace neural_networks
{
    template <class T, std::size_t Weights>
    class perceptron
    {
        std::array<T, Weights> weights;
        T bias;

    public:
        perceptron() = default;

        perceptron(const std::array<T, Weights> &weights, const T &bias) : weights(weights), bias(bias)
        {
        }

        template <class InputElementType, std::size_t InputSize, class OutputElementType>
        void forward(const tensor<InputElementType, InputSize> &input, OutputElementType &output) const
        {
            auto result = OutputElementType(bias);

            for (std::size_t i = 0; i < Weights; ++i)
            {
                result += input[i] * weights[i];
            }

            output = result;
        }

        template <class InputElementType, std::size_t InputSize, class InputGradientType, class OutputGradientType>
        void backward(const tensor<InputElementType, InputSize> &input,
                      const InputGradientType &input_gradient,
                      tensor<OutputGradientType, InputSize> &output_gradient,
                      perceptron &output_weight_gradient) const
        {
            for (std::size_t i = 0; i < Weights; ++i)
            {
                output_gradient[i] += input_gradient * weights[i];
                output_weight_gradient.weights[i] += input_gradient * input[i];
            }

            output_weight_gradient.bias += input_gradient;
        }

        template <class StepSizeType>
        void update_weights(const perceptron &weight_gradient, const StepSizeType &stepSize)
        {
            for (size_t i = 0; i < Weights; i++)
            {
                weights[i] -= weight_gradient.weights[i] * stepSize;
            }

            bias -= weight_gradient.bias * stepSize;
        }
    };
}
