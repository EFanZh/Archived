#pragma once

class Solution
{
    template <class T, class TSize>
    static bool isAddEquals(T a, TSize aLength, T b, TSize bLength, T c, TSize cLength)
    {
        auto ra = reverse_iterator<T>(a + aLength);
        auto rb = reverse_iterator<T>(b + bLength);
        auto rc = reverse_iterator<T>(c + cLength);

        auto i = TSize(0);
        auto carry = 0;

        for (; i < aLength; ++i)
        {
            const auto lhs = ra[i] - '0';
            const auto rhs = rb[i] - '0';
            const auto result = lhs + rhs + carry;

            if (result % 10 == rc[i] - '0')
            {
                carry = result / 10;
            }
            else
            {
                return false;
            }
        }

        for (; i < bLength; ++i)
        {
            const auto result = rb[i] - '0' + carry;

            if (result % 10 == rc[i] - '0')
            {
                carry = result / 10;
            }
            else
            {
                return false;
            }
        }

        return i == cLength || carry == rc[i] - '0';
    }

    static bool isAdditiveNumberHelper(const string &num, string::size_type start, string::size_type length1,
                                       string::size_type length2)
    {
        while (start + length1 + length2 < num.length())
        {
            const auto requires = start + length1 + length2 + length2;

            if (requires <= num.length())
            {
                const auto a = num.begin() + start;
                const auto b = a + length1;
                const auto c = b + length2;

                if (*c != '0' && isAddEquals(a, length1, b, length2, c, length2))
                {
                    start += length1;
                    length1 = length2;
                }
                else if (requires < num.length() && *c != '0' && isAddEquals(a, length1, b, length2, c, length2 + 1))
                {
                    start += length1;
                    length1 = length2;
                    ++length2;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        return start + length1 + length2 == num.length();
    }

public:
    bool isAdditiveNumber(const string &num)
    {
        if (num.length() < 3)
        {
            return false;
        }

        if (num[0] == '0' && num[1] == '0')
        {
            return all_of(num.cbegin(), num.cend(), [](char c) { return c == '0'; });
        }

        const auto firstNumLength = num.front() == '0' ? string::size_type(1) : num.length() / 3;
        auto i = string::size_type(1);

        for (; i <= firstNumLength; ++i)
        {
            if (num[i] == '0')
            {
                const auto a = num.cbegin();
                const auto b = a + i;
                const auto c = b + 1;

                if (equal(a, b, c) && isAdditiveNumberHelper(num, i, 1, i))
                {
                    return true;
                }
            }
            else
            {
                const auto secondNumLength = (num.length() - i) / 2;
                auto j = string::size_type(1);

                for (; j < i; ++j)
                {
                    if (num[i + j] != '0')
                    {
                        const auto a = num.cbegin();
                        const auto b = a + i;
                        const auto c = b + j;

                        if ((isAddEquals(b, j, a, i, c, j) && isAdditiveNumberHelper(num, i, j, j)) ||
                            (i + j + j < num.length() && isAddEquals(b, j, a, i, c, j + 1) &&
                             isAdditiveNumberHelper(num, i, j, j + 1)))
                        {
                            return true;
                        }
                    }
                }

                for (; j <= secondNumLength; ++j)
                {
                    if (isAdditiveNumberHelper(num, 0, i, j))
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }
};
