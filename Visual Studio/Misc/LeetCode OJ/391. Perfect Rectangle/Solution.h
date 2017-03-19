#pragma once

class Solution
{
public:
    bool isRectangleCover(const vector<vector<int>> &rectangles)
    {
        struct Rectangle
        {
            int x0;
            int y0;
            int x1;
            int y1;

            bool operator<(const Rectangle &rhs) const
            {
                return tie(y0, x0) < tie(rhs.y0, rhs.x0);
            }
        };

        // Sort rectangles.

        auto sortedRectangles = vector<Rectangle>(rectangles.size());

        for (auto i = size_t(0); i < rectangles.size(); ++i)
        {
            sortedRectangles[i].x0 = rectangles[i][0];
            sortedRectangles[i].y0 = rectangles[i][1];
            sortedRectangles[i].x1 = rectangles[i][2];
            sortedRectangles[i].y1 = rectangles[i][3];
        }

        sort(sortedRectangles.begin(), sortedRectangles.end());

        auto cornersByX = map<int, int>();

        cornersByX.emplace(numeric_limits<int>::min(), sortedRectangles.front().y0);
        cornersByX.emplace(sortedRectangles.front().x0, sortedRectangles.front().y1);
        cornersByX.emplace(sortedRectangles.front().x1, sortedRectangles.front().y0);

        // Stack rectangles.

        for (auto it = next(sortedRectangles.cbegin()); it != sortedRectangles.cend(); ++it)
        {
            const auto &current = *it;
            const auto leftBoundIt = --cornersByX.upper_bound(it->x0);
            const auto rightBoundIt = cornersByX.lower_bound(it->x1);

            for (auto it2 = leftBoundIt; it2 != rightBoundIt; ++it2)
            {
                if (it2->second != current.y0)
                {
                    return false;
                }
            }

            cornersByX.erase(next(leftBoundIt), rightBoundIt);

            if (current.x0 == leftBoundIt->first)
            {
                leftBoundIt->second = current.y1;

                if (current.x1 != rightBoundIt->first)
                {
                    cornersByX.emplace(current.x1, current.y0);
                }
            }
            else
            {
                return false;
            }
        }

        auto it = next(cornersByX.cbegin());
        const auto itEnd = prev(cornersByX.cend());
        const auto top = it->second;

        for (++it; it != itEnd; ++it)
        {
            if (it->second != top)
            {
                return false;
            }
        }

        return true;
    }
};
