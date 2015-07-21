#pragma once

class Solution
{
public:
    int longestConsecutive(vector<int> &nums)
    {
        int result = 0;
        unordered_map<int, int> heads, tails;
        unordered_set<int> uniqueNums(nums.cbegin(), nums.cend());

        for (int num : uniqueNums)
        {
            auto it1 = heads.find(num + 1);
            auto it2 = tails.find(num - 1);

            if (it1 == heads.end())
            {
                if (it2 == tails.end())
                {
                    heads.emplace(num, num);
                    tails.emplace(num, num);
                    result = max(result, 1);
                }
                else
                {
                    int head = it2->second;

                    ++heads[head];
                    tails.erase(it2);
                    tails.emplace(num, head);
                    result = max(result, num - head + 1);
                }
            }
            else
            {
                if (it2 == tails.end())
                {
                    int tail = it1->second;

                    --tails[tail];
                    heads.erase(it1);
                    heads.emplace(num, tail);
                    result = max(result, tail - num + 1);
                }
                else
                {
                    int newHead = it2->second;
                    int newTail = it1->second;

                    heads[newHead] = newTail;
                    tails[newTail] = newHead;
                    heads.erase(it1);
                    tails.erase(it2);
                    result = max(result, newTail - newHead + 1);
                }
            }
        }

        return result;
    }
};
