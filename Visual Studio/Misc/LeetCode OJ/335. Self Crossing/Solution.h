#pragma once

class Solution
{
    // +----+
    // |    |
    // +----+
    //      ^ Start here.
    //
    // Requires `first` != `last`.
    template <class Iterator>
    static bool isSelfCrossingHelper(int width, int height, Iterator first, Iterator last)
    {
        for (; first != last; ++first)
        {
            if (*first < height)
            {
                height = *first;
            }
            else
            {
                return true;
            }

            ++first;

            if (first == last)
            {
                break;
            }

            if (*first < width)
            {
                width = *first;
            }
            else
            {
                return true;
            }
        }

        return false;
    }

public:
    bool isSelfCrossing(const vector<int> &x)
    {
        const auto count = x.size();

        if (count < 4)
        {
            return false;
        }

        if (x[2] <= x[0])
        {
            // +--+
            // |  |
            //    |
            return isSelfCrossingHelper(x[2], x[1], x.cbegin() + 3, x.cend());
        }

        if (x[3] < x[1])
        {
            // +--+
            // |  |
            // |
            // +-
            return isSelfCrossingHelper(x[3], x[2], x.cbegin() + 4, x.cend());
        }
        else if (x[3] == x[1])
        {
            // +--+
            // |  |
            // |
            // +---
            return isSelfCrossingHelper(x[3], x[2] - x[0], x.cbegin() + 4, x.cend());
        }

        for (auto i = vector<int>::size_type(4); i < count; ++i)
        {
            if (x[i] < x[i - 2] - x[i - 4])
            {
                return isSelfCrossingHelper(x[i], x[i - 1], x.cbegin() + (i + 1), x.cend());
            }
            else if (x[i] <= x[i - 2])
            {
                return isSelfCrossingHelper(x[i], x[i - 1] - x[i - 3], x.cbegin() + (i + 1), x.cend());
            }
        }

        return false;
    }
};
