#pragma once

/**
* Definition for a point.
* struct Point {
*     int x;
*     int y;
*     Point() : x(0), y(0) {}
*     Point(int a, int b) : x(a), y(b) {}
* };
*/
class Solution
{
    static int getGCD(int x, int y)
    {
        return y == 0 ? x : getGCD(y, x % y);
    }

public:
    int maxPoints(vector<Point> &points)
    {
        if (points.empty())
        {
            return 0;
        }
        else
        {
            int result = 0;

            for (auto it_1 = points.cbegin(); it_1 != points.cend() - 1; ++it_1)
            {
                map<int, map<int, int>> counts;
                int current_max = 0;
                int extra = 0;

                for (auto it_2 = it_1 + 1; it_2 != points.cend(); ++it_2)
                {
                    int x_offset = it_2->x - it_1->x;
                    int y_offset = it_2->y - it_1->y;
                    int current_result = 0;

                    if (x_offset == 0)
                    {
                        if (y_offset == 0)
                        {
                            ++extra;
                            continue;
                        }
                        else
                        {
                            current_result = ++counts[0][1];
                        }
                    }
                    else
                    {
                        int t = getGCD(x_offset, y_offset);
                        current_result = ++counts[x_offset / t][y_offset / t];
                    }
                    if (current_max < current_result)
                    {
                        current_max = current_result;
                    }
                }

                if (result < current_max + extra)
                {
                    result = current_max + extra;
                }
            }

            return result + 1;
        }
    }
};
