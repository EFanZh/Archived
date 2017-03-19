#pragma once

class Solution
{
    static vector<int> getMaxNumber(const vector<int> &nums, size_t n)
    {
        auto result = vector<int>();

        if (n > 0)
        {
            result.reserve(n);

            result.emplace_back(nums.front());

            for (auto i = size_t(1); i < nums.size(); ++i)
            {
                if (nums.size() - i == n - result.size())
                {
                    result.insert(result.cend(), nums.cbegin() + i, nums.cend());

                    break;
                }
                else if (nums[i] > result.back())
                {
                    auto k = nums.size() - 2;

                    while (k < nums.size() && nums[i] > result[k])
                    {
                        --k;
                    }

                    result[k + 1] = nums[i];

                    result.erase(result.cbegin() + k + 2, result.cend());
                }
            }
        }

        return result;
    }

    static size_t findNextIncrease(const vector<int> &nums, size_t start)
    {
        if (start == 0)
        {
            ++start;
        }

        while (start < nums.size() && nums[start - 1] >= nums[start])
        {
            ++start;
        }

        return start;
    }

    static vector<vector<int>> getMaxNumbers(const vector<int> &nums, size_t minCount, size_t maxCount)
    {
        auto result = vector<vector<int>>(maxCount - minCount + 1);

        result.back() = getMaxNumber(nums, maxCount);

        auto previousDecreaseSequence = size_t(1);

        for (auto i = result.size() - 2; i < result.size(); --i)
        {
            result[i].reserve(i);

            auto increase = findNextIncrease(result[i + 1], previousDecreaseSequence);

            previousDecreaseSequence = increase - 1;

            result[i].insert(result[i].cend(), result[i + 1].cbegin(), result[i + 1].cbegin() + increase - 1);
            result[i].insert(result[i].cend(), result[i + 1].cbegin() + increase, result[i + 1].cend());
        }

        return result;
    }

    static vector<int> mergeNumber(const vector<int> &num1, const vector<int> &num2)
    {
        auto result = vector<int>();

        result.reserve(num1.size() + num2.size());

        auto i = vector<int>::size_type(0);
        auto j = vector<int>::size_type(0);

        if (i < num1.size())
        {
            goto JUnknown;
        }
        else
        {
            goto IEnd;
        }

    IUnknown:
        if (i < num1.size())
        {
            goto BothKnown;
        }
        else
        {
            goto IEnd;
        }

    IEnd:
        result.insert(result.cend(), num2.cbegin() + j, num2.cend());

        goto End;

    JUnknown:
        if (j < num2.size())
        {
            goto BothKnown;
        }
        else
        {
            result.insert(result.cend(), num1.cbegin() + i, num1.cend());

            goto End;
        }

    BothKnown:
        if (lexicographical_compare(num1.cbegin() + i, num1.cend(), num2.cbegin() + j, num2.cend()))
        {
            result.emplace_back(num2[j]);
            ++j;

            goto JUnknown;
        }
        else
        {
            result.emplace_back(num1[i]);
            ++i;

            goto IUnknown;
        }

    End:
        return result;
    }

    // Precondition: nums1.size() <= nums2.size() && k > 0 && k < numic_limits<size_t>::max()
    static vector<int> maxNumberHelper(const vector<int> &nums1, const vector<int> &nums2, size_t k)
    {
        auto maxNumbers1 = vector<vector<int>>();
        auto maxNumbers2 = vector<vector<int>>();
        auto result = vector<int>();

        if (k < nums1.size())
        {
            maxNumbers1 = getMaxNumbers(nums1, 0, k);
            maxNumbers2 = getMaxNumbers(nums2, 0, k);
        }
        else if (k < nums2.size())
        {
            maxNumbers1 = getMaxNumbers(nums1, 0, nums1.size());
            maxNumbers2 = getMaxNumbers(nums2, k - nums1.size(), k);
        }
        else
        {
            maxNumbers1 = getMaxNumbers(nums1, k - nums2.size(), nums1.size());
            maxNumbers2 = getMaxNumbers(nums2, k - nums1.size(), nums2.size());
        }

        for (auto i = size_t(0); i < maxNumbers1.size(); ++i)
        {
            result = max(result, mergeNumber(maxNumbers1[i], maxNumbers2[maxNumbers2.size() - 1 - i]));
        }

        return result;
    }

public:
    vector<int> maxNumber(const vector<int> &nums1, const vector<int> &nums2, int k)
    {
        if (k == 0)
        {
            return {};
        }
        else if (nums1.size() <= nums2.size())
        {
            return maxNumberHelper(nums1, nums2, k);
        }
        else
        {
            return maxNumberHelper(nums2, nums1, k);
        }
    }
};
