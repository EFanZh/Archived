#pragma once

class Solution
{
public:
    vector<pair<int, int>> kSmallestPairs(const vector<int> &nums1, const vector<int> &nums2, int k)
    {
        const auto target = min<size_t>(nums1.size() * nums2.size(), k);
        auto result = vector<pair<int, int>>{};

        if (target > 0)
        {
            using QueueItem = pair<size_t, size_t>;

            const auto comparer = [&](const QueueItem &lhs, const QueueItem &rhs) {
                return nums1[rhs.first] + nums2[rhs.second] < nums1[lhs.first] + nums2[lhs.second];
            };

            auto q = priority_queue<QueueItem, vector<QueueItem>, decltype(comparer)>(comparer);

            q.emplace(0, 0);

            for (;;)
            {
                const auto current = q.top();

                result.emplace_back(nums1[current.first], nums2[current.second]);

                if (result.size() == target)
                {
                    break;
                }

                q.pop();

                if (current.second < nums2.size() - 1)
                {
                    q.emplace(current.first, current.second + 1);
                }

                if (current.second == 0 && current.first < nums1.size() - 1)
                {
                    q.emplace(current.first + 1, current.second);
                }
            }
        }

        return result;
    }
};
