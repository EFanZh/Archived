#pragma once

#include "tensor.h"

namespace neural_networks
{
    template <class T, std::size_t... Dimensions>
    class flatten_layer
    {
    public:
        using input_type = tensor<T, Dimensions...>;
        using output_type = tensor<T, input_type::element_count>;

        void forward(const input_type &input, output_type &output) const
        {
            output = input.as_vector();
        }

        void backward(const input_type &,
                      const output_type &,
                      const output_type &input_gradient,
                      input_type &output_gradient) const
        {
            output_gradient.as_vector() = input_gradient;
        }
    };
}
