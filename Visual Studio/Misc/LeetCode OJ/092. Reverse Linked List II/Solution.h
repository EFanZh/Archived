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
        auto insertPoint = &head;
        auto i = size_t(1);

        for (; i < static_cast<size_t>(m); ++i)
        {
            insertPoint = &(*insertPoint)->next;
        }

        auto tail = &(*insertPoint)->next;

        for (; i < static_cast<size_t>(n); ++i)
        {
            auto temp = *tail;

            *tail = (*tail)->next;
            temp->next = *insertPoint;
            *insertPoint = temp;
        }

        return head;
    }
};
