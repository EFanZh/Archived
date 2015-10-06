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
    ListNode *addTwoNumbers(ListNode *l1, ListNode *l2)
    {
        ListNode *head;
        ListNode **current = &head;
        ListNode *rest;

        int carry = 0;

        for (;;)
        {
            if (l1 == nullptr)
            {
                rest = l2;
                break;
            }
            else if (l2 == nullptr) // This "else" improves speed in OJ.
            {
                rest = l1;
                break;
            }

            int result = carry + l1->val + l2->val;

            *current = new ListNode(result % 10);
            current = &(*current)->next;
            carry = result / 10;
            l1 = l1->next;
            l2 = l2->next;
        }

        for (; rest != nullptr; rest = rest->next)
        {
            int result = carry + rest->val;

            *current = new ListNode(result % 10);
            current = &(*current)->next;
            carry = result / 10;
        }

        if (carry > 0)
        {
            *current = new ListNode(carry);
        }

        return head;
    }
};
