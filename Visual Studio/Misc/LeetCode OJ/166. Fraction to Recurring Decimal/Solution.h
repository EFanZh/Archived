#pragma once

class Solution
{
public:
    string fractionToDecimal(int numerator, int denominator)
    {
        if (numerator == 0)
        {
            return "0";
        }

        long long newNumerator = numerator, newDenominator = denominator;
        long long intPart = newNumerator / newDenominator;
        long long remain = abs(newNumerator % newDenominator);
        string result = to_string(intPart);

        if (remain == 0)
        {
            return result;
        }

        if (intPart == 0 && (numerator < 0) != (denominator < 0))
        {
            result.insert(result.begin(), '-');
        }

        result += '.';
        newDenominator = abs(newDenominator);

        unordered_map<long long, size_t> seen;

        while (remain > 0)
        {
            auto it = seen.find(remain);

            if (it == seen.cend())
            {
                seen.emplace(remain, result.size());
            }
            else
            {
                result.insert(result.begin() + it->second, '(');
                result += ')';

                break;
            }

            remain *= 10;
            while (remain < newDenominator)
            {
                result += '0';
                seen.emplace(remain, result.size());
                remain *= 10;
            }
            result += static_cast<char>('0' + remain / newDenominator);
            remain %= newDenominator;
        }

        return result;
    }
};
