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
    size_t getListLength(ListNode *list)
    {
        size_t result = 0;

        while (list != nullptr)
        {
            ++result;
            list = list->next;
        }

        return result;
    }

public:
    ListNode *getIntersectionNode(ListNode *headA, ListNode *headB)
    {
        size_t l1 = getListLength(headA);
        size_t l2 = getListLength(headB);

        if (l1 < l2)
        {
            for (size_t i = 0; i < l2 - l1; ++i)
            {
                headB = headB->next;
            }
        }
        else
        {
            for (size_t i = 0; i < l1 - l2; ++i)
            {
                headA = headA->next;
            }
        }

        while (headA != headB)
        {
            headA = headA->next;
            headB = headB->next;
        }

        return headA;
    }
};
