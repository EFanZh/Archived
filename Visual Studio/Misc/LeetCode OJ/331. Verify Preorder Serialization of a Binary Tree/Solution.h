#pragma once

class Solution
{
    template <class Iterator>
    static bool readTree(Iterator &first, Iterator last)
    {
        for (;;)
        {
            if (first > last)
            {
                return false;
            }

            if (*first == '#')
            {
                ++first;

                return true;
            }
            else
            {
                // Skip numbers.
                for (;;)
                {
                    ++first;

                    if (static_cast<unsigned char>(*first - '0') > 9)
                    {
                        break;
                    }
                }

                // Skip comma.
                ++first;

                if (!readTree(first, last))
                {
                    return false;
                }
                else
                {
                    // Skip comma.
                    ++first;

                    // Manual tail recursion.
                }
            }
        }
    }

public:
    bool isValidSerialization(const string &preorder)
    {
        auto it = preorder.begin();

        return readTree(it, preorder.end()) && it == preorder.end();
    }
};
