#pragma once

class Solution
{
public:
    vector<int> largestDivisibleSubset(const vector<int> &nums)
    {
        if (nums.size() < 2)
        {
            return nums;
        }

        auto sortedNums = nums;

        sort(sortedNums.begin(), sortedNums.end());

        const auto invalidIndex = numeric_limits<size_t>::max();
        const auto numCount = sortedNums.size();
        auto cache = vector<pair<size_t, size_t>>(numCount);
        auto maxIndex = size_t(0);

        for (size_t i = 0; i < numCount; ++i)
        {
            auto cacheItem = pair<size_t, size_t>(invalidIndex, 0);

            for (size_t j = 0; j < i; ++j)
            {
                if (sortedNums[i] % sortedNums[j] == 0 && cache[j].second > cacheItem.second)
                {
                    cacheItem = { j, cache[j].second };
                }
            }

            cache[i] = { cacheItem.first, cacheItem.second + 1 };

            if (cache[i].second > cache[maxIndex].second)
            {
                maxIndex = i;
            }
        }

        auto result = vector<int>();

        result.reserve(cache[maxIndex].second);

        for (auto i = maxIndex; i != invalidIndex; i = cache[i].first)
        {
            result.emplace_back(sortedNums[i]);
        }

        return result;
    }
};
