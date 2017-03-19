#pragma once

/**
* Definition for an interval.
* struct Interval {
*     int start;
*     int end;
*     Interval() : start(0), end(0) {}
*     Interval(int s, int e) : start(s), end(e) {}
* };
*/
class Solution
{
public:
    vector<Interval> merge(vector<Interval> &intervals)
    {
        if (intervals.size() <= 1)
        {
            return intervals;
        }

        sort(intervals.begin(), intervals.end(),
             [](const Interval &lhs, const Interval &rhs) { return lhs.start < rhs.start; });

        vector<Interval> result = { intervals.front() };

        for (size_t i = 1; i < intervals.size(); ++i)
        {
            if (intervals[i].start <= result.back().end)
            {
                result.back().end = max(result.back().end, intervals[i].end);
            }
            else
            {
                result.emplace_back(intervals[i]);
            }
        }

        return result;
    }
};
