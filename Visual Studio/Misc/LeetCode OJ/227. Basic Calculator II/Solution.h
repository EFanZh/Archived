#pragma once

class Solution
{
    static void skipWhitespaces(const string &s, size_t &i)
    {
        while (i < s.length() && isspace(s[i]))
        {
            ++i;
        }
    }

    static int number(const string &s, size_t &i)
    {
        int result = 0;

        for (; i < s.length() && isdigit(s[i]); ++i)
        {
            result *= 10;
            result += s[i] - '0';
        }

        return result;
    }

    static int term(const string &s, size_t &i)
    {
        int result = number(s, i);

        for (;;)
        {
            skipWhitespaces(s, i);

            if (i < s.length())
            {
                if (s[i] == '*')
                {
                    ++i;
                    skipWhitespaces(s, i);
                    result *= number(s, i);
                }
                else if (s[i] == '/')
                {
                    ++i;
                    skipWhitespaces(s, i);
                    result /= number(s, i);
                }
                else
                {
                    break;
                }
            }
            else
            {
                break;
            }
        }

        return result;
    }

    static int expression(const string &s, size_t &i)
    {
        skipWhitespaces(s, i);

        int result = term(s, i);

        for (;;)
        {
            skipWhitespaces(s, i);

            if (i < s.length())
            {
                if (s[i] == '+')
                {
                    ++i;
                    skipWhitespaces(s, i);
                    result += term(s, i);
                }
                else if (s[i] == '-')
                {
                    ++i;
                    skipWhitespaces(s, i);
                    result -= term(s, i);
                }
                else
                {
                    break;
                }
            }
            else
            {
                break;
            }
        }

        return result;
    }

public:
    int calculate(string s)
    {
        size_t i = 0;

        return expression(s, i);
    }
};
