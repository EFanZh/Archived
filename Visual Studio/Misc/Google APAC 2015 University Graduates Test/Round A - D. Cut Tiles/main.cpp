// https://code.google.com/codejam/contest/3214486/dashboard#s=p3

#include <algorithm>
#include <iostream>
#include <vector>
#include <cstdint>

using namespace std;

void fit(vector<int> &tiles, int available_size, uintmax_t available_count)
{
    if (tiles.size() == 0 || available_size < 0 || available_count == 0)
    {
        return;
    }
    else
    {
        for (auto it = tiles.begin(); it != tiles.end();)
        {
            if (*it == available_size)
            {
                it = tiles.erase(it);
                --available_count;
                if (available_count == 0)
                {
                    break;
                }
            }
            else
            {
                ++it;
            }
        }
        fit(tiles, available_size - 1, available_count * 4);
    }
}

void cut(vector<int> &tiles, int size)
{
    if (tiles.size() == 0 || size == 0)
    {
        return;
    }
    else
    {
        int available_size = 0;
        int actual_available_size = 1;
        while ((size & actual_available_size) == 0)
        {
            ++available_size;
            actual_available_size <<= 1;
        }

        fit(tiles, available_size, size / actual_available_size * 2 - 1);
        cut(tiles, size - actual_available_size);
    }
}

void process(istream &input, ostream &output)
{
    int need_count, tile_size;
    input >> need_count >> tile_size;

    vector<int> tiles;
    tiles.resize(need_count);

    for (int i = 0; i != need_count; ++i)
    {
        input >> tiles[i];
    }

    int result = 0;
    while (!tiles.empty())
    {
        ++result;
        cut(tiles, tile_size);
    }

    output << ' ' << result << '\n';
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
