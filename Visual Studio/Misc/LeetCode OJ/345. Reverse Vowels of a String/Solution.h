#pragma once

namespace
{
    typedef array<bool, 'z' - 'A' + 1> VowelArray;

    static VowelArray GetVowelArray()
    {
        VowelArray result = {};

        result['A' - 'A'] = true;
        result['E' - 'A'] = true;
        result['I' - 'A'] = true;
        result['O' - 'A'] = true;
        result['U' - 'A'] = true;
        result['a' - 'A'] = true;
        result['e' - 'A'] = true;
        result['i' - 'A'] = true;
        result['o' - 'A'] = true;
        result['u' - 'A'] = true;

        return result;
    }

    static VowelArray vowelArray = GetVowelArray();
    static const bool *vowel = &vowelArray[0] - 'A';
}

class Solution
{
    static bool isVowel(char c)
    {
        return vowel[c];
    }

public:
    string reverseVowels(const string &s)
    {
        auto result = s;
        auto left = result.begin();
        auto right = result.end();

    Start:
        if (right - left > 1)
        {
            if (isVowel(*left))
            {
                for (;;)
                {
                    if (isVowel(right[-1]))
                    {
                        swap(*left, right[-1]);

                        ++left;
                        --right;

                        goto Start;
                    }
                    else
                    {
                        --right;

                        if (right - left <= 1)
                        {
                            return result;
                        }
                    }
                }
            }
            else
            {
                ++left;

                goto Start;
            }
        }

        return result;
    }
};
