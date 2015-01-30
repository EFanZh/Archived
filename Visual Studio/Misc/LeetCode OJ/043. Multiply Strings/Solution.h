#pragma once

class Solution
{
public:
    string multiply(string num1, string num2)
    {
        vector<uint16_t> cache(num1.length() + num2.length(), 0);

        for (size_t i = 0; i < num1.length(); ++i)
        {
            for (size_t j = 0; j < num2.length(); ++j)
            {
                cache[i + j] += (num1[num1.length() - 1 - i] - '0') * (num2[num2.length() - 1 - j] - '0');
            }
        }

        for (size_t i = 0; i < cache.size(); ++i)
        {
            if (cache[i] >= 10)
            {
                cache[i + 1] += cache[i] / 10;
                cache[i] %= 10;
            }
        }

        while (cache.size() > 1 && cache.back() == 0)
        {
            cache.pop_back();
        }

        string s;

        transform(cache.crbegin(), cache.crend(), back_inserter(s), [](uint16_t n) { return '0' + n; });

        return s;
    }
};
