#include <neural_networks/convolutional_layer.h>
#include <neural_networks/fully_connected_layer.h>
#include <neural_networks/max_pooling_layer.h>
#include <neural_networks/relu_layer.h>
#include <neural_networks/dropout_layer.h>
#include <neural_networks/softmax_layer.h>
#include <neural_networks/local_response_normalization_layer.h>
#include <neural_networks/test.h>
#include <neural_networks/zero_padding_layer.h>
#include <neural_networks/utilities.h>
#include <neural_networks/zero_padding_layer.h>
#include <numeric>
#include <iostream>

using namespace std;
using namespace neural_networks;

TEST(convolution_layer_forward)
{
    using my_layer = convolutional_layer<double, static_size<3, 7, 7>, filter<double, 3, 3, 3>, static_size<2, 2>, 2>;

    const auto layer = my_layer({ { { { { { { { { -1, 1, 0 } }, { { 0, 1, 0 } }, { { 0, 1, 1 } } } },
                                          { { { { -1, -1, 0 } }, { { 0, 0, 0 } }, { { 0, -1, 0 } } } },
                                          { { { { 0, 0, -1 } }, { { 0, 1, 0 } }, { { 1, -1, -1 } } } } } },
                                      1 },
                                    { { { { { { { 1, 1, -1 } }, { { -1, -1, 1 } }, { { 0, -1, 1 } } } },
                                          { { { { 0, 1, 0 } }, { { -1, 0, -1 } }, { { -1, 1, 0 } } } },
                                          { { { { -1, 0, 0 } }, { { -1, 0, 1 } }, { { -1, 0, 0 } } } } } },
                                      0 } } });

    const auto input = tensor<double, 3, 7, 7>{ { { { { { 0, 0, 0, 0, 0, 0, 0 } },
                                                      { { 0, 0, 1, 1, 0, 2, 0 } },
                                                      { { 0, 2, 2, 2, 2, 1, 0 } },
                                                      { { 0, 1, 0, 0, 2, 0, 0 } },
                                                      { { 0, 0, 1, 1, 0, 0, 0 } },
                                                      { { 0, 1, 2, 0, 0, 2, 0 } },
                                                      { { 0, 0, 0, 0, 0, 0, 0 } } } },
                                                  { { { { 0, 0, 0, 0, 0, 0, 0 } },
                                                      { { 0, 1, 0, 2, 2, 0, 0 } },
                                                      { { 0, 0, 0, 0, 2, 0, 0 } },
                                                      { { 0, 1, 2, 1, 2, 1, 0 } },
                                                      { { 0, 1, 0, 0, 0, 0, 0 } },
                                                      { { 0, 1, 2, 1, 1, 1, 0 } },
                                                      { { 0, 0, 0, 0, 0, 0, 0 } } } },
                                                  { { { { 0, 0, 0, 0, 0, 0, 0 } },
                                                      { { 0, 2, 1, 2, 0, 0, 0 } },
                                                      { { 0, 1, 0, 0, 1, 0, 0 } },
                                                      { { 0, 0, 2, 1, 0, 1, 0 } },
                                                      { { 0, 0, 1, 2, 2, 2, 0 } },
                                                      { { 0, 2, 1, 0, 0, 1, 0 } },
                                                      { { 0, 0, 0, 0, 0, 0, 0 } } } } } };

    auto output = tensor<double, 2, 3, 3>();

    layer.forward(input, output);

    const auto expected =
        tensor<double, 2, 3, 3>{ { { { { { 6, 7, 5 } }, { { 3, -1, -1 } }, { { 2, -1, 4 } } } },
                                   { { { { 2, -5, -8 } }, { { 1, -4, -4 } }, { { 0, -5, -5 } } } } } };

    expect(output == expected);
}

