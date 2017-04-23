#pragma once

class Solution
{
public:
    vector<double> calcEquation(const vector<pair<string, string>> &equations,
                                const vector<double> &values,
                                const vector<pair<string, string>> &queries)
    {
        using NodeType = unsigned int;

        auto nextNode = NodeType(0);
        auto nodes = unordered_map<string, NodeType>();
        auto graph = vector<vector<pair<NodeType, double>>>();

        nodes.reserve(equations.size() * 2);
        graph.reserve(equations.size() * 2);

        const auto getNode = [&](const string &s) {
            const auto it = nodes.find(s);

            if (it == nodes.end())
            {
                graph.emplace_back();

                return nodes.emplace(s, nextNode++).first->second;
            }
            else
            {
                return it->second;
            }
        };

        // Build graph.

        for (auto i = decltype(equations.size())(0); i < equations.size(); ++i)
        {
            const auto &equation = equations[i];
            const auto firstNode = getNode(equation.first);
            const auto secondNode = getNode(equation.second);
            const auto &value = values[i];

            graph[firstNode].emplace_back(secondNode, 1.0 / value);
            graph[secondNode].emplace_back(firstNode, value);
        }

        // Build groups and assign node values.

        using GroupType = unsigned int;

        auto groupAndValues = vector<pair<GroupType, double>>(graph.size());
        auto visited = unordered_set<NodeType>();
        auto nextGroup = GroupType(0);
        const auto lastNode = static_cast<NodeType>(graph.size());

        visited.reserve(graph.size());

        for (auto node = NodeType(0); node < lastNode; ++node)
        {
            if (visited.emplace(node).second)
            {
                const auto currentGroup = nextGroup++;
                auto q = queue<pair<NodeType, double>>();
                auto current = make_pair(node, 1.0);

                for (;;)
                {
                    groupAndValues[current.first] = make_pair(currentGroup, current.second);

                    for (const auto &next : graph.at(current.first))
                    {
                        if (visited.emplace(next.first).second)
                        {
                            q.emplace(next.first, current.second * next.second);
                        }
                    }

                    if (q.empty())
                    {
                        break;
                    }
                    else
                    {
                        current = q.front();
                        q.pop();
                    }
                }
            }
        }

        // Calculate results;

        auto result = vector<double>();

        for (const auto &query : queries)
        {
            const auto it1 = nodes.find(query.first);

            if (it1 != nodes.end())
            {
                const auto it2 = nodes.find(query.second);

                if (it2 != nodes.end())
                {
                    const auto &groupAndValue1 = groupAndValues[it1->second];
                    const auto &groupAndValue2 = groupAndValues[it2->second];

                    if (groupAndValue1.first == groupAndValue2.first)
                    {
                        result.emplace_back(groupAndValue1.second / groupAndValue2.second);

                        continue;
                    }
                }
            }

            result.emplace_back(-1.0);
        }

        return result;
    }
};
