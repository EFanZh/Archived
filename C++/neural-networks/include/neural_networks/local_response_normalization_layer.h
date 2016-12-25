#pragma once

#include "local_response_normalization_filter.h"

namespace neural_networks
{
    template <class T, class Size, class FilterType>
    class local_response_normalization_layer;

    template <class T, std::size_t Channels, std::size_t Rows, std::size_t Columns, class FilterType>
    class local_response_normalization_layer<T, static_size<Channels, Rows, Columns>, FilterType>
    {
    public:
        using input_type = tensor<T, Channels, Rows, Columns>;
        using output_type = tensor<T, Channels, Rows, Columns>;
        using context_type = tensor<std::array<typename FilterType::context, Channels>, Rows, Columns>;

        void forward(const input_type &input, output_type &output, context_type &context) const
        {
            for (std::size_t row = 0; row < Rows; ++row)
            {
                for (std::size_t column = 0; column < Columns; ++column)
                {
                    FilterType().forward(input, row, column, output, context[row][column].get_value());
                }
            }
        }

        void predict(const input_type &input, output_type &output) const
        {
            for (std::size_t row = 0; row < Rows; ++row)
            {
                for (std::size_t column = 0; column < Columns; ++column)
                {
                    FilterType().predict(input, row, column, output);
                }
            }
        }

        void backward(const input_type &input,
                      const output_type &output,
                      const output_type &input_gradient,
                      input_type &output_gradient,
                      const context_type &context) const
        {
            for (std::size_t row = 0; row < Rows; ++row)
            {
                for (std::size_t column = 0; column < Columns; ++column)
                {
                    FilterType().backward(input,
                                          row,
                                          column,
                                          output,
                                          input_gradient,
                                          context[row][column].get_value(),
                                          output_gradient);
                }
            }
        }
    };
}