TEST(convolution_layer_backward)
{
    using my_layer = convolutional_layer<double, static_size<1, 3, 3>, filter<double, 1, 2, 2>, static_size<1, 1>, 2>;

    auto layer = my_layer(
        { { { { { { { { { 1, 2 } }, { { 1, 7 } } } } } }, 2 }, { { { { { { { 1, 2 } }, { { 1, 7 } } } } } }, 3 } } });

    const auto input_1 = tensor<double, 1, 3, 3>{ { { { { { 1, 2, 3 } }, { { 3, 4, 1 } }, { { 1, 1, 1 } } } } } };

    const auto input_2 = tensor<double, 1, 3, 3>{ { { { { { 2, 1, 2 } }, { { 0, 3, 2 } }, { { 1, 3, 3 } } } } } };

    for (auto i = 0; i < 20; ++i)
    {
        auto output_1 = tensor<double, 2, 2, 2>();
        auto output_2 = tensor<double, 2, 2, 2>();

        layer.forward(input_1, output_1);
        layer.forward(input_2, output_2);

        const auto error_1 = accumulate(output_1.cbegin_element(), output_1.cend_element(), 0.0) - 4;
        const auto error_2 = accumulate(output_1.cbegin_element(), output_1.cend_element(), 0.0) - 2;
        const auto target = (error_1 * error_1 + error_2 * error_2) * 0.5;

        cout << "convolution_backward target: " << target << '\n';

        const auto gradient_target_to_error_1 = error_1;
        const auto gradient_target_to_error_2 = error_2;
        auto gradient_target_to_output_1 = tensor<double, 2, 2, 2>();
        auto gradient_target_to_output_2 = tensor<double, 2, 2, 2>();

        fill(gradient_target_to_output_1.begin_element(),
             gradient_target_to_output_1.end_element(),
             gradient_target_to_error_1);

        fill(gradient_target_to_output_2.begin_element(),
             gradient_target_to_output_2.end_element(),
             gradient_target_to_error_2);

        auto gradient_target_to_input_1 = tensor<double, 1, 3, 3>();
        auto gradient_target_to_input_2 = tensor<double, 1, 3, 3>();
        auto gradient_layer_weight_to_target = my_layer();

        layer.backward(input_1,
                       output_1,
                       gradient_target_to_output_1,
                       gradient_target_to_input_1,
                       gradient_layer_weight_to_target);
        layer.backward(input_2,
                       output_2,
                       gradient_target_to_output_2,
                       gradient_target_to_input_2,
                       gradient_layer_weight_to_target);

        layer.update_weights(gradient_layer_weight_to_target, 0.001);
    }
}

TEST(fully_connected_layer_forward)
{
    using my_layer = fully_connected_layer<perceptron<double, 3>, 4>;

    const auto layer = my_layer(
        { { { { { 1, 3, 4 } }, 1 }, { { { 5, 7, 4 } }, 3 }, { { { 1, 3, 6 } }, 6 }, { { { 1, 2, 3 } }, 2 } } });

    const auto input = tensor<double, 3>{ { 1, 2, 3 } };

    tensor<double, 4> output;

    layer.forward(input, output);

    const auto expected = tensor<double, 4>{ { 20, 34, 31, 16 } };

    expect(output == expected);
}

TEST(fully_connected_layer_backward)
{
    using my_layer = fully_connected_layer<perceptron<double, 3>, 3>;

    auto layer = my_layer({ { { { { 2, 3, 4 } }, 1 }, { { { 6, 7, 8 } }, 2 }, { { { 7, 8, 3 } }, 3 } } });

    const auto input = tensor<double, 3>{ { 1, 2, 3 } };

    for (auto i = 0; i < 20; ++i)
    {
        auto output = tensor<double, 3>();

        layer.forward(input, output);

        const auto error = tensor<double, 3>{ { output[0] - 0, output[1] - 4, output[2] - 0 } };
        const auto target = (error[0] * error[0] + error[1] * error[1] + error[2] * error[2]) * 0.5;

        cout << "fully_connected_layer_backward target: " << target << '\n';

        const auto gradient_target_to_error = error;
        const auto gradient_target_to_output = gradient_target_to_error;
        auto gradient_target_to_input = tensor<double, 3>();
        auto gradient_layer_weights_to_target = my_layer();

        layer.backward(input, output, gradient_target_to_output, gradient_target_to_input, gradient_layer_weights_to_target);

        layer.update_weights(gradient_layer_weights_to_target, 0.1);
    }
}

TEST(max_pooling_layer_forward)
{
    using my_layer = max_pooling_layer<double, static_size<1, 5, 5>, static_size<3, 3>, static_size<2, 2>>;

    const auto layer = my_layer();
    const auto input = tensor<double, 1, 5, 5>{ { { { { { 2, 4, 1, 3, 5 } },
                                                      { { 1, 0, 7, 6, 2 } },
                                                      { { 3, 9, 0, 2, 5 } },
                                                      { { 7, 8, 4, 0, 1 } },
                                                      { { 9, 5, 6, 4, 2 } } } } } };

    auto output = tensor<double, 1, 2, 2>();
    auto context = my_layer::context_type();

    layer.forward(input, output, context);

    const auto expected_output = tensor<double, 1, 2, 2>{ { { { { { 9, 7 } }, { { 9, 6 } } } } } };
    const auto expected_context = my_layer::context_type
        { { { { { { { { { 2, 1 } } }, { { { 1, 2 } } } } }, { { { { { 2, 1 } } }, { { { 4, 2 } } } } } } } } }
    ;

    expect(output == expected_output);
    expect(context == expected_context);
}

