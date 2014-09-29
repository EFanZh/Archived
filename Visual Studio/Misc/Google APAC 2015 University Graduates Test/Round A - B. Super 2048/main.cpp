// https://code.google.com/codejam/contest/3214486/dashboard#s=p1

#include <fstream>
#include <iostream>
#include <string>
#include <vector>

using namespace std;

void collapse(vector<reference_wrapper<int>>::iterator it_begin, vector<reference_wrapper<int>>::iterator it_end)
{
    if (it_begin == it_end)
    {
        return;
    }
    else
    {
        if (it_begin == it_end - 1)
        {
            return;
        }
        else if (*it_begin == it_begin[1])
        {
            it_begin->get() += it_begin[1];
            for (auto it = it_begin + 1; it != it_end - 1; ++it)
            {
                it->get() = it[1];
            }
            it_end[-1].get() = 0;
            collapse(it_begin + 1, it_end - 1);
        }
        else
        {
            collapse(it_begin + 1, it_end);
        }
    }
}

void collapse(vector<reference_wrapper<int>> &v)
{
    auto it_target = v.begin();
    for (auto it = v.begin(); it != v.end(); ++it)
    {
        if (*it != 0)
        {
            it_target->get() = *it;
            ++it_target;
        }
    }
    for (auto it = it_target; it != v.end(); ++it)
    {
        it->get() = 0;
    }
    collapse(v.begin(), it_target);
}

void process(istream &input, ostream &output)
{
    int n;
    string direction;

    input >> n >> direction;

    int count = n * n;
    vector<int> matrix;

    for (int i = 0; i != count; ++i)
    {
        int x;
        input >> x;
        matrix.emplace_back(x);
    }

    if (direction == "left")
    {
        for (int row = 0; row != n; ++row)
        {
            vector<reference_wrapper<int>> v;
            for (int column = 0; column != n; ++column)
            {
                v.emplace_back(matrix[n * row + column]);
            }
            collapse(v);
        }
    }
    else if (direction == "right")
    {
        for (int row = 0; row != n; ++row)
        {
            vector<reference_wrapper<int>> v;
            for (int column = n - 1; column >= 0; --column)
            {
                v.emplace_back(matrix[n * row + column]);
            }
            collapse(v);
        }
    }
    else if (direction == "up")
    {
        for (int column = 0; column != n; ++column)
        {
            vector<reference_wrapper<int>> v;
            for (int row = 0; row != n; ++row)
            {
                v.emplace_back(matrix[n * row + column]);
            }
            collapse(v);
        }
    }
    else if (direction == "down")
    {
        for (int column = 0; column != n; ++column)
        {
            vector<reference_wrapper<int>> v;
            for (int row = n - 1; row >= 0; --row)
            {
                v.emplace_back(matrix[n * row + column]);
            }
            collapse(v);
        }
    }

    output << '\n';
    for (int row = 0; row != n; ++row)
    {
        for (int column = 0; column != n; ++column)
        {
            output << matrix[n * row + column] << ' ';
        }
        output << '\n';
    }
}

void main()
{
    istream &input = cin;
    ostream &output = cout;

    int case_count;
    input >> case_count;

    for (int i = 1; i <= case_count; ++i)
    {
        output << "Case #" << i << ":";
        process(input, output);
    }
}
