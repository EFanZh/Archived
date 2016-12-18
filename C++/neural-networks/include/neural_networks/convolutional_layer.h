#pragma once

#include "filter.h"

namespace neural_networks
{
    template <class Filter, class Stride, std::size_t OutputChannels>
    class convolutional_layer;

    template <class FilterElementType,
              std::size_t FilterChannels,
              std::size_t FilterRows,
              std::size_t FilterColumns,
              std::size_t StrideRows,
              std::size_t StrideColumns,
              std::size_t OutputChannels>
    class convolutional_layer<filter<FilterElementType, FilterChannels, FilterRows, FilterColumns>,
                              static_size<StrideRows, StrideColumns>,
                              OutputChannels>
    {
        using filter_type = filter<FilterElementType, FilterChannels, FilterRows, FilterColumns>;

        std::array<filter_type, OutputChannels> filters;

    public:
        convolutional_layer() = default;

        convolutional_layer(const std::array<filter_type, OutputChannels> &filters) : filters(filters)
        {
        }

        template <class InputElementType, std::size_t InputRows, std::size_t InputColumns, class OutputElementType>
        void forward(const tensor<InputElementType, FilterChannels, InputRows, InputColumns> &input,
                     tensor<OutputElementType,
                            OutputChannels,
                            (InputRows - FilterRows) / StrideRows + 1,
                            (InputColumns - FilterColumns) / StrideColumns + 1> &output) const
        {
            const auto output_rows = (InputRows - FilterRows) / StrideRows + 1;
            const auto output_columns = (InputColumns - FilterColumns) / StrideColumns + 1;

            for (size_t output_channel = 0; output_channel < OutputChannels; ++output_channel)
            {
                for (size_t output_row = 0; output_row < output_rows; ++output_row)
                {
                    for (size_t output_column = 0; output_column < output_columns; ++output_column)
                    {
                        filters[output_channel].forward(input,
                                                        StrideRows * output_row,
                                                        StrideColumns * output_column,
                                                        output[output_channel][output_row][output_column]);
                    }
                }
            }
        }

        template <class InputElementType, std::size_t InputRows, std::size_t InputColumns, class OutputElementType>
        void backward(const tensor<InputElementType, FilterChannels, InputRows, InputColumns> &input,
                      tensor<OutputElementType,
                             OutputChannels,
                             (InputRows - FilterRows) / StrideRows + 1,
                             (InputColumns - FilterColumns) / StrideColumns + 1> &input_gradient,
                      tensor<InputElementType, FilterChannels, InputRows, InputColumns> &output_gradient,
                      convolutional_layer &output_weight_gradient) const
        {
            const auto output_rows = (InputRows - FilterRows) / StrideRows + 1;
            const auto output_columns = (InputColumns - FilterColumns) / StrideColumns + 1;

            for (size_t output_channel = 0; output_channel < FilterChannels; ++output_channel)
            {
                for (size_t output_row = 0; output_row < output_rows; ++output_row)
                {
                    for (size_t output_column = 0; output_column < output_columns; ++output_column)
                    {
                        filters[output_channel].backward(input,
                                                         StrideRows * output_row,
                                                         StrideColumns * output_column,
                                                         input_gradient[output_channel][output_row][output_column],
                                                         output_gradient,
                                                         output_weight_gradient.filters[output_channel]);
                    }
                }
            }
        }

        template <class StepSizeType>
        void update_weights(const convolutional_layer &gradient, const StepSizeType &step_size)
        {
            for (std::size_t i = 0; i < OutputChannels; ++i)
            {
                filters[i].update_weights(gradient.filters[i], step_size);
            }
        }
    };
}
