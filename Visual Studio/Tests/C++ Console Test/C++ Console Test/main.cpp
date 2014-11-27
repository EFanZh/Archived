#include <algorithm>
#include <iostream>
#include <unordered_map>
#include <cassert>

using namespace std;

class Solution
{
    bool isValidSegment(const char *s)
    {
        return  *s < '2' && *s != '0' || (*s == '2' && (s[1] < '5' || (s[1] == '5' && s[2] < '6')));
    }

    template<size_t Level>
    static size_t getMin(const string &s, size_t i)
    {
        if (s[i] == '0')
        {
            return i + 1;
        }
        else
        {
            return i + 3 * Level < s.length() ? s.length() - 3 * Level - i : i + 1;
        }
    }

    template<size_t Level>
    static size_t getMax(const string &s, size_t i)
    {
        if (s[i] == '0')
        {
            return i + 1;
        }
        else
        {
            return min(i + 3, s.length() - Level);
        }
    }

public:
    vector<string> restoreIpAddresses(string s)
    {
        vector<string> result;

        if (s.length() >= 4 && s.length() <= 12)
        {
            size_t iMin = getMin<3>(s, 0);
            size_t iMax = getMax<3>(s, 0);

            for (size_t i = iMin; i <= iMax; ++i)
            {
                size_t jMin = getMin<2>(s, i);
                size_t jMax = getMax<2>(s, i);

                for (size_t j = jMin; j <= jMax; ++j)
                {
                    size_t kMin = getMin<1>(s, i);
                    size_t kMax = getMax<1>(s, i);

                    for (size_t k = kMin; k <= kMax; ++k)
                    {
                        if (s.length() - k < 2 || isValidSegment(s.data() + k))
                        {
                            string ip(s.cbegin(), s.cbegin() + i);

                            ip += '.';
                            ip.insert(ip.end(), s.cbegin() + i, s.cbegin() + j);
                            ip += '.';
                            ip.insert(ip.end(), s.cbegin() + j, s.cbegin() + k);
                            ip += '.';
                            ip.insert(ip.end(), s.cbegin() + k, s.cend());

                            result.emplace_back(move(ip));
                        }
                    }
                }
            }
        }

        return result;
    }
};

int main()
{
    Solution().restoreIpAddresses("0000");
}
