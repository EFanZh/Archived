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
    ListNode *detectCycle(ListNode *head)
    {
        if (head == nullptr || head->next == nullptr)
        {
            return nullptr;
        }

        ListNode *slow = head->next;
        ListNode *fast = head->next->next;

        for (;;)
        {
            if (fast == nullptr)
            {
                return nullptr;
            }
            if (fast->next == nullptr)
            {
                return nullptr;
            }

            if (fast == slow)
            {
                break;
            }

            slow = slow->next;
            fast = fast->next->next;
        }

        for (slow = head; slow != fast; slow = slow->next)
        {
            fast = fast->next;
        }

        return slow;
    }
};
