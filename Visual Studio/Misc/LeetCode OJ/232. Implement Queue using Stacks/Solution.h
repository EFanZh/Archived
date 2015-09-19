#pragma once

class Queue
{
    stack<int> pushStack;
    stack<int> popStack;

    void preparePopStack()
    {
        if (popStack.empty())
        {
            while (!pushStack.empty())
            {
                popStack.emplace(pushStack.top());
                pushStack.pop();
            }
        }
    }

public:
    // Push element x to the back of queue.
    void push(int x)
    {
        pushStack.push(x);
    }

    // Removes the element from in front of queue.
    void pop(void)
    {
        preparePopStack();
        popStack.pop();
    }

    // Get the front element.
    int peek(void)
    {
        preparePopStack();

        return popStack.top();
    }

    // Return whether the queue is empty.
    bool empty(void)
    {
        return pushStack.empty() && popStack.empty();
    }
};
