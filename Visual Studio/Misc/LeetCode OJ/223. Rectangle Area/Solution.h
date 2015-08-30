#pragma once

class Solution
{
    static int computeAreaRect(int x1, int y1, int x2, int y2)
    {
        return (x2 - x1) * (y2 - y1);
    }

    static int computeAreaRectOutside(int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4)
    {
        return computeAreaRect(x1, y1, x2, y2) + computeAreaRect(x3, y3, x4, y4);
    }

    static int computeAreaRectAside(int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4)
    {
        return computeAreaRectOutside(x1, y1, x2, y2, x3, y3, x4, y4) - computeAreaRect(x3, y3, x2, y4);
    }

    static int computeAreaRectCorner(int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4)
    {
        return computeAreaRectOutside(x1, y1, x2, y2, x3, y3, x4, y4) - computeAreaRect(x3, y3, x2, y2);
    }

    static int computeAreaRectCross(int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4)
    {
        return computeAreaRectOutside(x1, y1, x2, y2, x3, y3, x4, y4) - computeAreaRect(x3, y1, x4, y2);
    }

    static int computeAreaDispatcher(int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4)
    {
        if (x2 <= x3 || y2 <= y3 || y1 >= y4)
        {
            return computeAreaRectOutside(x1, y1, x2, y2, x3, y3, x4, y4);
        }

        if (x4 <= x2)
        {
            if (y3 >= y1)
            {
                if (y4 <= y2)
                {
                    return computeAreaRect(x1, y1, x2, y2);
                }
                else
                {
                    // Rotate 90 degrees clockwise.
                    return computeAreaRectAside(y1, -x2, y2, -x1, y3, -x4, y4, -x3);
                }
            }
            else
            {
                if (y4 <= y2)
                {
                    // Rotate 90 degrees counter clockwise.
                    return computeAreaRectAside(-y2, x1, -y1, x2, -y4, x3, -y3, x4);
                }
                else
                {
                    return computeAreaRectCross(x1, y1, x2, y2, x3, y3, x4, y4);
                }
            }
        }
        else
        {
            if (y3 >= y1)
            {
                if (y4 <= y2)
                {
                    return computeAreaRectAside(x1, y1, x2, y2, x3, y3, x4, y4);
                }
                else
                {
                    return computeAreaRectCorner(x1, y1, x2, y2, x3, y3, x4, y4);
                }
            }
            else
            {
                if (y4 <= y2)
                {
                    return computeAreaRectCorner(x1, -y2, x2, -y1, x3, -y4, x4, -y3);
                }
                else
                {
                    return computeAreaRectAside(-x4, y3, -x3, y4, -x2, y1, -x1, y2);
                }
            }
        }
    }

public:
    int computeArea(int A, int B, int C, int D, int E, int F, int G, int H)
    {
        if (A < E)
        {
            return computeAreaDispatcher(A, B, C, D, E, F, G, H);
        }
        else
        {
            return computeAreaDispatcher(E, F, G, H, A, B, C, D);
        }
    }
};
