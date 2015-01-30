#pragma once

/**
* Definition for undirected graph.
* struct UndirectedGraphNode {
*     int label;
*     vector<UndirectedGraphNode *> neighbors;
*     UndirectedGraphNode(int x) : label(x) {}
* };
*/
class Solution
{
public:
    UndirectedGraphNode *cloneGraph(UndirectedGraphNode *node)
    {
        if (node == nullptr)
        {
            return nullptr;
        }

        unordered_map<const UndirectedGraphNode *, UndirectedGraphNode *> oldToNew = { { node, new UndirectedGraphNode(node->label) } };
        queue<const UndirectedGraphNode *> q;

        q.emplace(node);

        while (!q.empty())
        {
            const UndirectedGraphNode *item = q.front();

            q.pop();

            for (auto *next : item->neighbors)
            {
                if (oldToNew.count(next) == 0)
                {
                    oldToNew.emplace(next, new UndirectedGraphNode(next->label));
                    q.emplace(next);
                }
            }
        }

        for (auto &item : oldToNew)
        {
            for (auto next : item.first->neighbors)
            {
                item.second->neighbors.emplace_back(oldToNew.at(next));
            }
        }

        return oldToNew.at(node);
    }
};
