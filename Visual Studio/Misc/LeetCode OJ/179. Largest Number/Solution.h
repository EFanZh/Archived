#pragma once

class Solution
{
public:
    string largestNumber(vector<int> &num)
    {
        vector<string> strs;

        transform(num.cbegin(), num.cend(), back_inserter(strs), static_cast<string (&)(int)>(to_string));
        sort(strs.begin(), strs.end(), [](const string &lhs, const string &rhs) {
            size_t i = 0, j = 0;

            auto left = lhs.cbegin();
            auto right = rhs.cbegin();

            while (left != rhs.cend())
            {
                if (*left < *right)
                {
                    return false;
                }
                else if (*right < *left)
                {
                    return true;
                }

                ++left;
                ++right;

                if (left == lhs.cend())
                {
                    left = rhs.cbegin();
                }
                if (right == rhs.cend())
                {
                    right = lhs.cbegin();
                }
            }

            return false;
        });

        if (strs.front().front() == '0')
        {
            return "0";
        }
        else
        {
            string result;

            for (auto &s : strs)
            {
                result += s;
            }

            return result;
        }
    }
};
