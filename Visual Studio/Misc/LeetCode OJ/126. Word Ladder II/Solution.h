#pragma once

class Solution
{
    static bool isNeighbor(const string &a, const string &b)
    {
        bool has_diff = false;

        for (size_t i = 0; i < a.length(); ++i)
        {
            if (a[i] != b[i])
            {
                if (has_diff)
                {
                    return false;
                }
                else
                {
                    has_diff = true;
                }
            }
        }

        return has_diff;
    }

public:
    vector<vector<string>> findLadders(string start, string end, unordered_set<string> &dict)
    {
        unordered_map<string, vector<string>> graph;
        unordered_set<string> new_nodes = { end };

        dict.emplace(start);
        dict.erase(end);

        while (!new_nodes.empty())
        {
            bool should_break = false;

            for (auto current : unordered_set<string>(move(new_nodes)))
            {
                for (auto &previous : dict)
                {
                    if (isNeighbor(previous, current))
                    {
                        graph[previous].emplace_back(current);
                        new_nodes.emplace(previous);

                        if (previous == start)
                        {
                            should_break = true;
                        }
                    }
                }
            }

            if (should_break)
            {
                break;
            }

            for (auto node : new_nodes)
            {
                dict.erase(node);
            }
        }

        vector<vector<string>> result;
        stack<tuple<string, vector<string>>> s;

        s.emplace(start, vector<string>{ start });

        while (!s.empty())
        {
            auto current = move(get<0>(s.top()));
            auto current_path = move(get<1>(s.top()));
            s.pop();

            if (current == end)
            {
                result.emplace_back(move(current_path));
            }
            else
            {
                for (auto next : graph[current])
                {
                    vector<string> v = current_path;

                    v.emplace_back(next);
                    s.emplace(next, move(v));
                }
            }
        }

        return result;
    }
};
