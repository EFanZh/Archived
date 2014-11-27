// https://code.google.com/codejam/contest/6214486/dashboard#s=p1

#include <iostream>
#include <queue>
#include <string>
#include <tuple>
#include <unordered_map>
#include <unordered_set>
#include <vector>

using namespace std;

void process(istream &input, ostream &output)
{
    int bus_count;
    input >> bus_count;

    unordered_map<int, int> cache;
    for (int i = 0; i != bus_count; ++i)
    {
        int from;
        int to;

        input >> from >> to;
        for (int i = from; i <= to; ++i)
        {
            ++cache[i];
        }
    }

    int city_count;
    input >> city_count;

    for (int i = 0; i != city_count; ++i)
    {
        int city;
        input >> city;
        output << ' ' << cache[city];
    }
    output << '\n';
}

int main()
{
    istream &input = cin;
    ostream &output = cout;

    int case_count;
    input >> case_count;

    for (int i = 1; i <= case_count; ++i)
    {
        output << "Case #" << i << ":";
        process(input, output);
    }
}
