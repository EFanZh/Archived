#pragma once

class MinStack
{
    deque<int> values;
    stack<size_t> minIndexes;

public:
    void push(int x)
    {
        if (minIndexes.empty() || x < getMin())
        {
            minIndexes.emplace(values.size());
        }

        values.emplace_back(x);
    }

    void pop()
    {
        values.pop_back();

        if (minIndexes.top() == values.size())
        {
            minIndexes.pop();
        }
    }

    int top()
    {
        return values.back();
    }

    int getMin()
    {
        return values[minIndexes.top()];
    }
};
