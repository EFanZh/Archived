#pragma once

#include "max_pooling_filter.h"

namespace neural_networks
{
    template <class FilterSize, class Stride>
    class max_pooling_layer;

    template <std::size_t FilterRows, std::size_t FilterColumns, std::size_t StrideRows, std::size_t StrideColumns>
    class max_pooling_layer<static_size<FilterRows, FilterColumns>, static_size<StrideRows, StrideColumns>>
    {
    public:
        template <std::size_t OutputChannels, std::size_t OutputRows, std::size_t OutputColumns>
        struct context
        {
            tensor<std::array<std::size_t, 2>, OutputChannels, OutputRows, OutputColumns> max_indexes;
        };

        template <class ElementType, std::size_t InputChannels, std::size_t InputRows, std::size_t InputColumns>
        void forward(const tensor<ElementType, InputChannels, InputRows, InputColumns> &input,
                     tensor<ElementType,
                            InputChannels,
                            (InputRows - FilterRows) / StrideRows + 1,
                            (InputColumns - FilterColumns) / StrideColumns + 1> &output,
                     context<InputChannels,
                             (InputRows - FilterRows) / StrideRows + 1,
                             (InputColumns - FilterColumns) / StrideColumns + 1> &context) const
        {
            const auto output_rows = (InputRows - FilterRows) / StrideRows + 1;
            const auto output_columns = (InputColumns - FilterColumns) / StrideColumns + 1;

            for (std::size_t output_channel = 0; output_channel < InputChannels; ++output_channel)
            {
                for (std::size_t output_row = 0; output_row < output_rows; ++output_row)
                {
                    for (std::size_t output_column = 0; output_column < output_columns; ++output_column)
                    {
                        max_pooling_filter<ElementType, FilterRows, FilterColumns>()
                            .forward(input[output_channel],
                                     StrideRows * output_row,
                                     StrideColumns * output_column,
                                     output[output_channel][output_row][output_column],
                                     context.max_indexes[output_channel][output_row][output_column]);
                    }
                }
            }
        }

        template <class ElementType, std::size_t InputChannels, std::size_t InputRows, std::size_t InputColumns>
        void backward(const tensor<ElementType,
                                   InputChannels,
                                   (InputRows - FilterRows) / StrideRows + 1,
                                   (InputColumns - FilterColumns) / StrideColumns + 1> &input_gradient,
                      const context<InputChannels,
                                    (InputRows - FilterRows) / StrideRows + 1,
                                    (InputColumns - FilterColumns) / StrideColumns + 1> &context,
                      tensor<ElementType, InputChannels, InputRows, InputColumns> &output_gradient) const
        {
            const auto output_rows = (InputRows - FilterRows) / StrideRows + 1;
            const auto output_columns = (InputColumns - FilterColumns) / StrideColumns + 1;

            for (std::size_t output_channel = 0; output_channel < InputChannels; ++output_channel)
            {
                for (std::size_t output_row = 0; output_row < output_rows; ++output_row)
                {
                    for (std::size_t output_column = 0; output_column < output_columns; ++output_column)
                    {
                        max_pooling_filter<ElementType, FilterRows, FilterColumns>()
                            .backward(input_gradient[output_channel][output_row][output_column],
                                      context.max_indexes[output_channel][output_row][output_column],
                                      output_gradient[output_channel]);
                    }
                }
            }
        }
    };
}
