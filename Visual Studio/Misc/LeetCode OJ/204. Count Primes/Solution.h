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

        auto primes = vector<int>();

        primes.reserve((n - 2) / 2);

        for (auto i = 3; i < n; i += 2)
        {
            primes.emplace_back(i);
        }

        const auto lastTestPrime = static_cast<int>(sqrt(n - 1));

        if (lastTestPrime < 3)
        {
            return static_cast<int>(primes.size()) + 1;
        }

        const auto lastTestPrimeIndex = static_cast<size_t>((lastTestPrime - 3) / 2);

        for (auto i = size_t(0); i <= lastTestPrimeIndex; ++i)
        {
            auto current = primes[i];

            if (current == 0)
            {
                continue;
            }

            const auto step = i * 2 + 3;

            for (auto j = i + step; j < primes.size(); j += step)
            {
                primes[j] = 0;
            }
        }

        return static_cast<int>((primes.size() - count(primes.cbegin(), primes.cend(), 0)) + 1);
    }
};
