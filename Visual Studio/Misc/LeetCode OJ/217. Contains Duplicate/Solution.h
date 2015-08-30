#pragma once

class Solution
{
public:
    bool containsDuplicate(const vector<int> &nums)
    {
        unordered_set<int> s;

        for (auto i : nums)
        {
            if (!s.emplace(i).second)
            {
                return true;
            }
        }

        return false;
    }
};
