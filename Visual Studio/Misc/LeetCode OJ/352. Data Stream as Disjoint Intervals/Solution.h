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
class SummaryRanges
{
    map<int, int> intervals;

public:
    /** Initialize your data structure here. */
    SummaryRanges()
    {
    }

    void addNum(int val)
    {
        const auto it = intervals.upper_bound(val);

        if (it == intervals.cend())
        {
            if (intervals.empty())
            {
                intervals.emplace(val, val + 1);
            }
            else
            {
                auto &interval = *--intervals.end();

                if (interval.second < val)
                {
                    intervals.emplace_hint(intervals.end(), val, val + 1);
                }
                else if (interval.second == val)
                {
                    ++interval.second;
                }
            }
        }
        else if (it == intervals.cbegin())
        {
            if (it->first == val + 1)
            {
                const auto second = it->second;

                intervals.emplace_hint(intervals.erase(it), val, second);
            }
            else
            {
                intervals.emplace_hint(intervals.cbegin(), val, val + 1);
            }
        }
        else
        {
            const auto previousIt = prev(it);

            if (previousIt->second < val)
            {
                if (it->first == val + 1)
                {
                    auto second = it->second;

                    intervals.emplace_hint(intervals.erase(it), val, second);
                }
                else
                {
                    intervals.emplace(val, val + 1);
                }
            }
            else if (previousIt->second == val)
            {
                if (it->first == val + 1)
                {
                    previousIt->second = it->second;

                    intervals.erase(it);
                }
                else
                {
                    ++previousIt->second;
                }
            }
        }
    }

    vector<Interval> getIntervals()
    {
        auto result = vector<Interval>(intervals.size());
        auto it = intervals.cbegin();

        for (auto &interval : result)
        {
            interval.start = it->first;
            interval.end = it->second - 1;

            ++it;
        }

        return result;
    }
};

/**
 * Your SummaryRanges object will be instantiated and called as such:
 * SummaryRanges obj = new SummaryRanges();
 * obj.addNum(val);
 * vector<Interval> param_2 = obj.getIntervals();
 */
