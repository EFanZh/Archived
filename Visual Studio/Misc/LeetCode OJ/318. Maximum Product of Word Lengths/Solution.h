#pragma once

class Solution
{
    static uint32_t toNumber(const string &word)
    {
        auto result = uint32_t(0);

        for (auto letter : word)
        {
            result |= uint32_t(1) << (letter - 'a');
        }

        return result;
    }

public:
    int maxProduct(const vector<string> &words)
    {
        if (words.size() < 2)
        {
            return 0;
        }

        auto numbers = vector<pair<uint32_t, int>>();

        for (const auto &word : words)
        {
            const auto number = toNumber(word);

            const auto it = lower_bound(numbers.begin(), numbers.end(), number,
                                        [](const pair<uint32_t, int> &lhs, uint32_t rhs) { return lhs.first < rhs; });

            if (it == numbers.end())
            {
                numbers.emplace_back(number, static_cast<int>(word.length()));
            }
            else if (it->first == number)
            {
                it->second = max(it->second, static_cast<int>(word.length()));
            }
            else
            {
                numbers.emplace(it, number, static_cast<int>(word.length()));
            }
        }

        auto result = 0;

        for (auto it1 = numbers.cbegin(), end1 = numbers.cend() - 1; it1 != end1; ++it1)
        {
            for (auto it2 = it1 + 1, end2 = numbers.cend(); it2 != end2; ++it2)
            {
                if ((it1->first & it2->first) == 0)
                {
                    result = max(result, it1->second * it2->second);
                }
            }
        }

        return result;
    }
};
