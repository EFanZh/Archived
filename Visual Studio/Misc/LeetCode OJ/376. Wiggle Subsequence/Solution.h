#pragma once

class Solution
{
public:
    int wiggleMaxLength(const vector<int> &nums)
    {
        const auto length = nums.size();

        if (length < 2)
        {
            return static_cast<int>(length);
        }
        else
        {
            auto i = size_t(1);
            int result;

            for (;;)
            {
                if (nums[i] == nums[i - 1])
                {
                    ++i;

                    if (i == length)
                    {
                        return 1;
                    }
                }
                else
                {
                    result = 2;

                    if (nums[i] > nums[i - 1])
                    {
                        ++i;

                        goto Increasing;
                    }
                    else
                    {
                        ++i;

                        goto Decreasing;
                    }
                }
            }

        Increasing:
            for (;; ++i)
            {
                if (i == length)
                {
                    return result;
                }
                else if (nums[i] < nums[i - 1])
                {
                    ++result;
                    ++i;

                    break;
                }
            }

        Decreasing:
            for (;; ++i)
            {
                if (i == length)
                {
                    return result;
                }
                else if (nums[i] > nums[i - 1])
                {
                    ++result;
                    ++i;

                    goto Increasing;
                }
            }
        }
    }
};
