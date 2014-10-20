// https://code.google.com/codejam/contest/4214486/dashboard#s=p3

#include <iostream>
#include <map>
#include <cstdint>

using namespace std;

map<int, map<int, uintmax_t>> count_cache;

uintmax_t add(uintmax_t a, uintmax_t b)
{
    if (b < numeric_limits<uintmax_t>::max() - a)
    {
        return a + b;
    }
    else
    {
        return numeric_limits<uintmax_t>::max();
    }
}

uintmax_t get_count(int rest_length, int stack_size)
{
    auto r1 = count_cache.find(rest_length);
    if (r1 != count_cache.end())
    {
        auto r2 = r1->second.find(stack_size);
        if (r2 != r1->second.end())
        {
            return r2->second;
        }
    }

    uintmax_t result;

    if (rest_length < stack_size)
    {
        return 0;
    }
    else
    {
        if (stack_size == 0)
        {
            if (rest_length == 0)
            {
                return 1;
            }
            else
            {
                result = get_count(rest_length - 1, stack_size + 1);
            }
        }
        else
        {
            result = add(get_count(rest_length - 1, stack_size - 1), get_count(rest_length - 1, stack_size + 1));
        }
    }

    count_cache[rest_length][stack_size] = result;
    return result;
}

void output_result(ostream &output, int n, uintmax_t k)
{
    int rest_length = n * 2;

    if (get_count(rest_length, 0) < k)
    {
        output << "Doesn't Exist!";
    }
    else
    {
        int stack_size = 0;

        while (rest_length > 0)
        {
            uintmax_t count = get_count(rest_length - 1, stack_size + 1);
            if (count >= k)
            {
                cout << '(';
                ++stack_size;
            }
            else
            {
                cout << ')';
                --stack_size;
                k -= count;
            }
            --rest_length;
        }
    }
}

void process(istream &input, ostream &output)
{
    int n;
    uintmax_t k;

    input >> n >> k;

    output << ' ';
    output_result(output, n, k);
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
