#pragma once

class Solution
{
    void skipBlank(const string &input, size_t &i)
    {
        while (isspace(input[i]))
        {
            ++i;
        }
    }

    int getInteger(const string &input, size_t &i)
    {
        int result = 0;

        for (; isdigit(input[i]); ++i)
        {
            result *= 10;
            result += input[i] - '0';
        }

        return result;
    }

public:
    int calculate(const string &s)
    {
        stack<int> results;
        stack<char> operators;
        size_t i = 0;

        while (i < s.length())
        {
            skipBlank(s, i);

            if (isdigit(s[i]))
            {
                int k = getInteger(s, i);

                if (operators.empty())
                {
                    results.emplace(k);
                }
                else
                {
                    switch (operators.top())
                    {
                        case '(':
                            results.emplace(k);
                            break;

                        case '+':
                            results.top() += k;
                            break;

                        case '-':
                            results.top() -= k;
                            break;
                    }

                    operators.pop();
                }
            }
            else if (s[i] == ')')
            {
                if (!operators.empty())
                {
                    int k = results.top();

                    switch (operators.top())
                    {
                        case '+':
                            results.pop();
                            operators.pop();
                            results.top() += k;
                            break;

                        case '-':
                            results.pop();
                            operators.pop();
                            results.top() -= k;
                            break;
                    }
                }

                ++i;
            }
            else if (i < s.length())
            {
                operators.emplace(s[i]);

                ++i;
            }
        }

        return results.top();
    }
};
