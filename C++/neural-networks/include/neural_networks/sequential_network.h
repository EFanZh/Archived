#pragma once

#include <tuple>
#include "layer_traits.h"
#include <memory>

namespace neural_networks
{
    namespace details
    {
        template <class SequentialNetwork, std::size_t RestLayers>
        struct layer_helper
        {
            static constexpr auto index = SequentialNetwork::layer_count - RestLayers;

            template <std::size_t I>
            using layer_type = typename SequentialNetwork::template layer_type<I>;

            using layer_helper_context = typename SequentialNetwork::layer_helper_context;
            using layers_tuple_type = typename SequentialNetwork::layers_tuple_type;
            using output_type = typename SequentialNetwork::output_type;

            static_assert(std::is_convertible<typename layer_type<index>::output_type,
                                              typename layer_type<index + 1>::input_type>::value,
                          "Type not match.");

            using current_layer_type = layer_type<index>;

            static void get_weight_gradient(layer_helper_context &context,
                                            const typename layer_type<index>::input_type &input,
                                            typename layer_type<index>::input_type &output_gradient,
                                            std::false_type,
                                            std::false_type)
            {
                const auto &layer = std::get<index>(context.network.layers);
                auto output = std::make_unique<typename current_layer_type::output_type>();

                layer.forward(input, *output);

                auto output_gradient_next = std::make_unique<typename current_layer_type::output_type>();

                layer_helper<SequentialNetwork,
                             RestLayers - 1>::get_weight_gradient(context,
                                                                  *output,
                                                                  *output_gradient_next,
                                                                  layer_traits<layer_type<index + 1>>::has_context,
                                                                  layer_traits<layer_type<index + 1>>::has_weights);

                layer.backward(input, *output, *output_gradient_next, output_gradient);
            }

            static void get_weight_gradient(layer_helper_context &context,
                                            const typename layer_type<index>::input_type &input,
                                            typename layer_type<index>::input_type &output_gradient,
                                            std::false_type,
                                            std::true_type)
            {
                const auto &layer = std::get<index>(context.network.layers);
                auto output = std::make_unique<typename current_layer_type::output_type>();

                layer.forward(input, *output);

                auto output_gradient_next = std::make_unique<typename current_layer_type::output_type>();

                layer_helper<SequentialNetwork,
                             RestLayers - 1>::get_weight_gradient(context,
                                                                  *output,
                                                                  *output_gradient_next,
                                                                  layer_traits<layer_type<index + 1>>::has_context,
                                                                  layer_traits<layer_type<index + 1>>::has_weights);

                layer.backward(input,
                               *output,
                               *output_gradient_next,
                               output_gradient,
                               std::get<index>(context.weight_gradient.layers));
            }

            static void get_weight_gradient(layer_helper_context &context,
                                            const typename layer_type<index>::input_type &input,
                                            typename layer_type<index>::input_type &output_gradient,
                                            std::true_type,
                                            std::false_type)
            {
                const auto &layer = std::get<index>(context.network.layers);
                auto output = std::make_unique<typename current_layer_type::output_type>();
                auto layer_context = std::make_unique<typename current_layer_type::context_type>();

                layer.forward(input, *output, *layer_context);

                auto output_gradient_next = std::make_unique<typename current_layer_type::output_type>();

                layer_helper<SequentialNetwork,
                             RestLayers - 1>::get_weight_gradient(context,
                                                                  *output,
                                                                  *output_gradient_next,
                                                                  layer_traits<layer_type<index + 1>>::has_context,
                                                                  layer_traits<layer_type<index + 1>>::has_weights);

                layer.backward(input, *output, *output_gradient_next, output_gradient, *layer_context);
            }

            static void get_weight_gradient(layer_helper_context &context,
                                            const typename layer_type<index>::input_type &input,
                                            typename layer_type<index>::input_type &output_gradient,
                                            std::true_type,
                                            std::true_type)
            {
                const auto &layer = std::get<index>(context.network.layers);
                auto output = std::make_unique<typename current_layer_type::output_type>();
                auto layer_context = std::make_unique<typename current_layer_type::context_type>();

                layer.forward(input, *output, *layer_context);

                auto output_gradient_next = std::make_unique<typename current_layer_type::output_type>();

                layer_helper<SequentialNetwork,
                             RestLayers - 1>::get_weight_gradient(context,
                                                                  *output,
                                                                  *output_gradient_next,
                                                                  layer_traits<layer_type<index + 1>>::has_context,
                                                                  layer_traits<layer_type<index + 1>>::has_weights);

                layer.backward(input,
                               *output,
                               *output_gradient_next,
                               output_gradient,
                               std::get<index>(context.weight_gradient.layers),
                               *layer_context);
            }

