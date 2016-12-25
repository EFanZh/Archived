#pragma once

#include "perceptron.h"

namespace neural_networks
{
    template <class Perceptron, std::size_t OutputSize>
    class fully_connected_layer;

    template <class T, std::size_t InputSize, std::size_t OutputSize>
    class fully_connected_layer<perceptron<T, InputSize>, OutputSize>
    {
        using perceptron_type = perceptron<T, InputSize>;

        std::array<perceptron_type, OutputSize> perceptrons;

    public:
        using input_type = tensor<T, InputSize>;
        using output_type = tensor<T, OutputSize>;

        fully_connected_layer() = default;

        fully_connected_layer(const std::array<perceptron_type, OutputSize> &perceptrons) : perceptrons(perceptrons)
        {
        }

        void forward(const input_type &input, output_type &output) const
        {
            for (std::size_t i = 0; i < OutputSize; ++i)
            {
                perceptrons[i].forward(input, output[i]);
            }
        }

        void predict(const input_type &input, output_type &output) const
        {
            forward(input, output);
        }

        void backward(const input_type &input,
                      const output_type &,
                      const output_type &input_gradient,
                      input_type &output_gradient,
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

        void update_weights(const fully_connected_layer &weight_gradient, const T &step_size)
        {
            for (std::size_t i = 0; i < OutputSize; ++i)
            {
                perceptrons[i].update_weights(weight_gradient.perceptrons[i], step_size);
            }
        }
    };
}
