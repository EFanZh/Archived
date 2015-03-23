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
    ListNode *partition(ListNode *head, int x)
    {
        ListNode *head1 = nullptr;
        ListNode **tail1 = &head1;
        ListNode *head2 = nullptr;
        ListNode **tail2 = &head2;

        while (head != nullptr)
        {
            if (head->val < x)
            {
                *tail1 = head;
                tail1 = &head->next;
            }
            else
            {
                *tail2 = head;
                tail2 = &head->next;
            }

            head = head->next;
        }

        *tail1 = head2;
        *tail2 = nullptr;

        return head1;
    }
};
