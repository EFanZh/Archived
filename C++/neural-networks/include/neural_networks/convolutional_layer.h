#pragma once

#include "filter.h"

namespace neural_networks
{
    template <class T, class InputSize, class Filter, class Stride, std::size_t OutputChannels>
    class convolutional_layer;

    template <class T,
              std::size_t InputChannels,
              std::size_t InputRows,
              std::size_t InputColumns,
              std::size_t FilterRows,
              std::size_t FilterColumns,
              std::size_t StrideRows,
              std::size_t StrideColumns,
              std::size_t OutputChannels>
    class convolutional_layer<T,
                              static_size<InputChannels, InputRows, InputColumns>,
                              filter<T, InputChannels, FilterRows, FilterColumns>,
                              static_size<StrideRows, StrideColumns>,
                              OutputChannels>
    {
        using filter_type = filter<T, InputChannels, FilterRows, FilterColumns>;

        std::array<filter_type, OutputChannels> filters;

    public:
        using input_type = tensor<T, InputChannels, InputRows, InputColumns>;
        using output_type = tensor<T,
                                   OutputChannels,
                                   (InputRows - FilterRows) / StrideRows + 1,
                                   (InputColumns - FilterColumns) / StrideColumns + 1>;

        convolutional_layer() = default;

        convolutional_layer(const std::array<filter_type, OutputChannels> &filters) : filters(filters)
        {
        }

        void forward(const input_type &input, output_type &output) const
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

        void predict(const input_type &input, output_type &output) const
        {
            forward(input, output);
        }

        void backward(const input_type &input,
                      const output_type &,
                      const output_type &input_gradient,
                      input_type &output_gradient,
                      convolutional_layer &output_weight_gradient) const
        {
            const auto output_rows = (InputRows - FilterRows) / StrideRows + 1;
            const auto output_columns = (InputColumns - FilterColumns) / StrideColumns + 1;

            for (size_t output_channel = 0; output_channel < OutputChannels; ++output_channel)
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

        void update_weights(const convolutional_layer &gradient, const T &step_size)
        {
            for (std::size_t i = 0; i < OutputChannels; ++i)
            {
                filters[i].update_weights(gradient.filters[i], step_size);
            }
        }
    };
}
