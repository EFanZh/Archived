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
    void removeDuplicates(ListNode **node)
    {
        if ((*node)->next != nullptr)
        {
            if ((*node)->val == (*node)->next->val)
            {
                ListNode *next = (*node)->next;
                // Not need to delete ?
                // delete *node;
                *node = next;
                removeDuplicates(node);
            }
            else
            {
                removeDuplicates(&(*node)->next);
            }
        }
    }

public:
    ListNode *deleteDuplicates(ListNode *head)
    {
        if (head == nullptr)
        {
            return head;
        }
        else
        {
            removeDuplicates(&head);
        }

        return head;
    }
};
