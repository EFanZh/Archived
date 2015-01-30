#pragma once

class Solution
{
    vector<string> generateParenthesis(int stack_size, int remain_size)
    {
        vector<string> result;

        if (stack_size >= 0 && remain_size >= stack_size)
        {
            if (remain_size == 0)
            {
                result.emplace_back("");
            }
            else
            {
                for (auto s : generateParenthesis(stack_size + 1, remain_size - 1))
                {
                    result.emplace_back("(" + s);
                }
                for (auto s : generateParenthesis(stack_size - 1, remain_size - 1))
                {
                    result.emplace_back(")" + s);
                }
            }
        }

        return result;
    }

public:
    vector<string> generateParenthesis(int n)
    {
        return generateParenthesis(0, n * 2);
    }
};
