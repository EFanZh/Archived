#pragma once

static const uint32_t fourPowers[] = { 1, 4, 16, 64, 256, 1024, 4096, 16384, 65536, 262144 };

class Solution
{
    static const size_t length = 10;

    static uint32_t toDigit(char ch)
    {
        switch (ch)
        {
            case 'C':
                return 1;

            case 'G':
                return 2;

            case 'T':
                return 3;

            default:
                return 0;
        }
    }

    static uint32_t toNumber(const char *seq, size_t n)
    {
        uint32_t result = 0;

        for (size_t i = 0; i < n; ++i)
        {
            result += toDigit(seq[i]) * fourPowers[i];
        }

        return result;
    }

    static string toSequence(uint32_t number)
    {
        string result;

        for (size_t i = 0; i < length; ++i)
        {
            result += "ACGT"[number % 4];
            number /= 4;
        }

        return result;
    }

public:
    vector<string> findRepeatedDnaSequences(string s)
    {
        vector<string> result;
        unordered_map<uint32_t, size_t> count;
        uint32_t current = toNumber(s.data(), length);

        ++count[current];

        for (size_t i = length; i < s.length(); ++i)
        {
            current /= 4;
            current += fourPowers[length - 1] * toDigit(s[i]);
            ++count[current];
        }

        for (auto &item : count)
        {
            if (item.second > 1)
            {
                result.emplace_back(toSequence(item.first));
            }
        }

        return result;
    }
};
