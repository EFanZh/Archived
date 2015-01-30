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
    ListNode *reverseBetween(ListNode *head, int m, int n)
    {
        ListNode **insertPoint = &head;
        size_t i = 1;

        for (; i < m; ++i)
        {
            insertPoint = &(*insertPoint)->next;
        }

        ListNode **tail = &(*insertPoint)->next;

        for (; i < n; ++i)
        {
            ListNode *temp = *tail;

            *tail = (*tail)->next;
            temp->next = *insertPoint;
            *insertPoint = temp;
        }

        return head;
    }
};
