#pragma once

class Solution
{
    static bool IsSatisfy(const unordered_map<char, size_t> &sourceMap, const unordered_map<char, size_t> &targetMap)
    {
        if (sourceMap.size() < targetMap.size())
        {
            return false;
        }
        else
        {
            for (auto &item : sourceMap)
            {
                if (item.second < targetMap.at(item.first))
                {
                    return false;
                }
            }

            return true;
        }
    }

public:
    string minWindow(string S, string T)
    {
        unordered_map<char, size_t> targetMap;

        for (auto c : T)
        {
            ++targetMap[c];
        }

        for (size_t i = 0; i < S.length(); ++i)
        {
            if (targetMap.find(S[i]) != targetMap.cend())
            {
                size_t from = i;
                unordered_map<char, size_t> currentMap;
                size_t minFrom = numeric_limits<size_t>::min();
                size_t minTo = numeric_limits<size_t>::max();
                bool hasWindow = false;

                for (size_t to = from; to < S.length(); ++to)
                {
                    if (targetMap.find(S[to]) != targetMap.cend())
                    {
                        ++currentMap[S[to]];

                        if (!hasWindow)
                        {
                            hasWindow = IsSatisfy(currentMap, targetMap);
                        }

                        // Collapse.
                        while (from < to)
                        {
                            if (targetMap.find(S[from]) != targetMap.cend())
                            {
                                if (currentMap[S[from]] > targetMap[S[from]])
                                {
                                    --currentMap[S[from]];
                                }
                                else
                                {
                                    break;
                                }
                            }

                            ++from;
                        }

                        if (hasWindow && to - from < minTo - minFrom)
                        {
                            minFrom = from;
                            minTo = to;
                        }
                    }
                }

                if (hasWindow)
                {
                    return S.substr(minFrom, minTo - minFrom + 1);
                }
                else
                {
                    break;
                }
            }
        }

        return {};
    }
};
