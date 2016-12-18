#pragma once

#include "local_response_normalization_filter.h"

namespace neural_networks
{
    template <class FilterType>
    class local_response_normalization_layer
    {
    public:
        template <class T, std::size_t Channels, std::size_t Rows, std::size_t Columns>
        struct context
        {
            tensor<std::array<T, Channels>, Rows, Columns> sigmas;
        };

        template <class InputElementType,
                  std::size_t InputChannels,
                  std::size_t InputRows,
                  std::size_t InputColumns,
                  class OutputElementType>
        void forward(const tensor<InputElementType, InputChannels, InputRows, InputColumns> &input,
                     tensor<OutputElementType, InputChannels, InputRows, InputColumns> &output,
                     context<OutputElementType, InputChannels, InputRows, InputColumns> &context) const
        {
            for (std::size_t row = 0; row < InputRows; ++row)
            {
                for (std::size_t column = 0; column < InputColumns; ++column)
                {
                    FilterType().forward(input, row, column, output, context.sigmas[row][column].get_value());
                }
            }
        }

        template <class InputElementType,
                  std::size_t InputChannels,
                  std::size_t InputRows,
                  std::size_t InputColumns,
                  class OutputElementType>
        void forward_naive(const tensor<InputElementType, InputChannels, InputRows, InputColumns> &input,
                           tensor<OutputElementType, InputChannels, InputRows, InputColumns> &output) const
        {
            for (std::size_t row = 0; row < InputRows; ++row)
            {
                for (std::size_t column = 0; column < InputColumns; ++column)
                {
                    FilterType().forward_naive(input, row, column, output);
                }
            }
        }

        template <class InputElementType,
                  std::size_t InputChannels,
                  std::size_t InputRows,
                  std::size_t InputColumns,
                  class InputGradientElementType,
                  class OutputGradientElementType>
        void backward(const tensor<InputElementType, InputChannels, InputRows, InputColumns> &input,
                      const tensor<InputGradientElementType, InputChannels, InputRows, InputColumns> &input_gradient,
                      const context<InputGradientElementType, InputChannels, InputRows, InputColumns> &input_context,
                      tensor<OutputGradientElementType, InputChannels, InputRows, InputColumns> &output_gradient) const
        {
            for (std::size_t row = 0; row < InputRows; ++row)
            {
                for (std::size_t column = 0; column < InputColumns; ++column)
                {
                    FilterType().backward(input,
                                          row,
                                          column,
                                          input_gradient,
                                          input_context.sigmas[row][column].get_value(),
                                          output_gradient);
                }
            }
        }
    };
}
