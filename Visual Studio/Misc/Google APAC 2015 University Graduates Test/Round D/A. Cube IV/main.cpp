// https://code.google.com/codejam/contest/6214486/dashboard#s=p0

#include <iostream>
#include <queue>
#include <string>
#include <tuple>
#include <unordered_map>
#include <unordered_set>
#include <vector>

using namespace std;

void process(istream &input, ostream &output)
{
    int size;
    input >> size;

    vector<int> cube;
    cube.resize(size * size);

    for (int i = 0; i != size; ++i)
    {
        for (int j = 0; j != size; ++j)
        {
            input >> cube[size * i + j];
        }
    }

    queue<int> q;
    unordered_map<int, unordered_set<int>> graph;
    unordered_map<int, int> cache;
    vector<bool> visited;
    visited.resize(size * size);

    int room_number = INT_MAX;
    int max_length = 1;

    for (size_t i = 0; i != cube.size(); ++i)
    {
        int row = i / size;
        int column = i % size;

        bool is_end = true;
        if (column > 0 && cube[i - 1] == cube[i] + 1)
        {
            graph[i - 1].emplace(i);
            is_end = false;
        }
        if (column < size - 1 && cube[i + 1] == cube[i] + 1)
        {
            graph[i + 1].emplace(i);
            is_end = false;
        }
        if (row > 0 && cube[i - size] == cube[i] + 1)
        {
            graph[i - size].emplace(i);
            is_end = false;
        }
        if (row < size - 1 && cube[i + size] == cube[i] + 1)
        {
            graph[i + size].emplace(i);
            is_end = false;
        }

        if (is_end)
        {
            cache.emplace(i, 1);
            q.emplace(i);
            visited[i] = true;
            if (cube[i] < room_number)
            {
                room_number = cube[i];
            }
        }
    }

    while (!q.empty())
    {
        int k = q.front();
        q.pop();

        for (auto next : graph[k])
        {
            if (cache[k] + 1 > cache[next])
            {
                cache[next] = cache[k] + 1;
                if (cache[next] > max_length)
                {
                    room_number = cube[next];
                    max_length = cache[next];
                }
                else if (cache[next] == max_length && cube[next] < room_number)
                {
                    room_number = cube[next];
                }
            }
            if (!visited[next])
            {
                q.emplace(next);
                visited[next] = true;
            }
        }
    }

    cout << ' ' << room_number << ' ' << max_length << '\n';
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
