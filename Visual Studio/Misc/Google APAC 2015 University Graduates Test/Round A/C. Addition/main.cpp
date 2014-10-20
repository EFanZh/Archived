// https://code.google.com/codejam/contest/3214486/dashboard#s=p2
// This answer may be wrong.

#include <fstream>
#include <iostream>
#include <map>
#include <queue>
#include <string>
#include <vector>

using namespace std;

map<string, int> variable_to_id;
vector<vector<pair<int, int>>> graph;

string get_variable(istream &input)
{
    string str;
    int c;

    while (c = input.get(), isspace(c))
    {
    }
    input.unget();

    while (c = input.get(), isalpha(c))
    {
        str += static_cast<char>(c);
    }
    input.unget();

    return str;
}

int get_variable_id(string var)
{
    auto result = variable_to_id.find(var);
    if (result == variable_to_id.end())
    {
        return variable_to_id.emplace(var, variable_to_id.size() * 2).first->second;
    }
    else
    {
        return result->second;
    }
}

void add_edge(int x_id, int y_id, int value)
{
    size_t new_size = max(x_id, y_id) + 2;

    if (graph.size() < new_size)
    {
        graph.resize(new_size);
    }

    graph[x_id].emplace_back(y_id + 1, value);
    graph[x_id + 1].emplace_back(y_id, value);
    graph[y_id].emplace_back(x_id + 1, value);
    graph[y_id + 1].emplace_back(x_id, value);
}

pair<bool, int> calculate(int x_id, int y_id)
{
    // (vertex, value)
    queue<tuple<int, int>> q;
    q.emplace(x_id, 0);
    vector<bool> visited;
    visited.resize(graph.size());
    visited[x_id] = true;

    while (!q.empty())
    {
        int vertex;
        int value;

        tie(vertex, value) = q.front();
        q.pop();

        for (auto &next_pair : graph[vertex])
        {
            if (visited[next_pair.first])
            {
                continue;
            }
            else
            {
                if (next_pair.first == y_id)
                {
                    return make_pair(true, value + next_pair.second);
                }

                visited[next_pair.first] = true;
                q.emplace(next_pair.first, (next_pair.first % 2 == 0 ? value - next_pair.second : value + next_pair.second));
            }
        }
    }

    return make_pair(false, 0);
}

void process(istream &input, ostream &output)
{
    variable_to_id.clear();
    graph.clear();

    int answered_count;
    input >> answered_count;
    for (int i = 0; i != answered_count; ++i)
    {
        string var_1 = get_variable(input);
        input.get();
        string var_2 = get_variable(input);
        input.get();
        int value;
        input >> value;

        add_edge(get_variable_id(var_1), get_variable_id(var_2), value);
    }

    output << '\n';
    int question_count;
    input >> question_count;
    for (int i = 0; i != question_count; ++i)
    {
        string var_1 = get_variable(input);
        input.get();
        string var_2 = get_variable(input);

        auto result = calculate(get_variable_id(var_1), get_variable_id(var_2) + 1);
        if (result.first)
        {
            output << var_1 << '+' << var_2 << '=' << result.second << '\n';
        }
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
