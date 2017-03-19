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
    ListNode *mergeKLists(const vector<ListNode *> &lists)
    {
        auto comparer = [](const ListNode *lhs, const ListNode *rhs) {
            return rhs != nullptr && (lhs == nullptr || lhs->val > rhs->val);
        };

        auto q = priority_queue<ListNode *, vector<ListNode *>, decltype(comparer)>(comparer);

        for (const auto node : lists)
        {
            if (node != nullptr)
            {
                q.emplace(node);
            }
        }

        auto head = static_cast<ListNode *>(nullptr);
        auto current = &head;

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
