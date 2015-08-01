#pragma once

class Solution
{
public:
    int countPrimes(int n)
    {
        if (n <= 2)
        {
            return 0;
        }
        else if (n == 3)
        {
            return 1;
        }

        vector<int> primes;

        primes.reserve((n - 2) / 2);
        for (int i = 3; i < n; i += 2)
        {
            primes.emplace_back(i);
        }

        int lastTestPrime = static_cast<int>(sqrt(n - 1));

        if (lastTestPrime < 3)
        {
            return static_cast<int>(primes.size()) + 1;
        }

        size_t lastTestPrimeIndex = (lastTestPrime - 3) / 2;

        for (size_t i = 0; i <= lastTestPrimeIndex; ++i)
        {
            int current = primes[i];

            if (current == 0)
            {
                continue;
            }

            int step = i * 2 + 3;

            for (size_t j = i + step; j < primes.size(); j += step)
            {
                primes[j] = 0;
            }
        }

        return (primes.size() - count(primes.cbegin(), primes.cend(), 0)) + 1;
    }
};
