// https://code.google.com/codejam/contest/3214486/dashboard#s=p0

#include <algorithm>
#include <iostream>
#include <string>
#include <tuple>
#include <vector>
#include <cstdint>

using namespace std;

uint8_t digits[10];
char *digit_strs[10] =
{
    "1111110",
    "0110000",
    "1101101",
    "1111001",
    "0110011",
    "1011011",
    "1011111",
    "1110000",
    "1111111",
    "1111011",
};

uint8_t get_digit(const string &digit_str)
{
    uint8_t result = 0;

    for (int i = 0; i != 7; ++i)
    {
        if (digit_str[i] == '1')
        {
            result += 1 << i;
        }
    }

    return result;
}

string get_digit_str(uint8_t digit)
{
    string result;

    for (int i = 0; i != 7; ++i)
    {
        result += (digit >> i & 0x1) == 1 ? '1' : '0';
    }

    return result;
}

// Returns (is unambigous, is success, value).
tuple<bool, bool, uint8_t> get_next(const vector<uint8_t> &digit_sequence, int n)
{
    uint8_t good_digit = 0;
    uint8_t bad_digit = 0;

    for (auto digit : digit_sequence)
    {
        uint8_t compare_digit = digits[n];

        // If not match.
        if ((digit & compare_digit) != digit)
        {
            return make_tuple(true, false, 0);
        }
        // If bad becomes good.
        if ((bad_digit & digit) != 0)
        {
            return make_tuple(true, false, 0);
        }
        good_digit |= digit;

        // If good becomes bad.
        uint8_t new_bad_digit = compare_digit & ~(compare_digit & digit);
        if ((good_digit & new_bad_digit) != 0)
        {
            return make_tuple(true, false, 0);
        }
        bad_digit |= new_bad_digit;

        n = n == 0 ? 9 : n - 1;
    }

    uint8_t next_digit = digits[n];

    if ((next_digit & ~(good_digit | bad_digit)) != 0)
    {
        return make_tuple(false, true, 0);
    }
    else
    {
        return make_tuple(true, true, next_digit & good_digit);
    }
}

void process(istream &input, ostream &output)
{
    int digit_count;
    input >> digit_count;

    vector<uint8_t> digit_sequence;
    for (int i = 0; i != digit_count; ++i)
    {
        string digit_str;
        input >> digit_str;
        digit_sequence.emplace_back(get_digit(digit_str));
    }

    vector<uint8_t> possible_next_digits;
    bool is_failed = false;
    for (int n = 0; n != 10; ++n)
    {
        auto result = get_next(digit_sequence, n);
        if (!get<0>(result))
        {
            is_failed = true;
            break;
        }
        else if (get<1>(result))
        {
            possible_next_digits.emplace_back(get<2>(result));
        }
    }

    output << ' ';
    if (is_failed || !all_of(possible_next_digits.cbegin(), possible_next_digits.cend(), [&possible_next_digits](uint8_t d)
    {
        return d == possible_next_digits.front();
    }))
    {
        output << "ERROR!";
    }
    else
    {
        output << get_digit_str(possible_next_digits.front());
    }
    output << '\n';
}

int main()
{
    for (int i = 0; i != 10; ++i)
    {
        digits[i] = get_digit(digit_strs[i]);
    }

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
