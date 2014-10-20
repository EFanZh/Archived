// https://code.google.com/codejam/contest/5214486/dashboard#s=p1

#include <iostream>
#include <queue>
#include <tuple>
#include <unordered_map>
#include <unordered_set>

using namespace std;

void process(istream &input, ostream &output)
{
    unordered_map<int, unordered_map<int, unordered_map<int, unordered_map<int, int>>>> graph;

    int metro_count;
    input >> metro_count;

    for (int i = 0; i != metro_count; ++i)
    {
        vector<int> times;

        int station_count;
        input >> station_count;

        times.resize(station_count - 1);

        int wait_time;
        input >> wait_time;

        for (int j = 0; j != station_count - 1; ++j)
        {
            input >> times[j];

            int run_time = 0;
            for (int k = j; k >= 0; --k)
            {
                run_time += times[k];
                graph[i][k][i][j + 1] = wait_time + run_time;
                graph[i][j + 1][i][k] = wait_time + run_time;
            }
        }
    }

    int tunnel_count;
    input >> tunnel_count;

    for (int i = 0; i != tunnel_count; ++i)
    {
        int from_metro;
        int from_station;
        int to_metro;
        int to_station;
        int time;

        input >> from_metro >> from_station >> to_metro >> to_station >> time;

        --from_metro;
        --from_station;
        --to_metro;
        --to_station;

        graph[from_metro][from_station][to_metro][to_station] = time;
        graph[to_metro][to_station][from_metro][from_station] = time;
    }

    int query_count;
    input >> query_count;

    unordered_map<int, unordered_map<int, unordered_map<int, unordered_map<int, int>>>> cache;

    for (int i = 0; i != query_count; ++i)
    {
        int from_metro;
        int from_station;
        int to_metro;
        int to_station;

        input >> from_metro >> from_station >> to_metro >> to_station;

        queue<tuple<int, int, int>> q;
        unordered_map<int, unordered_set<int>> visited;

        while (!q.empty())
        {
            int station;
        }

        cout << '\n';
    }
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
