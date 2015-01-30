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
    bool hasCycle(ListNode *head)
    {
        ListNode *fast_runner = head;

        while (fast_runner != nullptr && fast_runner->next != nullptr)
        {
            head = head->next;
            fast_runner = fast_runner->next->next;

            if (fast_runner == head)
            {
                return true;
            }
        }

        return false;
    }
};
