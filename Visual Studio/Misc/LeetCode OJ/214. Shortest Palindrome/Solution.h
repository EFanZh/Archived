#pragma once

class Solution
{
    template <class T>
    static bool isPalindrome(T first, T last)
    {
        while (first < last)
        {
            if (get<0>(*first) != get<0>(*last) || get<1>(*first) != get<1>(*last))
            {
                return false;
            }

            ++first;
            --last;
        }

        return true;
    }

    static size_t findLongestPalindrome(const std::string &s)
    {
        vector<tuple<char, size_t, size_t>> count;

        for (size_t i = 0; i < s.length();)
        {
            char c = s[i];
            size_t t = 1;

            for (++i; i < s.length() && s[i] == c; ++i)
            {
                ++t;
            }

            count.emplace_back(c, t, i);
        }

        size_t floorToOdd = count.size() % 2 == 0 ? count.size() - 2 : count.size() - 1;

        for (size_t i = floorToOdd; i <= floorToOdd; i -= 2)
        {
            if (get<0>(count[i]) == get<0>(count.front()) && get<1>(count[i]) >= get<1>(count.front()) &&
                isPalindrome(count.cbegin() + 1, count.cbegin() + (i - 1)))
            {
                return get<2>(count[i]) - (get<1>(count[i]) - get<1>(count.front()));
            }
        }

        return 0; // Unused.
    }

public:
    string shortestPalindrome(const string &s)
    {
        if (s.length() < 2)
        {
            return s;
        }

        size_t extraLength = s.length() - findLongestPalindrome(s);
        string result;

        result.reserve(s.length() + extraLength);

        copy(s.crbegin(), s.crbegin() + extraLength, back_inserter(result));
        copy(s.cbegin(), s.cend(), back_inserter(result));

        return result;
    }
};
