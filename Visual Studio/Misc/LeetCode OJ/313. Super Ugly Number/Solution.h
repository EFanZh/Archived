#pragma once

class Solution
{
public:
    int nthSuperUglyNumber(int n, const vector<int> &primes)
    {
        auto uglyNumbers = vector<int>(n, numeric_limits<int>::max());
        auto baseIndexes = vector<vector<int>::size_type>(primes.size());

        uglyNumbers.front() = 1;

        for (auto i = vector<int>::size_type(1); i < uglyNumbers.size(); ++i)
        {
            for (auto j = vector<int>::size_type(0); j < primes.size(); ++j)
            {
                if (uglyNumbers[baseIndexes[j]] * primes[j] <= uglyNumbers[i - 1])
                {
                    ++baseIndexes[j];
                }

                uglyNumbers[i] = min(uglyNumbers[i], uglyNumbers[baseIndexes[j]] * primes[j]);
            }
        }

        return uglyNumbers.back();
    }
};
