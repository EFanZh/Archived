#pragma once

#include "tensor.h"
#include <vector>
#include <random>

namespace neural_networks
{
    class dropout_strategy_half
    {
        std::default_random_engine random_engine;
        std::uniform_int_distribution<unsigned short> distribution{ 0, 1 };

    public:
        static constexpr double effective_probability = 0.5;

        bool operator()()
        {
            return distribution(random_engine) != 0;
        }
    };

    template <class T, std::size_t Dimensions, class Strategy>
    class dropout_layer
    {
        mutable Strategy strategy;

    public:
        using input_type = tensor<T, Dimensions>;
        using output_type = tensor<T, Dimensions>;

        // Effective indexes.
        using context_type = std::vector<std::size_t>;

        dropout_layer() = default;

        dropout_layer(const Strategy &strategy) : strategy(strategy)
        {
        }

        void forward(const input_type &input,
                     output_type &output,
                     std::vector<std::size_t> &context) const
        {
            for (std::size_t i = 0; i < Dimensions; ++i)
            {
                if (strategy())
                {
                    output[i] = input[i];
                    context.emplace_back(i);
                }
            }
        }

        // TODO: Implement super efficient bagging.
        void forward_not_training(const input_type &input, output_type &output) const
        {
            for (std::size_t i = 0; i < Dimensions; ++i)
            {
                output[i] = input[i] * T(Strategy::effective_probability);
            }
        }

        void backward(const input_type &,
                      const output_type &,
                      const output_type &input_gradient,
                      input_type &output_gradient,
                      const context_type &context) const
        {
            for (auto i : context)
            {
                output_gradient[i] = input_gradient[i];
            }
        }
    };
}
