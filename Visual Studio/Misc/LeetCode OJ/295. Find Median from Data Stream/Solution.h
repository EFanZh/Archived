#pragma once

class MedianFinder
{
    priority_queue<int, vector<int>, less<int>> left;
    priority_queue<int, vector<int>, greater<int>> right;

public:
    MedianFinder()
    {
        left.push(numeric_limits<int>::min());
        right.push(numeric_limits<int>::max());
    }

    // Adds a number into the data structure.
    void addNum(int num)
    {
        if (num < left.top())
        {
            if (left.size() > right.size())
            {
                right.push(left.top());
                left.pop();
            }
            left.push(num);
        }
        else if (num > right.top())
        {
            if (right.size() > left.size())
            {
                left.push(right.top());
                right.pop();
            }
            right.push(num);
        }
        else
        {
            if (left.size() <= right.size())
            {
                left.push(num);
            }
            else
            {
                right.push(num);
            }
        }
    }

    // Returns the median of current data stream
    double findMedian()
    {
        if (left.size() == right.size())
        {
            return left.top() + (right.top() - left.top()) / 2.0;
        }
        else
        {
            return left.size() < right.size() ? right.top() : left.top();
        }
    }
};

// Your MedianFinder object will be instantiated and called as such:
// MedianFinder mf;
// mf.addNum(1);
// mf.findMedian();
