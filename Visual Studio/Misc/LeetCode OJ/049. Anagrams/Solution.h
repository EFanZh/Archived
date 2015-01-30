#pragma once

class Solution
{
public:
    vector<string> anagrams(vector<string> &strs)
    {
        map<string, vector<size_t>> dict;

        for (size_t i = 0; i < strs.size(); i++)
        {
            string k = strs[i];

            sort(k.begin(), k.end());
            dict[k].emplace_back(i);
        }

        vector<string> result;

        for (auto &p : dict)
        {
            if (p.second.size() > 1)
            {
                for (auto i : p.second)
                {
                    result.emplace_back(strs[i]);
                }
            }
        }

        return result;
    }
};
