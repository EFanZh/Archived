#pragma once

#include "tensor.h"
#include "static_padding.h"

namespace neural_networks
{
    template <class T, class InputSize, class Padding>
    class zero_padding_layer;

    template <class T,
              std::size_t Channels,
              std::size_t Rows,
              std::size_t Columns,
              std::size_t LeftPadding,
              std::size_t TopPadding,
              std::size_t RightPadding,
              std::size_t BottomPadding>
    class zero_padding_layer<T,
                             static_size<Channels, Rows, Columns>,
                             static_padding<LeftPadding, TopPadding, RightPadding, BottomPadding>>
    {
    public:
        using input_type = tensor<T, Channels, Rows, Columns>;
        using output_type =
            tensor<T, Channels, Rows + TopPadding + BottomPadding, Columns + LeftPadding + RightPadding>;

        void forward(const input_type &input, output_type &output) const
        {
            for (std::size_t channel = 0; channel < Channels; ++channel)
            {
                for (std::size_t row = 0; row < Rows; ++row)
                {
                    for (std::size_t column = 0; column < Columns; ++column)
                    {
                        output[channel][row + TopPadding][column + LeftPadding] = input[channel][row][column];
                    }
                }
            }
        }

        void predict(const input_type &input, output_type &output) const
        {
            forward(input, output);
        }

        void backward(const input_type &,
                      const output_type &,
                      const output_type &input_gradient,
                      input_type &output_gradient) const
        {
            for (std::size_t channel = 0; channel < Channels; ++channel)
            {
                for (std::size_t row = 0; row < Rows; ++row)
                {
                    for (std::size_t column = 0; column < Columns; ++column)
                    {
                        output_gradient[channel][row][column] =
                            input_gradient[channel][row + TopPadding][column + LeftPadding];
                    }
                }
            }
        }
    };
}
