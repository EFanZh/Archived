#pragma once

class Solution
{
public:
    bool isNumber(const char *s)
    {
        // Leading space.
        while (isspace(*s))
        {
            ++s;
        }

        // Optional sign.
        if (*s == '+' || *s == '-')
        {
            ++s;
        }

        // Integer part.
        bool hasInteger = false;
        while (isdigit(*s))
        {
            hasInteger = true;
            ++s;
        }

        // Fractional part.
        if (*s == '.')
        {
            ++s;
        }
        if (!hasInteger && !isdigit(*s))
        {
            return false;
        }
        while (isdigit(*s))
        {
            ++s;
        }

        // Exponential part.
        if (*s == 'E' || *s == 'e')
        {
            ++s;

            if (*s == '+' || *s == '-')
            {
                ++s;
            }
            if (isdigit(*s))
            {
                ++s;
                while (isdigit(*s))
                {
                    ++s;
                }
            }
            else
            {
                return false;
            }
        }

        // Trailing space.
        while (isspace(*s))
        {
            ++s;
        }

        return *s == '\0';
    }
};