            template <class StepSize>
            static void update_weights(SequentialNetwork &network,
                                       const SequentialNetwork &gradient,
                                       StepSize step_size,
                                       std::false_type)
            {
                layer_helper<SequentialNetwork,
                             RestLayers - 1>::update_weights(network,
                                                             gradient,
                                                             step_size,
                                                             layer_traits<layer_type<index + 1>>::has_weights);
            }

            template <class StepSize>
            static void update_weights(SequentialNetwork &network,
                                       const SequentialNetwork &gradient,
                                       StepSize step_size,
                                       std::true_type)
            {
                std::get<index>(network.layers).update_weights(std::get<index>(gradient.layers), step_size);

                layer_helper<SequentialNetwork,
                             RestLayers - 1>::update_weights(network,
                                                             gradient,
                                                             step_size,
                                                             layer_traits<layer_type<index + 1>>::has_weights);
            }

            static void predict(const layers_tuple_type &layers,
                                const typename layer_type<index>::input_type &input,
                                output_type &output)
            {
                auto layer_output = std::make_unique<typename layer_type<index>::output_type>();

                std::get<index>(layers).predict(input, *layer_output);

                layer_helper<SequentialNetwork, RestLayers - 1>::predict(layers, *layer_output, output);
            }
        };

        template <class SequentialNetwork>
        struct layer_helper<SequentialNetwork, 1>
        {
            static constexpr auto layer_count = SequentialNetwork::layer_count;

            template <std::size_t I>
            using layer_type = typename SequentialNetwork::template layer_type<I>;

            using layer_helper_context = typename SequentialNetwork::layer_helper_context;
            using layers_tuple_type = typename SequentialNetwork::layers_tuple_type;
            using output_type = typename SequentialNetwork::output_type;

            static constexpr auto last_layer = layer_count - 1;
            using current_layer_type = layer_type<last_layer>;

            static void get_weight_gradient(layer_helper_context &context,
                                            const typename layer_type<last_layer>::input_type &input,
                                            typename layer_type<last_layer>::input_type &output_gradient,
                                            std::false_type,
                                            std::false_type)
            {
                const auto &layer = std::get<last_layer>(context.network.layers);
                auto output = std::make_unique<typename current_layer_type::output_type>();

                layer.forward(input, *output);

                auto output_gradient_next = std::make_unique<typename current_layer_type::output_type>();

                layer.backward(input, *output, *output_gradient_next, output_gradient);
            }

            static void get_weight_gradient(layer_helper_context &context,
                                            const typename layer_type<last_layer>::input_type &input,
                                            typename layer_type<last_layer>::input_type &output_gradient,
                                            std::false_type,
                                            std::true_type)
            {
                const auto &layer = std::get<last_layer>(context.network.layers);
                auto output = std::make_unique<typename current_layer_type::output_type>();

                layer.forward(input, *output);

                auto output_gradient_next = std::make_unique<typename current_layer_type::output_type>();

                layer.backward(input,
                               *output,
                               *output_gradient_next,
                               output_gradient,
                               std::get<last_layer>(context.weight_gradient.layers));
            }

            static void get_weight_gradient(layer_helper_context &context,
                                            const typename layer_type<last_layer>::input_type &input,
                                            typename layer_type<last_layer>::input_type &output_gradient,
                                            std::true_type,
                                            std::false_type)
            {
                const auto &layer = std::get<last_layer>(context.network.layers);
                auto output = std::make_unique<typename current_layer_type::output_type>();
                auto layer_context = std::make_unique<typename current_layer_type::context_type>();

                layer.forward(input, *output, *layer_context);

                auto output_gradient_next = std::make_unique<typename current_layer_type::output_type>();

                context.network.loss_function.get_gradient(*output, context.sample_result, *output_gradient_next);

                layer.backward(input, *output, *output_gradient_next, output_gradient, *layer_context);
            }

