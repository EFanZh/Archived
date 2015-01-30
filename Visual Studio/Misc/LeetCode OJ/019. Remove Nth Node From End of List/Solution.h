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
    ListNode *removeNthFromEnd(ListNode *head, int n)
    {
        ListNode **prev = &head;
        ListNode *p = head;

        for (int i = 0; i < n; ++i)
        {
            p = p->next;
        }

        while (p != nullptr)
        {
            prev = &(*prev)->next;
            p = p->next;
        }

        *prev = (*prev)->next;

        return head;
    }
};
