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

    template <class Strategy>
    class dropout_layer
    {
        mutable Strategy strategy;

    public:
        dropout_layer() = default;

        dropout_layer(const Strategy &strategy) : strategy(strategy)
        {
        }

        template <class T, std::size_t Dimensions>
        void forward(const tensor<T, Dimensions> &input,
                     tensor<T, Dimensions> &output,
                     std::vector<std::size_t> &effective_indexes) const
        {
            for (std::size_t i = 0; i < Dimensions; ++i)
            {
                if (strategy())
                {
                    output[i] = input[i];
                    effective_indexes.emplace_back(i);
                }
            }
        }

        // TODO: Implement super efficient bagging.
        template <class T, std::size_t Dimensions>
        void forward_not_training(const tensor<T, Dimensions> &input, tensor<T, Dimensions> &output) const
        {
            for (std::size_t i = 0; i < Dimensions; ++i)
            {
                output[i] = input[i] * Strategy::effective_probability;
            }
        }

        template <class T, std::size_t Dimensions>
        void backward(const tensor<T, Dimensions> &input_gradient,
                      const std::vector<std::size_t> &input_dropout_indexes,
                      tensor<T, Dimensions> &output_gradient) const
        {
            for (auto i : input_dropout_indexes)
            {
                output_gradient[i] = input_gradient[i];
            }
        }
    };
}