            static void get_weight_gradient(layer_helper_context &context,
                                            const typename layer_type<last_layer>::input_type &input,
                                            typename layer_type<last_layer>::input_type &output_gradient,
                                            std::true_type,
                                            std::true_type)
            {
                const auto &layer = std::get<last_layer>(context.layers);
                auto output = std::make_unique<typename current_layer_type::output_type>();
                auto layer_context = std::make_unique<typename current_layer_type::context_type>();

                layer.forward(input, output, layer_context);

                auto output_gradient_next = std::make_unique<typename current_layer_type::output_type>();

                context.network.loss_function.get_gradient(*output, context.sample_result, *output_gradient_next);

                layer.backward(input,
                               *output,
                               *output_gradient_next,
                               context.weight_gradient,
                               std::get<last_layer>(context.weight_gradient.layers),
                               layer_context);
            }

            template <class StepSize>
            static void update_weights(SequentialNetwork &, const SequentialNetwork &, StepSize, std::false_type)
            {
            }

            template <class StepSize>
            static void update_weights(SequentialNetwork &network,
                                       const SequentialNetwork &gradient,
                                       StepSize step_size,
                                       std::true_type)
            {
                std::get<last_layer>(network.layers).update_weights(std::get<last_layer>(gradient.layers), step_size);
            }

            static void predict(const layers_tuple_type &layers,
                                const typename layer_type<last_layer>::input_type &input,
                                output_type &output)
            {
                std::get<last_layer>(layers).predict(input, output);
            }
        };
    }

    template <class Input, class Output>
    struct training_sample
    {
        Input input;
        Output output;
    };

    template <class LossFunction, class... Layers>
    class sequential_network
    {
        using layers_tuple_type = std::tuple<Layers...>;

        using sequential_network_type = sequential_network;

        template <std::size_t Index>
        using layer_type = std::tuple_element_t<Index, layers_tuple_type>;

        static constexpr auto layer_count = sizeof...(Layers);

        layers_tuple_type layers;
        LossFunction loss_function;

        struct layer_helper_context
        {
            const sequential_network &network;
            const typename LossFunction::result_type &sample_result;
            sequential_network &weight_gradient;
        };

        template <std::size_t Index>
        using layer_helper = details::layer_helper<sequential_network, layer_count - Index>;

        template <class Input, class Output>
        void train_one_sample(const training_sample<Input, Output> &sample,
                              sequential_network &output_weight_gradient) const
        {
            auto context = layer_helper_context{ *this, sample.output, output_weight_gradient };
            auto unused_output_gradient = std::make_unique<typename layer_type<0>::input_type>();

            layer_helper<0>::get_weight_gradient(context,
                                                 sample.input,
                                                 *unused_output_gradient,
                                                 layer_traits<layer_type<0>>::has_context,
                                                 layer_traits<layer_type<0>>::has_weights);
        }

        template <class T, std::size_t Index>
        friend struct details::layer_helper;

    public:
        using input_type = typename layer_type<0>::input_type;
        using output_type = typename layer_type<layer_count - 1>::output_type;

        template <class Iterator, class StepSize>
        void train(Iterator first,
                   Iterator last,
                   std::size_t batch_size,
                   std::size_t iterations,
                   const StepSize &step_size)
        {
            static_assert(std::is_convertible<decltype(first->input), typename layer_type<0>::input_type>::value,
                          "Invalid input type.");

            static_assert(std::is_convertible<decltype(first->output), typename LossFunction::result_type>::value,
                          "Invalid output type.");

            for (std::size_t iteration = 0; iteration < iterations; ++iteration)
            {
                while (first != last)
                {
                    auto weight_gradient = std::make_unique<sequential_network>();

                    for (std::size_t i = 0; i < batch_size; ++i)
                    {
                        train_one_sample(*first, *weight_gradient);

                        ++first;

                        if (first == last)
                        {
                            break;
                        }
                    }

                    layer_helper<0>::update_weights(*this,
                                                    *weight_gradient,
                                                    step_size,
                                                    layer_traits<layer_type<0>>::has_weights);
                }
            }
        }

        void predict(const input_type &input, output_type &output) const
        {
            layer_helper<0>::predict(layers, input, output);
        }
    };
}
