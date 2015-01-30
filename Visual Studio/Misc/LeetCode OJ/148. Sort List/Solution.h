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
    static ListNode *getTail(ListNode *head)
    {
        while (head->next != nullptr && head->next->val >= head->val)
        {
            head = head->next;
        }

        return head;
    }

    static ListNode **merge(ListNode **head, ListNode *tail1, ListNode *tail2)
    {
        ListNode *head1 = *head;
        ListNode *head2 = tail1->next;
        ListNode *end1 = head2;
        ListNode *end2 = tail2->next;

        while (head1 != end1 && head2 != end2)
        {
            if (head1->val <= head2->val)
            {
                *head = head1;
                head1 = head1->next;
            }
            else
            {
                *head = head2;
                head2 = head2->next;
            }

            head = &(*head)->next;
        }

        if (head1 == end1)
        {
            *head = head2;

            return &tail2->next;
        }
        else
        {
            *head = head1;
            tail1->next = end2;

            return &tail1->next;
        }
    }

    static ListNode **mergeOne(ListNode **head)
    {
        ListNode *tail1 = getTail(*head);

        return tail1->next == nullptr ? &tail1->next : merge(head, tail1, getTail(tail1->next));
    }

public:
    ListNode *sortList(ListNode *head)
    {
        if (head != nullptr)
        {
            for (;;)
            {
                ListNode **nextHead = mergeOne(&head);

                if (*nextHead == nullptr)
                {
                    break;
                }

                nextHead = mergeOne(nextHead);

                while (*nextHead != nullptr)
                {
                    nextHead = mergeOne(nextHead);
                }
            }
        }

        return head;
    }
};
