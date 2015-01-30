#pragma once

class Solution
{
public:
    int numDistinct(string S, string T)
    {
        if (S.length() < T.length())
        {
            return 0;
        }

        vector<int> cache(T.length());

        if (S.back() == T.back())
        {
            cache.back() = 1;
        }

        for (size_t i = S.length() - 2; i < S.length(); --i)
        {
            size_t last = min(T.length(), i + 1);

            for (size_t j = 0; j < last; ++j)
            {
                if (S[i] == T[j])
                {
                    cache[j] += j < T.length() - 1 ? cache[j + 1] : 1;
                }
            }
        }

        return cache.front();
    }
};
