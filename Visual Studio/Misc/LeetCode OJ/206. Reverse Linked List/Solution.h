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
    ListNode *reverseList(ListNode *head)
    {
        ListNode *reversed = nullptr;

        while (head != nullptr)
        {
            ListNode *temp = head->next;

            head->next = reversed;
            reversed = head;
            head = temp;
        }

        return reversed;
    }
};
