#pragma once

#include <type_traits>

namespace neural_networks
{
    namespace details
    {
        template <class T, class = std::void_t<>>
        struct layer_traits_has_context : std::false_type
        {
        };

        template <class T>
        struct layer_traits_has_context<T, std::void_t<typename T::context_type>> : std::true_type
        {
        };

        template <class T, class = std::void_t<>>
        struct layer_traits_has_weights : std::false_type
        {
        };

        template <class T>
        struct layer_traits_has_weights<T, std::void_t<decltype(&T::update_weights)>> : std::true_type
        {
        };
    }

    template <class T>
    struct layer_traits
    {
        static constexpr auto has_context = details::layer_traits_has_context<T>();
        static constexpr auto has_weights = details::layer_traits_has_weights<T>();
    };
}
