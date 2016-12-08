#pragma once

#include <cstddef>

namespace NeuralNetworks
{
    template <std::size_t Left, std::size_t Top = Left, std::size_t Right = Left, std::size_t Bottom = Top>
    struct StaticPadding
    {
        static const auto left = Left;
        static const auto top = Top;
        static const auto right = Right;
        static const auto bottom = Bottom;
        static const auto horizontal = Left + Right;
        static const auto vertical = Top + Bottom;
    };
}
