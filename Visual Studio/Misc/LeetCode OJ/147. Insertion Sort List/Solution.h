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
    void insertionSortListHelper(ListNode **head, ListNode *node)
    {
        for (;;)
        {
            if (*head == nullptr)
            {
                *head = node;
                node->next = nullptr;
                break;
            }
            else if (node->val < (*head)->val)
            {
                node->next = *head;
                *head = node;
                break;
            }
            else
            {
                head = &(*head)->next;
            }
        }
    }

public:
    ListNode *insertionSortList(ListNode *head)
    {
        ListNode *newHead = nullptr;

        while (head != nullptr)
        {
            ListNode *temp = head->next;

            insertionSortListHelper(&newHead, head);

            head = temp;
        }

        return newHead;
    }
};
