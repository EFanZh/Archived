#pragma once

#include <tuple>
#include "layer_traits.h"
#include <memory>

namespace neural_networks
{
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

    public:
        using input_type = typename layer_type<0>::input_type;
        using output_type = typename layer_type<layer_count - 1>::output_type;

    private:
        template <std::size_t Index, class = layer_type<Index>>
        struct layer_helper
        {
            static_assert(std::is_convertible<typename layer_type<Index>::output_type,
                                              typename layer_type<Index + 1>::input_type>::value,
                          "Type not match.");

            using current_layer_type = layer_type<Index>;

            static void get_weight_gradient(layer_helper_context &context,
                                            const typename layer_type<Index>::input_type &input,
                                            typename layer_type<Index>::input_type &output_gradient,
                                            std::false_type,
                                            std::false_type)
            {
                const auto &layer = std::get<Index>(context.network.layers);
                auto output = std::make_unique<current_layer_type::output_type>();

                layer.forward(input, *output);

                auto output_gradient_next = std::make_unique<current_layer_type::output_type>();

                layer_helper<Index + 1>::get_weight_gradient(context,
                                                             *output,
                                                             *output_gradient_next,
                                                             layer_traits<layer_type<Index + 1>>::has_context,
                                                             layer_traits<layer_type<Index + 1>>::has_weights);

                layer.backward(input, *output, *output_gradient_next, output_gradient);
            }

            static void get_weight_gradient(layer_helper_context &context,
                                            const typename layer_type<Index>::input_type &input,
                                            typename layer_type<Index>::input_type &output_gradient,
                                            std::false_type,
                                            std::true_type)
            {
                const auto &layer = std::get<Index>(context.network.layers);
                auto output = std::make_unique<current_layer_type::output_type>();

                layer.forward(input, *output);

                auto output_gradient_next = std::make_unique<current_layer_type::output_type>();

                layer_helper<Index + 1>::get_weight_gradient(context,
                                                             *output,
                                                             *output_gradient_next,
                                                             layer_traits<layer_type<Index + 1>>::has_context,
                                                             layer_traits<layer_type<Index + 1>>::has_weights);

                layer.backward(input,
                               *output,
                               *output_gradient_next,
                               output_gradient,
                               std::get<Index>(context.weight_gradient.layers));
            }

            static void get_weight_gradient(layer_helper_context &context,
                                            const typename layer_type<Index>::input_type &input,
                                            typename layer_type<Index>::input_type &output_gradient,
                                            std::true_type,
                                            std::false_type)
            {
                const auto &layer = std::get<Index>(context.network.layers);
                auto output = std::make_unique<current_layer_type::output_type>();
                auto layer_context = std::make_unique<current_layer_type::context_type>();

                layer.forward(input, *output, *layer_context);

                auto output_gradient_next = std::make_unique<current_layer_type::output_type>();

                layer_helper<Index + 1>::get_weight_gradient(context,
                                                             *output,
                                                             *output_gradient_next,
                                                             layer_traits<layer_type<Index + 1>>::has_context,
                                                             layer_traits<layer_type<Index + 1>>::has_weights);

                layer.backward(input, *output, *output_gradient_next, output_gradient, *layer_context);
            }

            static void get_weight_gradient(layer_helper_context &context,
                                            const typename layer_type<Index>::input_type &input,
                                            typename layer_type<Index>::input_type &output_gradient,
                                            std::true_type,
                                            std::true_type)
            {
                const auto &layer = std::get<Index>(context.network.layers);
                auto output = std::make_unique<current_layer_type::output_type>();
                auto layer_context = std::make_unique<current_layer_type::context_type>();

                layer.forward(input, *output, *layer_context);

                auto output_gradient_next = std::make_unique<current_layer_type::output_type>();

                layer_helper<Index + 1>::get_weight_gradient(context,
                                                             *output,
                                                             *output_gradient_next,
                                                             layer_traits<layer_type<Index + 1>>::has_context,
                                                             layer_traits<layer_type<Index + 1>>::has_weights);

                layer.backward(input,
                               *output,
                               *output_gradient_next,
                               output_gradient,
                               std::get<Index>(context.weight_gradient.layers),
                               *layer_context);
            }

