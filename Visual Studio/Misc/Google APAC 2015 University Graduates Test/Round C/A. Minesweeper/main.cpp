#include <iostream>
#include <queue>
#include <string>
#include <tuple>
#include <unordered_set>
#include <vector>

using namespace std;

void process(istream &input, ostream &output)
{
    int n;
    input >> n;
    input.get();

    vector<char> field;
    field.reserve(n * n);

    int total_mine_count = 0;

    for (int i = 0; i != n; ++i)
    {
        for (int j = 0; j != n; ++j)
        {
            int ch = input.get();
            field.emplace_back(ch);

            if (ch == '*')
            {
                ++total_mine_count;
            }
        }

        input.get();
    }

    for (int i = 0; i != field.size(); ++i)
    {
        if (field[i] == '*')
        {
            int row = i / n;
            int column = i % n;

            for (int i = row - 1; i <= row + 1; ++i)
            {
                for (int j = column - 1; j <= column + 1; ++j)
                {
                    if (i >= 0 && i < n && j >= 0 && j < n)
                    {
                        int t = n * i + j;

                        if (field[t] != '*')
                        {
                            if (field[t] == '.')
                            {
                                field[t] = '1';
                            }
                            else
                            {
                                ++field[t];
                            }
                        }
                    }
                }
            }
        }
    }

    unordered_set<int> unprocessed;

    for (size_t i = 0; i != field.size(); ++i)
    {
        unprocessed.emplace(i);
    }

    int block_count = 0;
    int uncovered = 0;

    while (true)
    {
        int first = -1;

        for (auto i : unprocessed)
        {
            if (field[i] == '.')
            {
                first = i;
                break;
            }
        }

        if (first == -1)
        {
            break;
        }
        unprocessed.erase(first);
        ++uncovered;

        queue<int> q;
        q.emplace(first);

        while (!q.empty())
        {
            int current = q.front();
            q.pop();

            int row = current / n;
            int column = current % n;

            if (field[current] == '.')
            {
                for (int i = row - 1; i <= row + 1; ++i)
                {
                    for (int j = column - 1; j <= column + 1; ++j)
                    {
                        if (i >= 0 && i < n && j >= 0 && j < n && !(i == row && j == column))
                        {
                            int t = n * i + j;

                            if (unprocessed.count(t) > 0)
                            {
                                if (field[t] == '.')
                                {
                                    q.emplace(t);
                                }

                                unprocessed.erase(t);
                                ++uncovered;
                            }
                        }
                    }
                }
            }
        }

        ++block_count;
    }

    output << ' ' << (block_count + (n * n - uncovered - total_mine_count)) << '\n';
}

int main()
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
