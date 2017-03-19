#pragma once

class Solution
{
    static int maxHistogramArea(const vector<int> &histogram)
    {
        size_t currentMaxArea = 0;
        stack<size_t> s;

        for (size_t i = 0; i < histogram.size(); ++i)
        {
            while (!s.empty() && histogram[s.top()] > histogram[i])
            {
                int minValue = histogram[s.top()];

                s.pop();
                currentMaxArea = max(currentMaxArea, minValue * (s.empty() ? i : i - 1 - s.top()));
            }

            s.emplace(i);
        }

        while (!s.empty())
        {
            int minValue = histogram[s.top()];

            s.pop();
            currentMaxArea =
                max(currentMaxArea, minValue * (s.empty() ? histogram.size() : histogram.size() - 1 - s.top()));
        }

        return currentMaxArea;
    }

public:
    int maximalRectangle(vector<vector<char>> &matrix)
    {
        if (matrix.empty() || matrix.front().empty())
        {
            return 0;
        }

        const size_t columns = matrix.front().size();
        int result = 0;
        vector<int> histogram(columns, 0);

        for (auto &row : matrix)
        {
            for (size_t i = 0; i < columns; ++i)
            {
                if (row[i] == '0')
                {
                    histogram[i] = 0;
                }
                else
                {
                    ++histogram[i];
                }
            }

            result = max(result, maxHistogramArea(histogram));
        }

        return result;
    }
};
