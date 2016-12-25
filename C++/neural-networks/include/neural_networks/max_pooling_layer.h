#pragma once

#include "max_pooling_filter.h"

namespace neural_networks
{
    template <class T, class InputSize, class FilterSize, class Stride>
    class max_pooling_layer;

    template <class T,
              std::size_t InputChannels,
              std::size_t InputRows,
              std::size_t InputColumns,
              std::size_t FilterRows,
              std::size_t FilterColumns,
              std::size_t StrideRows,
              std::size_t StrideColumns>
    class max_pooling_layer<T,
                            static_size<InputChannels, InputRows, InputColumns>,
                            static_size<FilterRows, FilterColumns>,
                            static_size<StrideRows, StrideColumns>>
    {
    public:
        using input_type = tensor<T, InputChannels, InputRows, InputColumns>;
        using output_type = tensor<T,
                                   InputChannels,
                                   (InputRows - FilterRows) / StrideRows + 1,
                                   (InputColumns - FilterColumns) / StrideColumns + 1>;
        using context_type = tensor<std::array<std::size_t, 2>,
                                    InputChannels,
                                    (InputRows - FilterRows) / StrideRows + 1,
                                    (InputColumns - FilterColumns) / StrideColumns + 1>;

        void forward(const input_type &input, output_type &output, context_type &context) const
        {
            const auto output_rows = (InputRows - FilterRows) / StrideRows + 1;
            const auto output_columns = (InputColumns - FilterColumns) / StrideColumns + 1;

            for (std::size_t output_channel = 0; output_channel < InputChannels; ++output_channel)
            {
                for (std::size_t output_row = 0; output_row < output_rows; ++output_row)
                {
                    for (std::size_t output_column = 0; output_column < output_columns; ++output_column)
                    {
                        max_pooling_filter<T, FilterRows, FilterColumns>()
                            .forward(input[output_channel],
                                     StrideRows * output_row,
                                     StrideColumns * output_column,
                                     output[output_channel][output_row][output_column],
                                     context[output_channel][output_row][output_column]);
                    }
                }
            }
        }

        void predict(const input_type &input, output_type &output) const
        {
            const auto output_rows = (InputRows - FilterRows) / StrideRows + 1;
            const auto output_columns = (InputColumns - FilterColumns) / StrideColumns + 1;

            for (std::size_t output_channel = 0; output_channel < InputChannels; ++output_channel)
            {
                for (std::size_t output_row = 0; output_row < output_rows; ++output_row)
                {
                    for (std::size_t output_column = 0; output_column < output_columns; ++output_column)
                    {
                        max_pooling_filter<T, FilterRows, FilterColumns>()
                            .predict(input[output_channel],
                                     StrideRows * output_row,
                                     StrideColumns * output_column,
                                     output[output_channel][output_row][output_column]);
                    }
                }
            }
        }

        void backward(const input_type &,
                      const output_type &,
                      const output_type &input_gradient,
                      input_type &output_gradient,
                      const context_type &context) const
        {
            const auto output_rows = (InputRows - FilterRows) / StrideRows + 1;
            const auto output_columns = (InputColumns - FilterColumns) / StrideColumns + 1;

            for (std::size_t output_channel = 0; output_channel < InputChannels; ++output_channel)
            {
                for (std::size_t output_row = 0; output_row < output_rows; ++output_row)
                {
                    for (std::size_t output_column = 0; output_column < output_columns; ++output_column)
                    {
                        max_pooling_filter<T, FilterRows, FilterColumns>()
                            .backward(input_gradient[output_channel][output_row][output_column],
                                      context[output_channel][output_row][output_column],
                                      output_gradient[output_channel]);
                    }
                }
            }
        }
    };
}
