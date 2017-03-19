#pragma once

class Solution
{
    template <class T>
    static void removeInvalidParenthesesHelper(size_t s,
                                               size_t removed,
                                               string &current,
                                               size_t &minRemoved,
                                               T first,
                                               T last,
                                               unordered_set<string> &result)
    {
        if (first == last)
        {
            if (s == 0)
            {
                if (removed < minRemoved)
                {
                    result.clear();
                    result.emplace(current);

                    minRemoved = removed;
                }
                else if (removed == minRemoved)
                {
                    result.emplace(current);
                }
            }
        }
        else
        {
            switch (*first)
            {
                case '(':
                    current += '(';
                    removeInvalidParenthesesHelper(s + 1, removed, current, minRemoved, first + 1, last, result);
                    current.pop_back();

                    if (removed < minRemoved)
                    {
                        removeInvalidParenthesesHelper(s, removed + 1, current, minRemoved, first + 1, last, result);
                    }
                    break;

                case ')':
                    if (s > 0)
                    {
                        current += ')';
                        removeInvalidParenthesesHelper(s - 1, removed, current, minRemoved, first + 1, last, result);
                        current.pop_back();
                    }

                    if (removed < minRemoved)
                    {
                        removeInvalidParenthesesHelper(s, removed + 1, current, minRemoved, first + 1, last, result);
                    }
                    break;

                default:
                    current += *first;
                    removeInvalidParenthesesHelper(s, removed, current, minRemoved, first + 1, last, result);
                    current.pop_back();
                    break;
            }
        }
    }

public:
    vector<string> removeInvalidParentheses(const string &s)
    {
        size_t stack = 0;
        string current;
        size_t minRemoved = s.length();
        unordered_set<string> result;

        removeInvalidParenthesesHelper(stack, 0, current, minRemoved, s.cbegin(), s.cend(), result);

        return { result.cbegin(), result.cend() };
    }
};
