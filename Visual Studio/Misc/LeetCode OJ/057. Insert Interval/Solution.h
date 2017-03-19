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
    vector<Interval> insert(vector<Interval> &intervals, Interval newInterval)
    {
        auto it1 = lower_bound(intervals.begin(), intervals.end(), newInterval.start,
                               [](const Interval &lhs, int rhs) { return lhs.end < rhs; });

        if (it1 == intervals.cend())
        {
            intervals.emplace_back(newInterval);
        }
        else
        {
            auto it2 = upper_bound(it1, intervals.end(), newInterval.end,
                                   [](int lhs, const Interval &rhs) { return lhs < rhs.start; });

            if (it2 == it1 && newInterval.end < it2->start)
            {
                intervals.emplace(it1, newInterval);
            }
            else
            {
                it1->start = min(it1->start, newInterval.start);
                it1->end = max(it2[-1].end, newInterval.end);
                intervals.erase(it1 + 1, it2);
            }
        }

        return intervals;
    }
};
