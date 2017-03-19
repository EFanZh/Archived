#pragma once

class Solution
{
    template <class T>
    static vector<pair<int, int>> mergeSkyline(const T &left, const T &right)
    {
        vector<pair<int, int>> result;
        auto it1 = left.cbegin();
        auto it2 = right.cbegin();

        while (it1 != left.cend() && it2 != right.cend())
        {
            int x;
            int height;

            if (it1->first < it2->first)
            {
                x = it1->first;
                height = max(it1->second, it2 == right.cbegin() ? 0 : it2[-1].second);

                ++it1;
            }
            else if (it2->first < it1->first)
            {
                x = it2->first;
                height = max(it2->second, it1 == left.cbegin() ? 0 : it1[-1].second);

                ++it2;
            }
            else
            {
                x = it1->first;
                height = max(it1->second, it2->second);

                ++it1;
                ++it2;
            }

            if (result.empty() || height != result.back().second)
            {
                result.emplace_back(x, height);
            }
        }

        if (it1 == left.cend())
        {
            move(it2, right.cend(), back_inserter(result));
        }
        else
        {
            move(it1, left.cend(), back_inserter(result));
        }

        return result;
    }

    template <class T>
    static vector<pair<int, int>> getSkylineHelper(T first, T last)
    {
        size_t count = last - first;

        if (count == 1)
        {
            return { { (*first)[0], (*first)[2] }, { (*first)[1], 0 } };
        }

        T middle = first + count / 2;
        auto result1 = getSkylineHelper(first, middle);
        auto result2 = getSkylineHelper(middle, last);

        return mergeSkyline(result1, result2);
    }

public:
    vector<pair<int, int>> getSkyline(const vector<vector<int>> &buildings)
    {
        if (buildings.empty())
        {
            return {};
        }

        return getSkylineHelper(buildings.cbegin(), buildings.cend());
    }
};
