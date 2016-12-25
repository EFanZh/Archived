#include "neural_networks/alexnet.h"

using namespace std;
using namespace neural_networks;

int main()
{
    using element_type = float;
    using network_type = alexnet<element_type>;

    auto network = make_unique<network_type>();
    const auto samples = make_unique<array<training_sample<tensor<element_type, 3, 224, 224>, std::size_t>, 1>>();

    network->train(samples->cbegin(), samples->cend(), 1, 0.01f);

    const auto input = make_unique<tensor<element_type, 3, 224, 224 >> ();
    auto output = make_unique<tensor<element_type, 1000>>();

    network->predict(*input, *output);
}
