// https://leetcode.com/problems/peeking-iterator/

#include "Solution.h"

struct Iterator::Data
{
    vector<int>::const_iterator first;
    vector<int>::const_iterator last;

    Data(vector<int>::const_iterator first, vector<int>::const_iterator last) : first(first), last(last)
    {
    }
};

Iterator::Iterator(const vector<int> &nums) : data(new Data(nums.cbegin(), nums.cend()))
{
}

Iterator::Iterator(const Iterator &iter) : data(new Data(iter.data->first, iter.data->last))
{
}

Iterator::~Iterator()
{
    delete data;
}

int Iterator::next()
{
    return *data->first++;
}

bool Iterator::hasNext() const
{
    return data->first == data->last;
}

int main()
{
}
