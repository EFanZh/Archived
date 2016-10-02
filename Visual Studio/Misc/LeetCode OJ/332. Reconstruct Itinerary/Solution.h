#pragma once

class Solution
{
    struct Environment
    {
        unordered_map<string, multiset<string>> graph;
        vector<string> path;
        vector<string>::size_type i;
    };

    static void findItineraryHelper(Environment &e, const string &from)
    {
        const auto it = e.graph.find(from);

        if (it != e.graph.end())
        {
            while (!it->second.empty())
            {
                const auto next = *it->second.begin();

                it->second.erase(it->second.begin());

                findItineraryHelper(e, next);
            }
        }

        e.path[e.i] = from;
        --e.i;
    }

public:
    vector<string> findItinerary(const vector<pair<string, string>> &tickets)
    {
        Environment e{};

        for (const auto &ticket : tickets)
        {
            e.graph[ticket.first].emplace(ticket.second);
        }

        e.path.resize(tickets.size() + 1);
        e.i = e.path.size() - 1;

        findItineraryHelper(e, "JFK");

        return e.path;
    }
};
