#pragma once

class Solution
{
public:
    int atoi(const char *str)
    {
        while (*str != '\0' && isspace(*str))
        {
            ++str;
        }

        if (*str != '\0')
        {
            bool is_positive = true;

            if (*str == '+')
            {
                ++str;
            }
            else if (*str == '-')
            {
                is_positive = false;
                ++str;
            }

            if (*str != '\0' && isdigit(*str))
            {
                int result;

                if (is_positive)
                {
                    result = *str - '0';

                    ++str;

                    for (; *str != '\0' && isdigit(*str); ++str)
                    {
                        if (result <= numeric_limits<int>::max() / 10)
                        {
                            result *= 10;
                        }
                        else
                        {
                            return numeric_limits<int>::max();
                        }

                        int digit = *str - '0';

                        if (result <= numeric_limits<int>::max() - digit)
                        {
                            result += digit;
                        }
                        else
                        {
                            return numeric_limits<int>::max();
                        }
                    }
                }
                else
                {
                    result = -(*str - '0');

                    ++str;

                    for (; *str != '\0' && isdigit(*str); ++str)
                    {
                        if (result >= numeric_limits<int>::min() / 10)
                        {
                            result *= 10;
                        }
                        else
                        {
                            return numeric_limits<int>::min();
                        }

                        int digit = *str - '0';

                        if (result >= numeric_limits<int>::min() + digit)
                        {
                            result -= digit;
                        }
                        else
                        {
                            return numeric_limits<int>::min();
                        }
                    }
                }

                return result;
            }
            else
            {
                return 0;
            }
        }
    }
};
