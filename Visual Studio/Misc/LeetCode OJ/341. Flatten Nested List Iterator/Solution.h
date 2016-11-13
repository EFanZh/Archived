#pragma once

/**
 * // This is the interface that allows for creating nested lists.
 * // You should not implement it, or speculate about its implementation
 * class NestedInteger {
 *   public:
 *     // Return true if this NestedInteger holds a single integer, rather than a nested list.
 *     bool isInteger() const;
 *
 *     // Return the single integer that this NestedInteger holds, if it holds a single integer
 *     // The result is undefined if this NestedInteger holds a nested list
 *     int getInteger() const;
 *
 *     // Return the nested list that this NestedInteger holds, if it holds a nested list
 *     // The result is undefined if this NestedInteger holds a single integer
 *     const vector<NestedInteger> &getList() const;
 * };
 */
class NestedIterator
{
    stack<pair<vector<NestedInteger>::const_iterator, vector<NestedInteger>::const_iterator>> s;

    void nextHelper()
    {
        for (;;)
        {
            if (s.top().first == s.top().second)
            {
                s.pop();

                if (s.empty())
                {
                    break;
                }
                else
                {
                    ++s.top().first;
                }
            }
            else if (s.top().first->isInteger())
            {
                break;
            }
            else
            {
                s.emplace(s.top().first->getList().cbegin(), s.top().first->getList().cend());
            }
        }
    }

public:
    NestedIterator(const vector<NestedInteger> &nestedList)
    {
        s.emplace(nestedList.cbegin(), nestedList.cend());
        nextHelper();
    }

    int next()
    {
        const auto result = s.top().first->getInteger();

        ++s.top().first;
        nextHelper();

        return result;
    }

    bool hasNext()
    {
        return !s.empty();
    }
};

/**
 * Your NestedIterator object will be instantiated and called as such:
 * NestedIterator i(nestedList);
 * while (i.hasNext()) cout << i.next();
 */
