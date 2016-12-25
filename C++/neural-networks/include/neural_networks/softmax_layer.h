#pragma once

#include "tensor.h"
#include <cmath>

namespace neural_networks
{
    class softmax_layer
    {
    public:
        using result_type = std::size_t;

        template<class InputElementType, std::size_t InputSize, class OutputType>
        void forward(const tensor<InputElementType, InputSize> &input, std::size_t expected, OutputType &output) const
        {
            const auto max_input_element = *std::max_element(input.cbegin(), input.cend());
            auto sum = OutputType(0);

            for (const auto &element : input)
            {
                sum += std::exp(element - max_input_element);
            }

            output = -std::log(std::exp(input[expected] - max_input_element) / sum);
        }

        template <class InputElementType, std::size_t InputSize, class OutputGradientElementType>
        void backward(const tensor<InputElementType, InputSize> &input,
                      std::size_t expected,
                      tensor<OutputGradientElementType, InputSize> &output_gradient) const
        {

            const auto max_input_element = *std::max_element(input.cbegin(), input.cend());
            auto normalized = tensor<OutputGradientElementType, InputSize>();
            auto sum = OutputGradientElementType(0);

            for (std::size_t i = 0; i < input.template get_dimensions<0>(); ++i)
            {
                normalized[i] = std::exp(input[i] - max_input_element);
                sum += normalized[i];
            }

            for (std::size_t i = 0; i < input.template get_dimensions<0>(); ++i)
            {
                output_gradient[i] = normalized[i] / sum;
            }

            output_gradient[expected] -= 1;
        }

        

        template <class InputElementType, std::size_t InputSize, class OutputGradientElementType>
        void get_gradient(const tensor<InputElementType, InputSize> &input,
                      std::size_t expected,
                      tensor<OutputGradientElementType, InputSize> &output_gradient) const
        {
            backward(input, expected, output_gradient);
        }
    };
}
