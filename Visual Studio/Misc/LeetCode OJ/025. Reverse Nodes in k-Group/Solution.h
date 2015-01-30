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
    static size_t getListSize(ListNode *head)
    {
        size_t count = 0;

        for (; head != nullptr; head = head->next)
        {
            ++count;
        }

        return count;
    }

public:
    ListNode *reverseKGroup(ListNode *head, int k)
    {
        if (k < 2)
        {
            return head;
        }

        size_t groups = getListSize(head) / k;
        ListNode **p1 = &head;
        ListNode **p2 = &head->next;

        for (size_t i = 0; i < groups; ++i)
        {
            ListNode *p = *p2;

            for (int j = 1; j < k; ++j)
            {
                *p2 = p->next;
                p->next = *p1;
                *p1 = p;
                p = *p2;
            }

            p1 = p2;
            p2 = &(*p2)->next;
        }

        return head;
    }
};
