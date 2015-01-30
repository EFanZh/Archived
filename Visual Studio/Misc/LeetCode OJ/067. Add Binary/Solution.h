#pragma once

class Solution
{
public:
    string addBinary(string a, string b)
    {
        if (b.size() < a.size())
        {
            swap(a, b);
        }

        auto it2 = b.rbegin();

        for (auto it1 = a.rbegin(); it1 != a.crend(); ++it1, ++it2)
        {
            *it2 += *it1 - '0' * 2;
        }

        while (it2 != b.crend())
        {
            *it2 -= '0';
            ++it2;
        }

        uint8_t carry = 0;

        for (auto it = b.rbegin(); it != b.rend(); ++it)
        {
            uint8_t k = carry + *it;

            *it = '0' + k % 2;
            carry = k / 2;
        }

        while (carry != 0)
        {
            b.insert(b.begin(), '0' + carry % 2);
            carry /= 2;
        }

        while (b.size() > 1 && b.front() == '0')
        {
            b.erase(b.begin());
        }

        return b;
    }
};
