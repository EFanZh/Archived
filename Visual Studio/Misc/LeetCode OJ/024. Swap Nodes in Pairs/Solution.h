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
    static void swapPairsHelper(ListNode **head)
    {
        while (*head != nullptr && (*head)->next != nullptr)
        {
            ListNode **node1Next = &(*head)->next;
            ListNode **node2Next = &(*head)->next->next;
            ListNode *temp = *head;

            *head = *node1Next;
            *node1Next = *node2Next;
            *node2Next = temp;

            head = node1Next;
        }
    }

public:
    ListNode *swapPairs(ListNode *head)
    {
        swapPairsHelper(&head);

        return head;
    }
};
