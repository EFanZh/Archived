// https://code.google.com/codejam/contest/4214486/dashboard#s=p2

#include <iostream>
#include <map>
#include <vector>

using namespace std;

map<vector<int>::const_iterator, map<vector<int>::const_iterator, int>> cache;
int k;

int solve(vector<int>::const_iterator it_begin, vector<int>::const_iterator it_end)
{
    auto r1 = cache.find(it_begin);
    if (r1 != cache.cend())
    {
        auto r2 = r1->second.find(it_end);
        if (r2 != r1->second.cend())
        {
            return r2->second;
        }
    }

    int result = it_end - it_begin;
    if (result >= 3)
    {
        for (auto it_1 = it_begin; it_1 != it_end - 2; ++it_1)
        {
            for (auto it_2 = it_1 + 1; it_2 != it_end - 1; ++it_2)
            {
                if (*it_2 - *it_1 == k && solve(it_1 + 1, it_2) == 0)
                {
                    for (auto it_3 = it_2 + 1; it_3 != it_end; ++it_3)
                    {
                        if (*it_3 - *it_2 == k && solve(it_2 + 1, it_3) == 0)
                        {
                            int new_result = solve(it_begin, it_1) + solve(it_3 + 1, it_end);
                            if (new_result < result)
                            {
                                result = new_result;
                            }
                        }
                    }
                }
            }
        }
    }

    cache[it_begin][it_end] = result;
    return result;
}

void process(istream &input, ostream &output)
{
    int card_count;
    input >> card_count >> k;

    vector<int> cards;
    cards.resize(card_count);

    for (int i = 0; i != card_count; ++i)
    {
        input >> cards[i];
    }

    cache.clear();
    output << ' ' << solve(cards.cbegin(), cards.cend()) << '\n';
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
