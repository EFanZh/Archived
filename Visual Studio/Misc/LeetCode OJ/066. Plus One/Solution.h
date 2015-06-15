#pragma once

class Solution
{
public:
    vector<int> plusOne(vector<int> &digits)
    {
        vector<int> result(digits);

        int carry = 1;

        for (size_t i = digits.size() - 1; i < digits.size(); --i)
        {
            result[i] += carry;

            if (result[i] > 9)
            {
                result[i] = 0;
                carry = 1;
            }
            else
            {
                carry = 0;
            }
        }

        if (carry != 0)
        {
            result.emplace(result.cbegin(), 1);
        }

        return result;
    }
};
