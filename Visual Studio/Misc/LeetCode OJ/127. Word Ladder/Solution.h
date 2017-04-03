#pragma once

class Solution
{
    static bool isNeighbor(const string &a, const string &b)
    {
        bool hasDiff = false;

        for (size_t i = 0; i < a.length(); ++i)
        {
            if (a[i] != b[i])
            {
                if (hasDiff)
                {
                    return false;
                }
                else
                {
                    hasDiff = true;
                }
            }
        }

        return hasDiff;
    }

public:
    int ladderLength(string start, string end, unordered_set<string> &dict)
    {
        unordered_map<const string *, vector<const string *>> graph;
        vector<const string *> vectorDict;

        for (auto &word : dict)
        {
            vectorDict.emplace_back(word == end ? &end : &word);
        }

        for (size_t i = 0; i < vectorDict.size() - 1; ++i)
        {
            for (size_t j = i + 1; j < vectorDict.size(); ++j)
            {
                if (isNeighbor(*vectorDict[i], *vectorDict[j]))
                {
                    graph[vectorDict[i]].emplace_back(vectorDict[j]);
                    graph[vectorDict[j]].emplace_back(vectorDict[i]);
                }
            }
        }

        for (auto *word : vectorDict)
        {
            if (isNeighbor(start, *word))
            {
                graph[&start].emplace_back(word);
            }
        }

        if (dict.count(end) == 0)
        {
            for (auto *word : vectorDict)
            {
                if (isNeighbor(*word, end))
                {
                    graph[word].emplace_back(&end);
                }
            }
        }

        queue<pair<const string *, size_t>> q;
        unordered_set<const string *> visited = { &start };

        q.emplace(&start, 1);

        while (!q.empty())
        {
            auto item = q.front();

            q.pop();

            auto it = graph.find(item.first);

            if (it != graph.cend())
            {
                for (auto *word : it->second)
                {
                    if (word == &end)
                    {
                        return static_cast<int>(item.second + 1);
                    }
                    else
                    {
                        if (visited.count(word) == 0)
                        {
                            visited.emplace(word);
                            q.emplace(word, item.second + 1);
                        }
                    }
                }
            }
        }

        return 0;
    }
};
