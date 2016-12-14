#include <NeuralNetworks/ConvolutionalLayer.h>
#include <NeuralNetworks/FullyConnectedLayer.h>

#include <iostream>

using namespace std;
using namespace NeuralNetworks;

int main()
{
    using MyLayer = FullyConnectedLayer<Tensor<double, StaticSize<3>>, Tensor<double, StaticSize<4, 3>>>;

    MyLayer layer({ { { { 1, 3, 4 } }, { { 5, 7, 4 } }, { { 1, 3, 6 } }, { { 1, 3, 6 } } } }, { { 1, 2, 3 } });

    MyLayer::InputType input = { { 1, 2, 3 } };

    Tensor<double, MyLayer::OutputSize> output;

    layer.Forward(input, output);
}
