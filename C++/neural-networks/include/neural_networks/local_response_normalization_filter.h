#pragma once

#include "tensor.h"
#include <cmath>
#include <numeric>

namespace neural_networks
{
    struct local_response_normalization_hyper_parameters_alexnet
    {
        static constexpr auto k = std::size_t(2);
        static constexpr auto n = std::size_t(5);
        static constexpr auto alpha = 0.0001;
        static constexpr auto beta = 0.75;
    };

    template <class T, class HyperParameters>
    class local_response_normalization_filter
    {
        static constexpr auto k = HyperParameters::k;
        static constexpr auto n = HyperParameters::n;
        static constexpr auto alpha = T(HyperParameters::alpha);
        static constexpr auto beta = T(HyperParameters::beta);

    public:
        struct context
        {
            T u;
            T v;

            context() = default;

            context(const T &u) : u(u), v(std::pow(u, -beta))
            {
            }
        };

        template <std::size_t InputChannels, std::size_t InputRows, std::size_t InputColumns>
        void forward(const tensor<T, InputChannels, InputRows, InputColumns> &input,
                     std::size_t input_row,
                     std::size_t input_column,
                     tensor<T, InputChannels, InputRows, InputColumns> &output,
                     std::array<context, InputChannels> &output_context) const
        {
            static_assert(n <= InputChannels, "Not ready to handle if n is greater than InputChannels.");

            // Σᵢ = aᵢ² + aⱼ² + …
            //
            // uᵢ = k + α Σᵢ
            //
            // vᵢ = uᵢ⁻ᵝ
            //
            //        aᵢ
            // bᵢ =  ───── = aᵢ vᵢ
            //        uᵢᵝ

            using namespace std;

            // Calculate squared input.

            array<T, InputChannels> squared_input;

            for (size_t i = 0; i < InputChannels; ++i)
            {
                squared_input[i] = input[i][input_row][input_column] * input[i][input_row][input_column];
            }

            // Calcualte initial sigma value.

            auto sigma = T(0);

            for (size_t i = 0; i < n / 2; ++i)
            {
                sigma += squared_input[i];
            }

            // Calculate output values.

            for (size_t i = 0; i < n / 2 + 1; ++i)
            {
                sigma += squared_input[i + n / 2];

                const auto context_value = context(k + alpha * sigma);

                output[i][input_row][input_column] = input[i][input_row][input_column] * context_value.v;
                output_context[i] = context_value;
            }

            for (size_t i = n / 2 + 1; i < InputChannels - n / 2; ++i)
            {
                sigma += squared_input[i + n / 2] - squared_input[i - (n / 2 + 1)];

                const auto context_value = context(k + alpha * sigma);

                output[i][input_row][input_column] = input[i][input_row][input_column] * context_value.v;
                output_context[i] = context_value;
            }

            for (size_t i = InputChannels - n / 2; i < InputChannels; ++i)
            {
                sigma -= squared_input[i - (n / 2 + 1)];

                const auto context_value = context(k + alpha * sigma);

                output[i][input_row][input_column] = input[i][input_row][input_column] * context_value.v;
                output_context[i] = context_value;
            }
        }

        template <std::size_t InputChannels, std::size_t InputRows, std::size_t InputColumns>
        void predict(const tensor<T, InputChannels, InputRows, InputColumns> &input,
                     std::size_t input_row,
                     std::size_t input_column,
                     tensor<T, InputChannels, InputRows, InputColumns> &output) const
        {
            static_assert(n <= InputChannels, "Not ready to handle if n is greater than InputChannels.");

            // Σᵢ = aᵢ² + aⱼ² + …
            //
            // uᵢ = k + α Σᵢ
            //
            // vᵢ = uᵢ⁻ᵝ
            //
            //        aᵢ
            // bᵢ =  ───── = aᵢ vᵢ
            //        uᵢᵝ

            using namespace std;

            // Calculate squared input.

            array<T, InputChannels> squared_input;

            for (size_t i = 0; i < InputChannels; ++i)
            {
                squared_input[i] = input[i][input_row][input_column] * input[i][input_row][input_column];
            }

            // Calcualte initial sigma value.

            auto sigma = T(0);

            for (size_t i = 0; i < n / 2; ++i)
            {
                sigma += squared_input[i];
            }

            // Calculate output values.

            for (size_t i = 0; i < n / 2 + 1; ++i)
            {
                sigma += squared_input[i + n / 2];

                output[i][input_row][input_column] = input[i][input_row][input_column] * context(k + alpha * sigma).v;
            }

            for (size_t i = n / 2 + 1; i < InputChannels - n / 2; ++i)
            {
                sigma += squared_input[i + n / 2] - squared_input[i - (n / 2 + 1)];

                output[i][input_row][input_column] = input[i][input_row][input_column] * context(k + alpha * sigma).v;
            }

            for (size_t i = InputChannels - n / 2; i < InputChannels; ++i)
            {
                sigma -= squared_input[i - (n / 2 + 1)];

                output[i][input_row][input_column] = input[i][input_row][input_column] * context(k + alpha * sigma).v;
            }
        }

        template <std::size_t InputChannels, std::size_t InputRows, std::size_t InputColumns>
        void backward(const tensor<T, InputChannels, InputRows, InputColumns> &input,
                      std::size_t input_row,
                      std::size_t input_column,
                      const tensor<T, InputChannels, InputRows, InputColumns> &output,
                      const tensor<T, InputChannels, InputRows, InputColumns> &input_gradient,
                      const std::array<context, InputChannels> &input_context,
                      tensor<T, InputChannels, InputRows, InputColumns> &output_gradient) const
        {
            // δᵇᵢ = δᵃᵢ vᵢ - 2 α β aᵢ sum(bⱼ δᵃⱼ / uⱼ)

            for (size_t i = 0; i < InputChannels; ++i)
            {
                const auto context_i = input_context[i];
                const auto a_i = input[i][input_row][input_column];
                const auto delta_i = input_gradient[i][input_row][input_column];

                auto sum = T(0);
                const auto j_begin = n / 2 < i ? i - n / 2 : 0;
                const auto j_end = std::min(InputChannels, i + (n / 2 + 1));

                for (std::size_t j = j_begin; j < j_end; ++j)
                {
                    const auto b_j = output[j][input_row][input_column];
                    const auto delta_j = input_gradient[j][input_row][input_column];

                    sum += b_j * delta_j / (k + alpha * input_context[j].u);
                }

                output_gradient[i][input_row][input_column] = context_i.v * delta_i - 2 * alpha * beta * a_i * sum;
            }
        }
    };
}
