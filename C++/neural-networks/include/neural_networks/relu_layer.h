#pragma once

#include "tensor.h"

namespace neural_networks
{
    class relu_layer
    {
    public:
        template <class InputElementType, std::size_t... InputDimensions, class OutputElementType>
        void forward(const tensor<InputElementType, InputDimensions...> &input,
                     tensor<OutputElementType, InputDimensions...> &output) const
        {
            for (std::size_t i = 0; i < tensor<InputElementType, InputDimensions...>::element_count; ++i)
            {
                output.as_vector()[i] = std::max<InputElementType>(input.as_vector()[i], 0);
            }
        }

        template <class InputElementType,
                  std::size_t... InputDimensions,
                  class InputGradientElementType,
                  class OutputGradientElementType>
        void backward(const tensor<InputElementType, InputDimensions...> &input,
                      const tensor<InputGradientElementType, InputDimensions...> &input_gradient,
                      tensor<OutputGradientElementType, InputDimensions...> &output_gradient) const
        {
            for (std::size_t i = 0; i < tensor<InputElementType, InputDimensions...>::element_count; ++i)
            {
                if (input.as_vector()[i] >= 0)
                {
                    output_gradient.as_vector()[i] += input_gradient.as_vector()[i];
                }
            }
        }
    };
}
