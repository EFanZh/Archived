#pragma once

class Solution
{
public:
    ListNode *rotateRight(ListNode *head, int k)
    {
        if (head == nullptr || k == 0)
        {
            return head;
        }

        vector<ListNode *> nodes = { head };

        head = head->next;
        while (head != nullptr)
        {
            nodes.emplace_back(head);
            head = head->next;
        }

        size_t i = k % nodes.size();

        if (i == 0)
        {
            return nodes.front();
        }

        nodes[nodes.size() - i - 1]->next = nullptr;
        nodes.back()->next = nodes.front();

        return nodes[nodes.size() - i];
    }
};
