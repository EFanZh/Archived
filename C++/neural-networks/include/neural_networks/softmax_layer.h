#pragma once

#include "tensor.h"
#include <cmath>

namespace neural_networks
{
    class softmax_layer
    {
        template<class InputElementType, std::size_t Size, class OutputType>
        void forward(const tensor<InputElementType, Size> &input, std::size_t expected, OutputType &output) const
        {
            const auto max_input_element = std::max_element(input.cbegin(), input.cend());
            auto sum = OutputType(0);

            for (const auto &element : input)
            {
                sum += std::exp(element - max_input_element);
            }

            output = -std::log(std::exp(input[expected] - max_input_element) / sum);
        }
    };
}
