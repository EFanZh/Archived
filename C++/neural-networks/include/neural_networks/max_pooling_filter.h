#pragma once

#include "tensor.h"
#include <limits>

namespace neural_networks
{
    template <class T, std::size_t Rows, std::size_t Columns>
    class max_pooling_filter
    {
    public:
        template <class InputElementType, std::size_t InputRows, std::size_t InputColumns, class OutputType>
        void forward(const tensor<InputElementType, InputRows, InputColumns> &input,
                     std::size_t input_row_start,
                     std::size_t input_column_start,
                     OutputType &output,
                     std::array<std::size_t, 2> &output_max_indexes) const
        {
            auto result = std::numeric_limits<OutputType>::min();
            auto max_indexes_result = std::array<std::size_t, 2>();

            for (std::size_t row = input_row_start; row < input_row_start + Rows; ++row)
            {
                for (std::size_t column = input_column_start; column < input_column_start + Columns; ++column)
                {
                    if (input[row][column] > result)
                    {
                        result = input[row][column];

                        max_indexes_result[0] = row;
                        max_indexes_result[1] = column;
                    }
                }
            }

            output = result;
            output_max_indexes = max_indexes_result;
        }

        template <class InputElementType, std::size_t InputRows, std::size_t InputColumns, class OutputType>
        void predict(const tensor<InputElementType, InputRows, InputColumns> &input,
                     std::size_t input_row_start,
                     std::size_t input_column_start,
                     OutputType &output) const
        {
            auto result = std::numeric_limits<OutputType>::min();

            for (std::size_t row = input_row_start; row < input_row_start + Rows; ++row)
            {
                for (std::size_t column = input_column_start; column < input_column_start + Columns; ++column)
                {
                    if (input[row][column] > result)
                    {
                        result = input[row][column];
                    }
                }
            }

            output = result;
        }

        template <std::size_t InputRows,
                  std::size_t InputColumns,
                  class OutputGradientElementType,
                  class OutputGradientType>
        void backward(const OutputGradientElementType &input_gradient,
                      const std::array<std::size_t, 2> &output_max_indexes,
                      tensor<OutputGradientType, InputRows, InputColumns> &output_gradient) const
        {
            output_gradient[output_max_indexes[0]][output_max_indexes[1]] += input_gradient;
        }
    };
}
