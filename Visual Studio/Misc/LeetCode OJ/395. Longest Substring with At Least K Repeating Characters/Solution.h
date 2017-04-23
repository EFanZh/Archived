#pragma once

class Solution
{
    static array<int, 26> countCharacters(string::const_iterator first, string::const_iterator last)
    {
        auto result = array<int, 26>();

        for (auto it = first; it != last; ++it)
        {
            ++result[static_cast<decltype(result.size())>(*it - 'a')];
        }

        return result;
    }

    static string::const_iterator::difference_type helper(string::const_iterator first,
                                                          string::const_iterator last,
                                                          string::const_iterator::difference_type k)
    {
        const auto countOf = countCharacters(first, last);
        auto result = decltype(k)(0);

        using IndexType = decltype(countOf.size());

        if (all_of(first, last, [&](char c) { return countOf[static_cast<IndexType>(c - 'a')] >= k; }))
        {
            return last - first;
        }
        else
        {
            for (auto it = first;;)
            {
                for (;; ++it)
                {
                    if (it == last)
                    {
                        return result;
                    }
                    else if (countOf[static_cast<IndexType>(*it - 'a')] >= k)
                    {
                        break;
                    }
                }

                const auto splitFrom = it;

                for (++it;; ++it)
                {
                    if (it == last)
                    {
                        return max(result, helper(splitFrom, last, k));
                    }
                    else if (countOf[static_cast<decltype(countOf.size())>(*it - 'a')] < k)
                    {
                        break;
                    }
                }

                if (it - splitFrom > result)
                {
                    result = max(result, helper(splitFrom, it, k));
                }
            }
        }
    }

public:
    int longestSubstring(const string &s, int k)
    {
        return static_cast<int>(helper(s.cbegin(), s.cend(), static_cast<string::difference_type>(k)));
    }
};
