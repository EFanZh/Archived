#pragma once

#include "perceptron.h"

namespace neural_networks
{
    template <class Perceptron, std::size_t OutputSize>
    class fully_connected_layer;

    template <class PerceptronWeightType, std::size_t InputSize, std::size_t OutputSize>
    class fully_connected_layer<perceptron<PerceptronWeightType, InputSize>, OutputSize>
    {
        using perceptron_type = perceptron<PerceptronWeightType, InputSize>;

        std::array<perceptron_type, OutputSize> perceptrons;

    public:
        fully_connected_layer() = default;

        fully_connected_layer(const std::array<perceptron_type, OutputSize> &perceptrons) : perceptrons(perceptrons)
        {
        }

        template <class InputElementType, class OutputElementType>
        void forward(const tensor<InputElementType, InputSize> &input,
                     tensor<OutputElementType, OutputSize> &output) const
        {
            for (std::size_t i = 0; i < OutputSize; ++i)
            {
                perceptrons[i].forward(input, output[i]);
            }
        }

        template <class InputElementType, class InputGradientElementType, class OutputGradientElementType>
        void backward(const tensor<InputElementType, InputSize> &input,
                      const tensor<InputGradientElementType, OutputSize> &input_gradient,
                      tensor<OutputGradientElementType, InputSize> &output_gradient,
                      fully_connected_layer &output_weight_gradient) const
        {
            for (std::size_t i = 0; i < OutputSize; ++i)
            {
                perceptrons[i].backward(input,
                                        input_gradient[i],
                                        output_gradient,
                                        output_weight_gradient.perceptrons[i]);
            }
        }

        template <class StepSizeType>
        void update_weights(const fully_connected_layer &weight_gradient, const StepSizeType &step_size)
        {
            for (std::size_t i = 0; i < OutputSize; ++i)
            {
                perceptrons[i].update_weights(weight_gradient.perceptrons[i], step_size);
            }
        }
    };
}
