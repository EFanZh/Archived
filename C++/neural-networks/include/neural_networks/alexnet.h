#pragma once

#include "convolutional_layer.h"
#include "relu_layer.h"
#include "local_response_normalization_layer.h"
#include "max_pooling_layer.h"
#include "zero_padding_layer.h"

namespace neural_networks
{
    class alexnet
    {
        using zero_padding_layer_1 = zero_padding_layer<1, 1, 2, 2>;
        using convolution_layer_1 = convolutional_layer<filter<double, 3, 11, 11>, static_size<4, 4>, 96>;
        using relu_layer_1 = relu_layer;

        using lrn_layer_1 = local_response_normalization_layer<
            local_response_normalization_filter<double, local_response_normalization_hyper_parameters_alexnet>>;

        using max_pooling_layer_1 = max_pooling_layer<static_size<3, 3>, static_size<2, 2>>;

        using convolution_layer_2 = convolutional_layer<filter<double, 96, 5, 5>, static_size<1, 1>, 256>;
        using relu_layer_2 = relu_layer;
        using lrn_layer_2 = lrn_layer_1;
        using max_pooling_layer_2 = max_pooling_layer<static_size<3, 3>, static_size<2, 2>>;

        using convolution_layer_3 = convolutional_layer<filter<double, 256, 3, 3>, static_size<1, 1>, 384>;
        using relu_layer_3 = relu_layer;

        using convolution_layer_4 = convolutional_layer<filter<double, 256, 3, 3>, static_size<1, 1>, 384>;
        using relu_layer_4 = relu_layer;

        using convolution_layer_5 = convolutional_layer<filter<double, 256, 27, 27>, static_size<1, 1>, 256>;
        using relu_layer_5 = relu_layer;

        static constexpr auto batch_size = std::size_t(128);
        static constexpr auto momentum = 0.9;
        static constexpr auto wieght_decay = 0.0005;

        zero_padding_layer_1 layer_1_1 = zero_padding_layer_1();
        convolution_layer_1 layer_1_2 = convolution_layer_1();
        lrn_layer_1 layer_1_3 = lrn_layer_1();

    public:
        void train(const tensor<double, 3, 224, 224> &input)
        {
            auto output_1 = tensor<double, 3, 227, 227>();

            layer_1_1.forward(input, output_1);

            auto output_2 = tensor<double, 96, 55, 55>();

            layer_1_2.forward(output_1, output_2);

            auto output_3 = tensor<double, 96, 55, 55>();

            layer_1_3.forward(output_2, output_3);
        }
    };
}