TEST(max_pooling_layer_backward)
{
    using my_layer = max_pooling_layer<double, static_size<1, 5, 5>, static_size<3, 3>, static_size<2, 2>>;

    const auto layer = my_layer();
    const auto input = tensor<double, 1, 5, 5>{ { { { { { 2, 4, 1, 3, 5 } },
                                                      { { 1, 0, 7, 6, 2 } },
                                                      { { 3, 9, 0, 2, 5 } },
                                                      { { 7, 8, 4, 0, 1 } },
                                                      { { 9, 5, 6, 4, 2 } } } } } };

    auto output = tensor<double, 1, 2, 2>();
    auto context = my_layer::context_type();

    layer.forward(input, output, context);

    const auto input_gradient = tensor<double, 1, 2, 2>{ { { { { { 11, 22 } }, { { 33, 44 } } } } } };
    auto output_gradient = tensor<double, 1, 5, 5>();

    layer.backward(input, output, input_gradient, output_gradient, context);

    const auto expected_output_gradient = tensor<double, 1, 5, 5>{ { { { { { 0, 0, 0, 0, 0 } },
                                                                         { { 0, 0, 22, 0, 0 } },
                                                                         { { 0, 44, 0, 0, 0 } },
                                                                         { { 0, 0, 0, 0, 0 } },
                                                                         { { 0, 0, 44, 0, 0 } } } } } };

    expect(output_gradient == expected_output_gradient);
}

TEST(relu_layer_forward)
{
    auto layer = relu_layer<double, 2, 3>();

    const auto input = tensor<double, 2, 3>{ { { { -2, 0, 1 } }, { { 7, 2, -3 } } } };
    auto output = tensor<double, 2, 3>();

    layer.forward(input, output);

    const auto expected_output = tensor<double, 2, 3>{ { { { 0, 0, 1 } }, { { 7, 2, 0 } } } };

    expect(output == expected_output);
}

TEST(relu_layer_backward)
{
    auto layer = relu_layer<double, 2, 3>();

    const auto input = tensor<double, 2, 3>{ { { { -2, 0, 1 } }, { { 7, 2, -3 } } } };
    auto output = tensor<double, 2, 3>();

    layer.forward(input, output);

    const auto input_gradient = tensor<double, 2, 3>{ { { { 1, 2, 3 } }, { { 4, 5, 6 } } } };
    auto output_gradient = tensor<double, 2, 3>();

    layer.backward(input, output, input_gradient, output_gradient);

    const auto expected_output_gradient = tensor<double, 2, 3>{ { { { 0, 2, 3 } }, { { 4, 5, 0 } } } };

    expect(output_gradient == expected_output_gradient);
}

TEST(local_response_normalization_layer_forward)
{
    using my_layer = local_response_normalization_layer<double, static_size<10,1,2>,
        local_response_normalization_filter<double, local_response_normalization_hyper_parameters_alexnet>>;

    const auto layer = my_layer();
    const auto input = tensor<double, 10, 1, 2>{ { { { { { 1, 6 } } } },
                                                   { { { { 2, 3 } } } },
                                                   { { { { 4, 5 } } } },
                                                   { { { { 3, 0 } } } },
                                                   { { { { 7, 3 } } } },
                                                   { { { { 2, 8 } } } },
                                                   { { { { 6, 7 } } } },
                                                   { { { { 2, 2 } } } },
                                                   { { { { 4, 0 } } } },
                                                   { { { { 7, 4 } } } } } };

    auto output = tensor<double, 10, 1, 2>();
    auto context = my_layer::context_type();

    layer.forward(input, output, context);
}

TEST(local_response_normalization_layer_backward)
{
    using my_layer = local_response_normalization_layer<
        double,
        static_size<10, 1, 2>,
        local_response_normalization_filter<double, local_response_normalization_hyper_parameters_alexnet>>;

    const auto layer = my_layer();
    const auto input = tensor<double, 10, 1, 2>{ { { { { { 1, 6 } } } },
                                                   { { { { 2, 3 } } } },
                                                   { { { { 4, 5 } } } },
                                                   { { { { 3, 0 } } } },
                                                   { { { { 7, 3 } } } },
                                                   { { { { 2, 8 } } } },
                                                   { { { { 6, 7 } } } },
                                                   { { { { 2, 2 } } } },
                                                   { { { { 4, 0 } } } },
                                                   { { { { 7, 4 } } } } } };

    auto output = tensor<double, 10, 1, 2>();
    auto context = my_layer::context_type();

    layer.forward(input, output, context);

    const auto input_gradient = tensor<double, 10, 1, 2>{ { { { { { 1, 1 } } } },
                                                            { { { { 2, 6 } } } },
                                                            { { { { 3, 4 } } } },
                                                            { { { { 3, 2 } } } },
                                                            { { { { 1, 5 } } } },
                                                            { { { { 2, 2 } } } },
                                                            { { { { 1, 4 } } } },
                                                            { { { { 4, 4 } } } },
                                                            { { { { 0, 1 } } } },
                                                            { { { { 3, 7 } } } } } };

    auto output_gradient = tensor<double, 10, 1, 2>();

    layer.backward(input, output, input_gradient, output_gradient, context);

    print_line(output_gradient);
}

