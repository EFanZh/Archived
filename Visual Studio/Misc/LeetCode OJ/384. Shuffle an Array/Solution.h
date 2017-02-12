#pragma once

class Solution
{
    vector<int> nums;

public:
    Solution(const vector<int> &nums) : nums(nums)
    {
    }

    /** Resets the array to its original configuration and return it. */
    vector<int> reset()
    {
        return nums;
    }

    /** Returns a random shuffling of the array. */
    vector<int> shuffle()
    {
        auto result = nums;

        random_shuffle(result.begin(), result.end());

        return result;
    }
};

/**
 * Your Solution object will be instantiated and called as such:
 * Solution obj = new Solution(nums);
 * vector<int> param_1 = obj.reset();
 * vector<int> param_2 = obj.shuffle();
 */
