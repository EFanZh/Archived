#pragma once

#include <cstddef>

namespace neural_networks
{
    template <std::size_t Left, std::size_t Top = Left, std::size_t Right = Left, std::size_t Bottom = Top>
    struct static_padding
    {
    };
}
