#include <iostream>
#include <random>
#include <vector>
#include <cmath>

using namespace std;

default_random_engine re{ random_device{}() };
uniform_real_distribution<> generator;

bool get_result(double p)
{
    return generator(re) < p;
}

class my_random
{
    double p;
    bool is_success = false;
    int n = 0;

public:
    my_random(double p) : p(p)
    {
    }

    bool operator()()
    {
        if (is_success)
        {
            is_success = get_result(pow(p, n + 1));
            n = is_success ? n + 1 : 0;
        }
        else
        {
            is_success = get_result(1.0 - pow(1.0 - p, n + 1));
            n = is_success ? 0 : n + 1;
        }

        return is_success;
    }
};

template<class T>
vector<int> test(T random, int count)
{
    vector<int> result;

    bool is_success = random();
    int n = 1;
    for (int i = 1; i != count; ++i)
    {
        if (random() == is_success)
        {
            ++n;
        }
        else
        {
            result.emplace_back(n);
            is_success = !is_success;
            n = 1;
        }
    }
    result.emplace_back(n);

    return result;
}

void output(vector<int> data)
{
    for (auto i : data)
    {
        cout << i << '\n';
    }
}

int main()
{
    int count = 1000;

    output(test(my_random(0.5), count));
}
