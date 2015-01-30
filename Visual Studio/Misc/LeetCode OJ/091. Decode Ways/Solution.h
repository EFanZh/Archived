#pragma once

class Solution
{
    static int getResult(const char *s, int prev, int prev2)
    {
        switch (*s)
        {
            case '0':
                return 0;

            case '1':
                return prev + prev2;

            case '2':
                return prev + (s[1] <= '6' ? prev2 : 0);

            default:
                return prev;
        }
    }

public:
    int numDecodings(string s)
    {
        if (s.size() > 0)
        {
            int b = s.back() == '0' ? 0 : 1;

            if (s.size() > 1)
            {
                int a = getResult(&s.back() - 1, b, 1);

                for (int i = static_cast<int>(s.size()) - 3; i >= 0; --i)
                {
                    int current_result = getResult(s.data() + i, a, b);

                    b = a;
                    a = current_result;
                }

                return a;
            }
            else
            {
                return b;
            }
        }
        else
        {
            return 0;
        }
    }
};
