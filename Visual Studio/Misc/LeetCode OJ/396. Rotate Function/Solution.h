#pragma once

class Solution
{
public:
    int maxRotateFunction(const vector<int> &A)
    {
        auto sumA = accumulate(A.cbegin(), A.cend(), 0);
        auto sumB = 0;

        for (auto i = decltype(A.size())(1); i < A.size(); ++i)
        {
            sumB += static_cast<int>(i) * A[i];
        }

        auto result = sumB;

        for (auto i = decltype(A.size())(1); i < A.size(); ++i)
        {
            sumB += sumA - static_cast<int>(A.size()) * A[A.size() - i];

            result = max(result, sumB);
        }

        return result;
    }
};
