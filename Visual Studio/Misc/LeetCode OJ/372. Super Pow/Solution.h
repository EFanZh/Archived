#pragma once

class Solution
{
    static constexpr auto target = 1337u;

    static unsigned int exp10NotReally(unsigned int base)
    {
        const auto k2 = (base * base) % target;
        const auto k5 = (k2 * k2 * base) % target;

        return k5 * k5;
    }

public:
    int superPow(int a, const vector<int> &b)
    {
        unsigned int mods[10];

        mods[0] = 1;
        mods[1] = a % target;
        mods[2] = (mods[1] * mods[1]) % target;
        mods[3] = (mods[2] * mods[1]) % target;
        mods[4] = (mods[3] * mods[1]) % target;
        mods[5] = (mods[4] * mods[1]) % target;
        mods[6] = (mods[5] * mods[1]) % target;
        mods[7] = (mods[6] * mods[1]) % target;
        mods[8] = (mods[7] * mods[1]) % target;
        mods[9] = (mods[8] * mods[1]) % target;

        auto result = mods[b.front()];

        for (auto it = b.cbegin() + 1, itEnd = b.cend(); it != itEnd; ++it)
        {
            result = (exp10NotReally(result) * mods[*it]) % target;
        }

        return static_cast<int>(result);
    }
};
