#pragma once

class Solution
{
public:
    int evalRPN(vector<string> &tokens)
    {
        stack<int> s;
        for (auto &t : tokens)
        {
            try
            {
                s.emplace(stoi(t));
            }
            catch (...)
            {
                int rhs = s.top();
                s.pop();

                switch (t[0])
                {
                    case '+':
                        s.top() += rhs;
                        break;

                    case '-':
                        s.top() -= rhs;
                        break;

                    case '*':
                        s.top() *= rhs;
                        break;

                    case '/':
                        s.top() /= rhs;
                        break;
                }
            }
        }

        return s.top();
    }
};
