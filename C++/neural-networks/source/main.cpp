#include "neural_networks/alexnet.h"

using namespace neural_networks;

int main()
{
    auto net = alexnet();

    net.train({});
}
