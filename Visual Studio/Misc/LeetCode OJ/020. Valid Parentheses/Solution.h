#pragma once

class Solution
{
public:
    bool isValid(string s)
    {
        stack<char> main_stack;

        for (auto c : s)
        {
            switch (c)
            {
                case '(':
                case '[':
                case '{':
                    main_stack.emplace(c);
                    break;

                default:
                {
                    char compare;

                    switch (c)
                    {
                        case ')':
                            compare = '(';
                            break;

                        case ']':
                            compare = '[';
                            break;

                        case '}':
                            compare = '{';
                            break;

                        default:
                            return false;
                    }

                    if (!main_stack.empty() && main_stack.top() == compare)
                    {
                        main_stack.pop();
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        return main_stack.empty();
    }
};
