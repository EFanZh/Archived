#pragma once

class Solution
{
    static bool isValidSegment(const char *s)
    {
        return *s < '2' || (*s == '2' && (s[1] < '5' || (s[1] == '5' && s[2] < '6')));
    }

    template <size_t Level>
    static pair<size_t, size_t> getBounds(const string &s, size_t i)
    {
        if (s[i] == '0')
        {
            return { i + 1, i + 2 };
        }
        else
        {
            size_t low = i + 3 * Level < s.length() ? s.length() - 3 * Level : i + 1;

            if (i + 3 < s.length())
            {
                return { low, min(i + (isValidSegment(s.data() + i) ? 4u : 3u), s.length() - Level + 1) };
            }
            else
            {
                return { low, s.length() - Level + 1 };
            }
        }
    }

public:
    vector<string> restoreIpAddresses(string s)
    {
        vector<string> result;

        if (s.length() >= 4 && s.length() <= 12)
        {
            auto iBounds = getBounds<3>(s, 0);

            for (size_t i = iBounds.first; i < iBounds.second; ++i)
            {
                auto jBounds = getBounds<2>(s, i);

                for (size_t j = jBounds.first; j < jBounds.second; ++j)
                {
                    auto kBounds = getBounds<1>(s, j);

                    for (size_t k = kBounds.first; k < kBounds.second; ++k)
                    {
                        if (s[k] == '0')
                        {
                            if (k + 1 != s.length())
                            {
                                continue;
                            }
                        }
                        else
                        {
                            if (s.length() - k > 3 || (s.length() - k == 3 && !isValidSegment(s.data() + k)))
                            {
                                continue;
                            }
                        }

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

        return result;
    }
};
