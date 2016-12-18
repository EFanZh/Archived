#pragma once

#include "tensor.h"

namespace neural_networks
{
    template <class T, std::size_t Channels, std::size_t Rows, std::size_t Columns>
    class filter
    {
        using kernel_type = tensor<T, Channels, Rows, Columns>;

        kernel_type kernel;
        T bias;

    public:
        filter() = default;

        filter(const kernel_type &kernel, T bias) : kernel(kernel), bias(bias)
        {
        }

        template <class InputElementType, std::size_t InputRows, std::size_t InputColumns, class OutputType>
        void forward(const tensor<InputElementType, Channels, InputRows, InputColumns> &input,
                     std::size_t input_row_start,
                     std::size_t input_column_start,
                     OutputType &output) const
        {
            auto result = OutputType(bias);

            for (std::size_t channel = 0; channel < Channels; ++channel)
            {
                for (std::size_t row = 0; row < Rows; ++row)
                {
                    for (std::size_t column = 0; column < Columns; ++column)
                    {
                        result += input[channel][input_row_start + row][input_column_start + column] *
                            kernel[channel][row][column];
                    }
                }
            }

            output = result;
        }

        template <class InputElementType,
                  std::size_t InputRows,
                  std::size_t InputColumns,
                  class OutputGradientElementType,
                  class OutputGradientType>
        void backward(const tensor<InputElementType, Channels, InputRows, InputColumns> &input,
                      std::size_t input_row_start,
                      std::size_t input_column_start,
                      const OutputGradientElementType &input_gradient,
                      tensor<OutputGradientType, Channels, InputRows, InputColumns> &output_gradient,
                      filter &output_weight_gradient) const
        {
            for (std::size_t channel = 0; channel < Channels; ++channel)
            {
                for (std::size_t row = 0; row < Rows; ++row)
                {
                    for (std::size_t column = 0; column < Columns; ++column)
                    {
                        output_gradient[channel][input_row_start + row][input_column_start + column] +=
                            input_gradient * kernel[channel][row][column];

                        output_weight_gradient.kernel[channel][row][column] +=
                            input_gradient * input[channel][input_row_start + row][input_column_start + column];
                    }
                }
            }

            output_weight_gradient.bias += input_gradient;
        }

        template <class StepSizeType>
        void update_weights(const filter &gradient, const StepSizeType &step_size)
        {
            for (std::size_t i = 0; i < kernel_type::element_count; ++i)
            {
                kernel.begin_element()[i] -= gradient.kernel.cbegin_element()[i] * step_size;
            }

            bias -= gradient.bias * step_size;
        }
    };
}