TEST(dropout_layer_forward)
{
    using my_layer = dropout_layer<int, 10, dropout_strategy_half>;

    const auto layer = my_layer();
    const auto input = tensor<int, 10>{ { 7, 6, 5, 4, 3, 2, 1, 9, 5, 2 } };
    auto output = tensor<int, 10>();
    auto effective_indexes = vector<size_t>();

    layer.forward(input, output, effective_indexes);

    for (size_t i = 0; i < output.get_dimensions<0>(); ++i)
    {
        if (output[i] == 0)
        {
            expect(!binary_search(effective_indexes.cbegin(), effective_indexes.cend(), i));
        }
        else
        {
            expect(binary_search(effective_indexes.cbegin(), effective_indexes.cend(), i));
        }
    }
}

TEST(dropout_layer_backward)
{
    using my_layer = dropout_layer<int, 10, dropout_strategy_half>;

    const auto layer = my_layer();
    const auto input = tensor<int, 10>{ { 7, 6, 5, 4, 3, 2, 1, 9, 5, 2 } };
    auto output = tensor<int, 10>();
    auto effective_indexes = vector<size_t>();

    layer.forward(input, output, effective_indexes);

    const auto input_gradient = tensor<int, 10>{ { 2, 2, 3, 3, 4, 5, 6, 9, 7, 2 } };
    auto output_gradient = tensor<int, 10>();

    layer.backward(input, output, input_gradient, output_gradient, effective_indexes);

    for (size_t i = 0; i < output_gradient.get_dimensions<0>(); ++i)
    {
        if (output_gradient[i] == 0)
        {
            expect(!binary_search(effective_indexes.cbegin(), effective_indexes.cend(), i));
        }
        else
        {
            expect(binary_search(effective_indexes.cbegin(), effective_indexes.cend(), i));
        }
    }
}

TEST(dropout_layer_forward_not_training)
{
    using my_layer = dropout_layer<double, 10, dropout_strategy_half>;

    const auto layer = my_layer();
    const auto input = tensor<double, 10>{ { 7, 6, 5, 4, 3, 2, 1, 9, 5, 2 } };
    auto output = tensor<double, 10>();

    layer.forward_not_training(input, output);

    for (size_t i = 0; i < output.get_dimensions<0>(); ++i)
    {
        expect(output[i] == input[i] * dropout_strategy_half::effective_probability);
    }
}

TEST(softmax_layer_forward)
{
    using my_layer = softmax_layer;

    const auto layer = my_layer();
    const auto input = tensor<double, 5>{ { 1, 4, 8, 7, 2 } };
    const auto expected = size_t(2);
    auto output = double(0);

    layer.forward(input, expected, output);

    print_line(output);
}

TEST(softmax_layer_backward)
{
    using my_layer = softmax_layer;

    const auto layer = my_layer();
    const auto input = tensor<double, 5>{ { 1, 4, 8, 7, 2 } };
    const auto expected = size_t(2);
    auto output_gradient = tensor<double, 5>();

    layer.backward(input, expected, output_gradient);

    print_line(output_gradient);
}

TEST(zero_padding_layer_forward)
{
    using my_layer = zero_padding_layer<double, static_size<2, 2, 2>, static_padding<1, 2, 3, 4>>;

    const auto layer = my_layer();
    const auto input =
        tensor<double, 2, 2, 2>{ { { { { { 1, 2 } }, { { 3, 4 } } } }, { { { { 5, 6 } }, { { 7, 8 } } } } } };

    auto output = tensor<double, 2, 8, 6>();

    layer.forward(input, output);
}

TEST(zero_padding_layer_backward)
{
    using my_layer = zero_padding_layer<double, static_size<2, 2, 2>, static_padding<1, 2, 3, 4>>;

    const auto layer = my_layer();
    const auto input_gradient = tensor<double, 2, 8, 6>();
    auto output_gradient = tensor<double, 2, 2, 2>();

    layer.backward(tensor<double, 2, 2, 2>(), tensor<double, 2, 8, 6>(), input_gradient, output_gradient);
}
