#pragma once

/**
* Definition for singly-linked list.
* struct ListNode {
*     int val;
*     ListNode *next;
*     ListNode(int x) : val(x), next(NULL) {}
* };
*/
class Solution
{
public:
    ListNode *mergeKLists(vector<ListNode *> &lists)
    {
        auto comparer = [](const ListNode *lhs, const ListNode *rhs)
        {
            return rhs != nullptr && (lhs == nullptr || lhs->val > rhs->val);
        };

        priority_queue<ListNode, vector<ListNode *>, decltype(comparer)> q(comparer);

        for (auto *node : lists)
        {
            if (node != nullptr)
            {
                q.emplace(node);
            }
        }

        ListNode *head = nullptr;
        ListNode **current = &head;

        while (!q.empty())
        {
            *current = q.top();
            q.pop();
            current = &(*current)->next;
            if (*current != nullptr)
            {
                q.emplace(*current);
            }
        }

        return head;
    }
};
