#include <NeuralNetworks/ConvolutionalLayer.h>
#include <NeuralNetworks/Test.h>

using namespace NeuralNetworks;

TEST(ConvolutionForward)
{
    using MyLayer = ConvolutionalLayer<Tensor<double, StaticSize<7, 7, 3>>,
                                       Tensor<double, StaticSize<3, 3, 3>>,
                                       StaticSize<2, 2, 1>>;

    const auto layer = MyLayer({ { { { { { -1, -1, 0 } }, { { 1, -1, 0 } }, { { 0, 0, -1 } } } },
                                   { { { { 0, 0, 0 } }, { { 1, 0, 1 } }, { { 0, 0, 0 } } } },
                                   { { { { 0, 0, 1 } }, { { 1, -1, -1 } }, { { 1, 0, -1 } } } } } },
                               1);

    const auto input = MyLayer::InputType{ { { { { { 0, 0, 0 } },
                                                 { { 0, 0, 0 } },
                                                 { { 0, 0, 0 } },
                                                 { { 0, 0, 0 } },
                                                 { { 0, 0, 0 } },
                                                 { { 0, 0, 0 } },
                                                 { { 0, 0, 0 } } } },
                                             { { { { 0, 0, 0 } },
                                                 { { 0, 1, 2 } },
                                                 { { 1, 0, 1 } },
                                                 { { 1, 2, 2 } },
                                                 { { 0, 2, 0 } },
                                                 { { 2, 0, 0 } },
                                                 { { 0, 0, 0 } } } },
                                             { { { { 0, 0, 0 } },
                                                 { { 2, 0, 1 } },
                                                 { { 2, 0, 0 } },
                                                 { { 2, 0, 0 } },
                                                 { { 2, 2, 1 } },
                                                 { { 1, 0, 0 } },
                                                 { { 0, 0, 0 } } } },
                                             { { { { 0, 0, 0 } },
                                                 { { 1, 1, 0 } },
                                                 { { 0, 2, 2 } },
                                                 { { 0, 1, 1 } },
                                                 { { 2, 2, 0 } },
                                                 { { 0, 1, 1 } },
                                                 { { 0, 0, 0 } } } },
                                             { { { { 0, 0, 0 } },
                                                 { { 0, 1, 0 } },
                                                 { { 1, 0, 1 } },
                                                 { { 1, 0, 2 } },
                                                 { { 0, 0, 2 } },
                                                 { { 0, 0, 2 } },
                                                 { { 0, 0, 0 } } } },
                                             { { { { 0, 0, 0 } },
                                                 { { 1, 1, 2 } },
                                                 { { 2, 2, 1 } },
                                                 { { 0, 1, 0 } },
                                                 { { 0, 1, 0 } },
                                                 { { 2, 1, 1 } },
                                                 { { 0, 0, 0 } } } },
                                             { { { { 0, 0, 0 } },
                                                 { { 0, 0, 0 } },
                                                 { { 0, 0, 0 } },
                                                 { { 0, 0, 0 } },
                                                 { { 0, 0, 0 } },
                                                 { { 0, 0, 0 } },
                                                 { { 0, 0, 0 } } } } } };

    auto output = Tensor<double, MyLayer::OutputSize>();

    layer.Forward(input, output);

    const auto expected = Tensor<double, StaticSize<3, 3, 1>>{ { { { { { 6 } }, { { 7 } }, { { 5 } } } },
                                                                 { { { { 3 } }, { { -1 } }, { { -1 } } } },
                                                                 { { { { 2 } }, { { -1 } }, { { 4 } } } } } };

    assert(output == expected);
}