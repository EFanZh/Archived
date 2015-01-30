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
    void reorderList(ListNode *head)
    {
        vector<ListNode *> v;

        for (; head != nullptr; head = head->next)
        {
            v.emplace_back(head);
        }

        if (v.size() > 2)
        {
            size_t tail = v.size() / 2;

            for (size_t i = 0; i < tail; ++i)
            {
                v[i]->next = v[v.size() - 1 - i];
                v[v.size() - 1 - i]->next = v[i + 1];
            }

            if (v.size() % 2 == 0)
            {
                v[tail - 1]->next = v[tail];
            }
            v[tail]->next = nullptr;
        }
    }
};