            template <class StepSize>
            static void update_weights(sequential_network &network,
                                       const sequential_network &gradient,
                                       StepSize step_size,
                                       std::false_type)
            {
                layer_helper<Index + 1>::update_weights(network,
                                                        gradient,
                                                        step_size,
                                                        layer_traits<layer_type<Index + 1>>::has_weights);
            }

            template <class StepSize>
            static void update_weights(sequential_network &network,
                                       const sequential_network &gradient,
                                       StepSize step_size,
                                       std::true_type)
            {
                std::get<Index>(network.layers).update_weights(std::get<Index>(gradient.layers), step_size);

                layer_helper<Index + 1>::update_weights(network,
                                                        gradient,
                                                        step_size,
                                                        layer_traits<layer_type<Index + 1>>::has_weights);
            }

            static void predict(const layers_tuple_type &layers,
                                const typename layer_type<Index>::input_type &input,
                                output_type &output)
            {
                auto layer_output = std::make_unique<typename layer_type<Index>::output_type>();

                std::get<Index>(layers).predict(input, *layer_output);

                layer_helper<Index + 1>::predict(layers, *layer_output, output);
            }
        };

        template <>
        struct layer_helper<layer_count - 1>
        {
            static constexpr auto last_layer = layer_count - 1;
            using current_layer_type = layer_type<last_layer>;

            static void get_weight_gradient(layer_helper_context &context,
                                            const typename layer_type<last_layer>::input_type &input,
                                            typename layer_type<last_layer>::input_type &output_gradient,
                                            std::false_type,
                                            std::false_type)
            {
                const auto &layer = std::get<last_layer>(context.network.layers);
                auto output = std::make_unique<current_layer_type::output_type>();

                layer.forward(input, *output);

                auto output_gradient_next = std::make_unique<current_layer_type::output_type>();

                layer.backward(input, *output, *output_gradient_next, output_gradient);
            }

            static void get_weight_gradient(layer_helper_context &context,
                                            const typename layer_type<last_layer>::input_type &input,
                                            typename layer_type<last_layer>::input_type &output_gradient,
                                            std::false_type,
                                            std::true_type)
            {
                const auto &layer = std::get<last_layer>(context.network.layers);
                auto output = std::make_unique<current_layer_type::output_type>();

                layer.forward(input, *output);

                auto output_gradient_next = std::make_unique<current_layer_type::output_type>();

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
                auto output = std::make_unique<current_layer_type::output_type>();
                auto layer_context = std::make_unique<current_layer_type::context_type>();

                layer.forward(input, *output, *layer_context);

                auto output_gradient_next = std::make_unique<current_layer_type::output_type>();

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
                auto output = std::make_unique<current_layer_type::output_type>();
                auto layer_context = std::make_unique<current_layer_type::context_type>();

                layer.forward(input, output, layer_context);

                auto output_gradient_next = std::make_unique<current_layer_type::output_type>();

                context.network.loss_function.get_gradient(*output, context.sample_result, *output_gradient_next);

                layer.backward(input,
                               *output,
                               *output_gradient_next,
                               context.weight_gradient,
                               std::get<last_layer>(context.weight_gradient.layers),
                               layer_context);
            }

            template <class StepSize>
            static void update_weights(sequential_network &, const sequential_network &, StepSize, std::false_type)
            {
            }

            template <class StepSize>
            static void update_weights(sequential_network &network,
                                       const sequential_network &gradient,
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

    public:
        template <class Iterator, class StepSize>
        void train(Iterator first, Iterator last, std::size_t batch_size, const StepSize &step_size)
        {
            static_assert(std::is_convertible<decltype(first->input), typename layer_type<0>::input_type>::value,
                          "Invalid input type.");

            static_assert(std::is_convertible<decltype(first->output), typename LossFunction::result_type>::value,
                          "Invalid output type.");

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

        void predict(const input_type &input, output_type &output) const
        {
            layer_helper<0>::predict(layers, input, output);
        }
    };
}
