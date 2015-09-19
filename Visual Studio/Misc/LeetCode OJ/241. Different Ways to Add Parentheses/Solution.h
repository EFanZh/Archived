#pragma once

class Solution
{
    static int parseInteger(const string &input, size_t &i)
    {
        int result = input[i] - '0';

        for (++i; i < input.length() && isdigit(input[i]); ++i)
        {
            result *= 10;
            result += input[i] - '0';
        }

        return result;
    }

    template<class T1, class T2>
    static vector<int> diffWaysToComputeHelper(T1 numbersBegin, T1 numbersEnd, T2 operatorsBegin, T2 operatorsEnd)
    {
        size_t operatorsCount = operatorsEnd - operatorsBegin;

        if (operatorsCount == 0)
        {
            return{ *numbersBegin };
        }

        vector<int> result;

        for (size_t i = 0; i < operatorsCount; ++i)
        {
            auto leftResults = diffWaysToComputeHelper(numbersBegin, numbersBegin + (i + 1), operatorsBegin, operatorsBegin + i);
            auto rightResults = diffWaysToComputeHelper(numbersBegin + (i + 1), numbersEnd, operatorsBegin + (i + 1), operatorsEnd);

            for (int lhs : leftResults)
            {
                for (int rhs : rightResults)
                {
                    switch (operatorsBegin[i])
                    {
                    case '+':
                        result.emplace_back(lhs + rhs);
                        break;

                    case '-':
                        result.emplace_back(lhs - rhs);
                        break;

                    case '*':
                        result.emplace_back(lhs * rhs);
                        break;
                    }
                }
            }
        }

        return result;
    }

public:
    vector<int> diffWaysToCompute(const string &input)
    {
        size_t i = 0;
        vector<int> numbers = { parseInteger(input, i) };
        vector<char> operators;

        for (; i < input.length();)
        {
            operators.emplace_back(input[i++]);
            numbers.emplace_back(parseInteger(input, i));
        }

        return diffWaysToComputeHelper(numbers.cbegin(), numbers.cend(), operators.cbegin(), operators.cend());
    }
};
