#pragma once

class Solution
{
    template <class T>
    static int findKthLargestHelper(T first, T last, size_t k)
    {
        int p = *first;
        auto it =
            stable_partition(first, last, [=](int x) { return x > p; }); // Wrong answer if std::partition is used.
        size_t leftSize = it - first;

        if (leftSize < k)
        {
            return findKthLargestHelper(it + 1, last, k - leftSize - 1);
        }
        else if (k < leftSize)
        {
            return findKthLargestHelper(first, it, k);
        }
        else
        {
            return p;
        }
    }

public:
    int findKthLargest(vector<int> &nums, int k)
    {
        return findKthLargestHelper(nums.begin(), nums.end(), k - 1);
    }
};
