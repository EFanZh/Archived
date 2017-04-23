#pragma once

class Solution
{
    template <class Iterator>
    static void helper(Iterator first, Iterator last, int n, int led, vector<string> &result)
    {
        if (n == 0)
        {
            const auto hour = led / 64;

            if (hour < 12)
            {
                const auto minute = led % 64;

                if (minute < 60)
                {
                    auto oss = ostringstream();

                    oss << hour << ':' << setfill('0') << setw(2) << minute;

                    result.emplace_back(oss.str());
                }
            }
        }
        else
        {
            const auto end = last - (n - 1);

            for (auto it = first; it != end; ++it)
            {
                helper(it + 1, last, n - 1, led + *it, result);
            }
        }
    }

public:
    vector<string> readBinaryWatch(int num)
    {
        static const auto leds = { 1, 2, 4, 8, 16, 32, 64 * 1, 64 * 2, 64 * 4, 64 * 8 };

        auto result = vector<string>();

        helper(leds.begin(), leds.end(), num, 0, result);

        return result;
    }
};
