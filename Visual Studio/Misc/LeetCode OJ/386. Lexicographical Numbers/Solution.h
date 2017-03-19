#pragma once

class Solution
{
public:
    vector<int> lexicalOrder(int n)
    {
        auto current = 1;
        auto result = vector<int>(n);

        result.front() = current;

        for (auto i = 1; i < n; ++i)
        {
            auto next = current * 10;

            if (next <= n)
            {
                current = next;
            }
            else
            {
                if (current == n)
                {
                    current /= 10;
                }

                ++current;

                while (current % 10 == 0)
                {
                    current /= 10;
                }
            }

            result[i] = current;
        }

        return result;
    }
};
