// https://code.google.com/codejam/contest/4214486/dashboard#s=p0

#include <iostream>
#include <map>
#include <cstdint>

using namespace std;

static const int x = 1000000007;
map<int, map<int, uintmax_t>> count_cache;
map<int, map<int, uintmax_t>> solve_cache;

uintmax_t get_count(int n, int position_count)
{
    auto r1 = count_cache.find(n);
    if (r1 != count_cache.end())
    {
        auto r2 = r1->second.find(position_count);
        if (r2 != r1->second.end())
        {
            return r2->second;
        }
    }

    if (n == 1)
    {
        return position_count + 1;
    }
    else
    {
        uintmax_t result = 0;

        for (int first = 0; first <= position_count; ++first)
        {
            result = (result + get_count(n - 1, position_count - first)) % x;
        }

        count_cache[n][position_count] = result;

        return result;
    }
}

uintmax_t solve(int char_count, int password_length)
{
    auto r1 = solve_cache.find(char_count);
    if (r1 != solve_cache.end())
    {
        auto r2 = r1->second.find(password_length);
        if (r2 != r1->second.end())
        {
            return r2->second;
        }
    }

    if (char_count == 1)
    {
        return 1;
    }
    else
    {
        int max = password_length - char_count + 1;
        uintmax_t result = 0;

        for (int i = 1; i <= max; ++i)
        {
            result = (result + get_count(i, password_length - i) * solve(char_count - 1, password_length - i)) % x;
        }

        solve_cache[char_count][password_length] = result;

        return result;
    }
}

void process(istream &input, ostream &output)
{
    int char_count, password_length;
    input >> char_count >> password_length;

    output << ' ' << solve(char_count, password_length) << '\n';
}

void main()
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
