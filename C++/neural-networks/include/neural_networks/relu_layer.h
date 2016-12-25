#pragma once

#include "tensor.h"

namespace neural_networks
{
    template <class T, std::size_t... Dimensions>
    class relu_layer
    {
    public:
        using input_type = tensor<T, Dimensions...>;
        using output_type = tensor<T, Dimensions...>;

        void forward(const input_type &input, output_type &output) const
        {
            for (std::size_t i = 0; i < input_type::element_count; ++i)
            {
                output.as_vector()[i] = std::max<T>(input.as_vector()[i], 0);
            }
        }

        void backward(const input_type &input,
                      const output_type &,
                      const output_type &input_gradient,
                      input_type &output_gradient) const
        {
            for (std::size_t i = 0; i < input_type::element_count; ++i)
            {
                if (input.as_vector()[i] >= 0)
                {
                    output_gradient.as_vector()[i] += input_gradient.as_vector()[i];
                }
            }
        }
    };
}
