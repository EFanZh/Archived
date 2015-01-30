#pragma once

class Solution
{
    static size_t collapse(const string &s, vector<pair<size_t, size_t>> &segments, size_t i, size_t j)
    {
        for (size_t k = segments.size() - 1;; --k)
        {
            // Expand.
            while (i - 1 < s.length() && j < s.length() && s[i - 1] == '(' && s[j] == ')')
            {
                --i;
                ++j;
            }

            // Merge.
            if (k < segments.size() && segments[k].second == i)
            {
                i = segments[k].first;
            }
            else if (k == segments.size() - 1)
            {
                segments.emplace_back(i, j);

                break;
            }
            else
            {
                segments.erase(segments.begin() + (k + 2), segments.end());

                break;
            }
        }

        segments.back() = make_pair(i, j);

        return j;
    }

public:
    int longestValidParentheses(string s)
    {
        vector<pair<size_t, size_t>> segments;

        for (size_t i = 0; i + 1 < s.length();)
        {
            if (s[i] == '(' && s[i + 1] == ')')
            {
                i = collapse(s, segments, i, i + 2);
            }
            else
            {
                ++i;
            }
        }

        size_t result = 0;

        for (auto item : segments)
        {
            if (item.second - item.first > result)
            {
                result = item.second - item.first;
            }
        }

        return result;
    }
};
