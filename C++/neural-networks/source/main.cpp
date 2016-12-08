// #include <NeuralNetworks/ConvolutionalLayer.h>
// #include <NeuralNetworks/StaticPadding.h>

#include <iostream>

using namespace std;
// using namespace NeuralNetworks;

int main()
{

    // using MyLayer = ConvolutionalLayer<Matrix<Vector<double, 3>, StaticSize<5>>,
    //                                    Matrix<Vector<double, 3>, StaticSize<3>>,
    //                                    StaticPadding<1>,
    //                                    StaticSize<2>>;
    //
    // const auto layer = MyLayer({ { { { { -1, -1, 0 }, { 1, -1, 0 }, { 0, 0, -1 } } },
    //                                { { { 0, 0, 0 }, { 1, 0, 1 }, { 0, 0, 0 } } },
    //                                { { { 0, 0, 1 }, { 1, -1, -1 }, { 1, 0, -1 } } } } },
    //                            1);
    // const auto input =
    //     MyLayer::InputType{ { { { { 0, 1, 2 }, { 1, 0, 1 }, { 1, 2, 2 }, { 0, 2, 0 }, { 2, 0, 0 } } },
    //                           { { { 2, 0, 1 }, { 2, 0, 0 }, { 2, 0, 0 }, { 2, 2, 1 }, { 1, 0, 0 } } },
    //                           { { { 1, 1, 0 }, { 0, 2, 2 }, { 0, 1, 1 }, { 2, 2, 0 }, { 0, 1, 1 } } },
    //                           { { { 0, 1, 0 }, { 1, 0, 1 }, { 1, 0, 2 }, { 0, 0, 2 }, { 0, 0, 2 } } },
    //                           { { { 1, 1, 2 }, { 2, 2, 1 }, { 0, 1, 0 }, { 0, 1, 0 }, { 2, 1, 1 } } } } };
    //
    // auto output = Matrix<double, MyLayer::OutputSize>();
    //
    // layer.Forward(input, output);
    //
    // for (const auto &a : output)
    // {
    //     for (const auto &b : a)
    //     {
    //         cout.width(2);
    //         cout << b << ' ';
    //     }
    //
    //     cout << '\n';
    // }
}
