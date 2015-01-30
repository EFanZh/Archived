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

        int carry = 0;

        while (l1 != nullptr || l2 != nullptr || carry != 0)
        {
            int result = carry;

            if (l1 != nullptr)
            {
                result += l1->val;
                l1 = l1->next;
            }

            if (l2 != nullptr)
            {
                result += l2->val;
                l2 = l2->next;
            }

            *current = new ListNode(result % 10);
            current = &(*current)->next;

            carry = result / 10;
        }

        return head;
    }
};
