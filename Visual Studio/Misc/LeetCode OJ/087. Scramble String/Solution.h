#pragma once

class Solution
{
    template <class T>
    static bool isScrambleHelper(T first1, T last1, T first2, T last2)
    {
        size_t length = last1 - first1;

        if (length == 1)
        {
            return *first1 == *first2;
        }

        map<char, size_t> m1, m2;

        for (T it = first1; it != last1; ++it)
        {
            ++m1[*it];
        }
        for (T it = first2; it != last2; ++it)
        {
            ++m2[*it];
        }

        if (m1 != m2)
        {
            return false;
        }

        for (size_t i = 1; i < length; ++i)
        {
            if ((isScrambleHelper(first1, first1 + i, first2, first2 + i) &&
                 isScrambleHelper(first1 + i, last1, first2 + i, last2)) ||
                (isScrambleHelper(first1, first1 + i, last2 - i, last2) &&
                 isScrambleHelper(first1 + i, last1, first2, last2 - i)))
            {
                return true;
            }
        }

        return false;
    }

public:
    bool isScramble(string s1, string s2)
    {
        return isScrambleHelper(s1.cbegin(), s1.cend(), s2.cbegin(), s2.cend());
    }
};
