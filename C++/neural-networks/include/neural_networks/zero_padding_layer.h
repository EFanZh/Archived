#pragma once

#include "tensor.h"

namespace neural_networks
{
    template <std::size_t LeftPadding,
              std::size_t TopPadding = LeftPadding,
              std::size_t RightPadding = LeftPadding,
              std::size_t BottomPadding = TopPadding>
    class zero_padding_layer
    {
    public:
        template <class InputElementType, std::size_t InputChannels, std::size_t Rows, std::size_t Columns>
        void forward(const tensor<InputElementType, InputChannels, Rows, Columns> &input,
                     tensor<InputElementType,
                            InputChannels,
                            Rows + TopPadding + BottomPadding,
                            Columns + LeftPadding + RightPadding> &output) const
        {
            for (std::size_t channel = 0; channel < InputChannels; ++channel)
            {
                for (std::size_t row = 0; row < InputChannels; ++row)
                {
                    for (std::size_t column = 0; column < InputChannels; ++column)
                    {
                        output[channel][row + TopPadding][column + LeftPadding] = input[channel][row][column];
                    }
                }
            }
        }

        template <class InputGradientElementType,
                  std::size_t InputChannels,
                  std::size_t Rows,
                  std::size_t Columns,
                  class OutputGradientElementType>
        void backward(const tensor<InputGradientElementType, InputChannels, Rows, Columns> &input_gradient,
                      tensor<OutputGradientElementType,
                             InputChannels,
                             Rows - (TopPadding + BottomPadding),
                             Columns - (LeftPadding + RightPadding)> &output_gradient) const
        {
            for (std::size_t channel = 0; channel < InputChannels; ++channel)
            {
                for (std::size_t row = 0; row < InputChannels; ++row)
                {
                    for (std::size_t column = 0; column < InputChannels; ++column)
                    {
                        output_gradient[channel][row][column] =
                            input_gradient[channel][row + TopPadding][column + LeftPadding];
                    }
                }
            }
        }
    };
}
