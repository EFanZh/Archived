#pragma once

#include "convolutional_layer.h"
#include "relu_layer.h"
#include "local_response_normalization_layer.h"
#include "max_pooling_layer.h"
#include "zero_padding_layer.h"
#include "fully_connected_layer.h"
#include "dropout_layer.h"
#include "softmax_layer.h"
#include "sequential_network.h"
#include "flatten_layer.h"

namespace neural_networks
{
    template <class T>
    using alexnet = sequential_network<
        softmax_layer,
        // Layer 1.
        zero_padding_layer<T, static_size<3, 224, 224>, static_padding<1, 1, 2, 2>>,
        convolutional_layer<T, static_size<3, 227, 227>, filter<T, 3, 11, 11>, static_size<4, 4>, 96>,
        relu_layer<T, 96, 55, 55>,
        local_response_normalization_layer<
            T,
            static_size<96, 55, 55>,
            local_response_normalization_filter<T, local_response_normalization_hyper_parameters_alexnet>>,
        // Layer 2.
        max_pooling_layer<T, static_size<96, 55, 55>, static_size<3, 3>, static_size<2, 2>>,
        zero_padding_layer<T, static_size<96, 27, 27>, static_padding<2>>,
        convolutional_layer<T, static_size<96, 31, 31>, filter<T, 96, 5, 5>, static_size<1, 1>, 256>,
        relu_layer<T, 256, 27, 27>,
        local_response_normalization_layer<
            T,
            static_size<256, 27, 27>,
            local_response_normalization_filter<T, local_response_normalization_hyper_parameters_alexnet>>,
        // Layer 3.
        max_pooling_layer<T, static_size<256, 27, 27>, static_size<3, 3>, static_size<2, 2>>,
        zero_padding_layer<T, static_size<256, 13, 13>, static_padding<1>>,
        convolutional_layer<T, static_size<256, 15, 15>, filter<T, 256, 3, 3>, static_size<1, 1>, 384>,
        relu_layer<T, 384, 13, 13>,
        // Layer 4.
        zero_padding_layer<T, static_size<384, 13, 13>, static_padding<1>>,
        convolutional_layer<T, static_size<384, 15, 15>, filter<T, 384, 3, 3>, static_size<1, 1>, 384>,
        relu_layer<T, 384, 13, 13>,
        // Layer 5.
        zero_padding_layer<T, static_size<384, 13, 13>, static_padding<1>>,
        convolutional_layer<T, static_size<384, 15, 15>, filter<T, 384, 3, 3>, static_size<1, 1>, 256>,
        relu_layer<T, 256, 13, 13>,
        // Layer 6.
        flatten_layer<T, 256, 13, 13>,
        fully_connected_layer<perceptron<T, 256 * 13 * 13>, 4096>,
        dropout_layer<T, 4096, dropout_strategy_half>,
        relu_layer<T, 4096>,
        // Layer 7.
        fully_connected_layer<perceptron<T, 4096>, 4096>,
        dropout_layer<T, 4096, dropout_strategy_half>,
        relu_layer<T, 4096>,
        // Layer 8.
        fully_connected_layer<perceptron<T, 4096>, 1000>,
        relu_layer<T, 1000>>;
}
