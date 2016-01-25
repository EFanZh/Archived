// https://leetcode.com/problems/range-sum-query-mutable/

#include "Solution.h"

int main()
{
    NumArray n({ 1, 3, 5 });

    n.sumRange(0, 2);
    n.update(1, 2);
    n.sumRange(0, 2);
}
