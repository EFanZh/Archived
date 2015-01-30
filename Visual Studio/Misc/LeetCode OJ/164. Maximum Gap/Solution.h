#pragma once

class Solution
{
    static int CeilDivide(int x, int y)
    {
        return (x + y - 1) / y;
    }

public:
    int maximumGap(vector<int> &num)
    {
        if (num.size() < 2)
        {
            return 0;
        }

        auto k = minmax_element(num.cbegin(), num.cend());
        auto bucketSize = CeilDivide(*k.second - *k.first, num.size() - 1);
        vector<pair<int, int>> buckets(CeilDivide(*k.second - *k.first + 1, bucketSize), make_pair(-1, -1));

        for (auto i : num)
        {
            auto bucket = (i - *k.first) / bucketSize;

            if (buckets[bucket].first == -1)
            {
                buckets[bucket].first = i;
                buckets[bucket].second = i;
            }
            else if (i < buckets[bucket].first)
            {
                buckets[bucket].first = i;
            }
            else if (i > buckets[bucket].second)
            {
                buckets[bucket].second = i;
            }
        }

        int result = 0;

        for (size_t i = 0; i < buckets.size() - 1;)
        {
            size_t j = i + 1;

            while (buckets[j].first == -1)
            {
                ++j;
            }

            result = max(result, buckets[j].first - buckets[i].second);

            i = j;
        }

        return result;
    }
};
