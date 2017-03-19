#pragma once

class Solution
{
    template <class T>
    static void addOperatorsHelper(string &base,
                                   intmax_t value,
                                   intmax_t lastTerm,
                                   T first,
                                   T last,
                                   int target,
                                   vector<string> &result)
    {
        if (first == last)
        {
            if (value == target)
            {
                result.emplace_back(base);
            }

            return;
        }

        if (*first == '0')
        {
            base += "+0";
            addOperatorsHelper(base, value, 0, first + 1, last, target, result);

            base.end()[-2] = '-';
            addOperatorsHelper(base, value, 0, first + 1, last, target, result);

            base.end()[-2] = '*';
            addOperatorsHelper(base, value - lastTerm, 0, first + 1, last, target, result);

            base.erase(base.end() - 2, base.end());
        }
        else
        {
            intmax_t termValue = 0;
            size_t operatorIndex = base.size();

            base += '\0';

            for (auto it = first; it < last; ++it)
            {
                base += *it;
                termValue *= 10;
                termValue += *it - '0';

                base[operatorIndex] = '+';
                addOperatorsHelper(base, value + termValue, termValue, it + 1, last, target, result);

                base[operatorIndex] = '-';
                addOperatorsHelper(base, value - termValue, -termValue, it + 1, last, target, result);

                base[operatorIndex] = '*';
                addOperatorsHelper(base,
                                   value + lastTerm * (termValue - 1),
                                   lastTerm * termValue,
                                   it + 1,
                                   last,
                                   target,
                                   result);
            }

            base.erase(base.begin() + operatorIndex, base.end());
        }
    }

public:
    vector<string> addOperators(const string &num, int target)
    {
        vector<string> result;

        if (!num.empty())
        {
            if (num.front() == '0')
            {
                string base = "0";

                addOperatorsHelper(base, 0, 0, num.cbegin() + 1, num.cend(), target, result);
            }
            else
            {
                string base;
                intmax_t value = 0;

                for (auto it = num.cbegin(); it != num.cend(); ++it)
                {
                    base += *it;
                    value *= 10;
                    value += *it - '0';

                    addOperatorsHelper(base, value, value, it + 1, num.cend(), target, result);
                }
            }
        }

        return result;
    }
};
