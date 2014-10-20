// https://code.google.com/codejam/contest/4214486/dashboard#s=p1

#include <algorithm>
#include <iostream>
#include <map>

using namespace std;

map<int, map<int, map<int, double>>> cache;

double get_value(int level, int row, int column, double value);

double get_overflow(int level, int row, int column, double value)
{
    if (row >= 0 && row < level && column >= 0 && column <= row)
    {
        return max(0.0, get_value(level, row, column, value) - 250.0);
    }
    else
    {
        return 0.0;
    }
}

double get_value(int level, int row, int column, double value)
{
    auto r1 = cache.find(level);
    if (r1 != cache.end())
    {
        auto r2 = r1->second.find(row);
        if (r2 != r1->second.end())
        {
            auto r3 = r2->second.find(column);
            if (r3 != r2->second.end())
            {
                return r3->second;
            }
        }
    }

    if (level == 1)
    {
        return value;
    }
    else
    {
        double overflow_1 = get_overflow(level - 1, row - 1, column - 1, value);
        double overflow_2 = get_overflow(level - 1, row - 1, column, value);
        double overflow_3 = get_overflow(level - 1, row, column, value);

        auto value = (overflow_1 + overflow_2 + overflow_3) / 3.0;
        cache[level][row][column] = value;

        return value;
    }
}

void process(istream &input, ostream &output)
{
    int bottles, level, n;
    input >> bottles >> level >> n;

    int row = 0;
    int first = 1;
    while (first + row + 1 <= n)
    {
        ++row;
        first += row;
    }

    cache.clear();

    double value = get_value(level, row, n - first, 750.0 * bottles);

    output.setf(ios_base::fixed, ios_base::floatfield);
    output.precision(7);
    output << ' ' << min(250.0, value) << '\n';
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
