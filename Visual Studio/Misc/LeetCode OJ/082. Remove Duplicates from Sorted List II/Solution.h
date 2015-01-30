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
    ListNode *deleteDuplicates(ListNode *head)
    {
        for (ListNode **p = &head; *p != nullptr;)
        {
            ListNode *p2 = (*p)->next;

            if (p2 != nullptr && p2->val == (*p)->val)
            {
                p2 = p2->next;
                while (p2 != nullptr && p2->val == (*p)->val)
                {
                    p2 = p2->next;
                }

                *p = p2;
            }
            else
            {
                p = &(*p)->next;
            }
        }

        return head;
    }
};
