#pragma once

class Stack
{
    queue<int> q;

public:
    // Push element x onto stack.
    void push(int x)
    {
        q.emplace(x);

        size_t size = q.size();

        for (size_t i = 1; i < size; ++i)
        {
            q.emplace(q.front());
            q.pop();
        }
    }

    // Removes the element on top of the stack.
    void pop()
    {
        q.pop();
    }

    // Get the top element.
    int top()
    {
        return q.front();
    }

    // Return whether the stack is empty.
    bool empty()
    {
        return q.empty();
    }
};
