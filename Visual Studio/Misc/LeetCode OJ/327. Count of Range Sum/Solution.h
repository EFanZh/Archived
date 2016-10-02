#pragma once

class Solution
{
    struct Environment
    {
        int lower;
        int upper;
        vector<long> buffer;
        int result;
    };

    static vector<long> getSums(const vector<int> &nums)
    {
        vector<long> result(nums.size() + 1);

        for (auto i = vector<long>::size_type(0); i < nums.size(); ++i)
        {
            result[i + 1] = result[i] + nums[i];
        }

        return result;
    }

    template <class Iterator, class T>
    static int countRange(Iterator first, Iterator last, const T &lower, const T &upper)
    {
        auto it = lower_bound(first, last, lower);

        return static_cast<int>(upper_bound(it, last, upper) - it);
    }

    template <class Iterator>
    static void mergeSort(Environment &e, Iterator first, Iterator last)
    {
        if (last - first > 1)
        {
            const auto middle = first + (last - first) / 2;

            mergeSort(e, first, middle);
            mergeSort(e, middle, last);

            auto right = middle;
            auto lower = middle;
            auto upper = middle;
            auto bufferIterator = e.buffer.begin();

            for (auto left = first; left != middle; ++left)
            {
                const auto leftValue = *left;
                const auto lowerValue = leftValue + e.lower;

                while (lower < last && *lower < lowerValue)
                {
                    ++lower;
                }

                const auto upperValue = leftValue + e.upper;

                while (upper < last && *upper <= upperValue)
                {
                    ++upper;
                }

                e.result += static_cast<int>(upper - lower);

                while (right != last && *right < leftValue)
                {
                    *bufferIterator = *right;

                    ++bufferIterator;
                    ++right;
                }

                *bufferIterator = leftValue;

                ++bufferIterator;
            }

            move(e.buffer.begin(), bufferIterator, first);
        }
    }

public:
    int countRangeSum(const vector<int> &nums, int lower, int upper)
    {
        auto sums = getSums(nums);
        auto e = Environment{ lower, upper, vector<long>(sums.size()), 0 };

        mergeSort(e, sums.begin(), sums.end());

        return e.result;
    }
};
