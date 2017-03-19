#pragma once

class Solution
{
public:
    ListNode *oddEvenList(ListNode *head)
    {
        if (head != nullptr && head->next != nullptr && head->next->next != nullptr)
        {
            ListNode *even = head->next;

            head->next = even->next;

            ListNode **oddTail = &head->next->next;
            ListNode **evenTail = &even->next;

            for (auto node = *oddTail;; node = *oddTail)
            {
                if (node == nullptr)
                {
                    *evenTail = nullptr;
                    break;
                }
                else
                {
                    *evenTail = node;
                    evenTail = &node->next;
                }

                node = node->next;

                if (node == nullptr)
                {
                    break;
                }
                else
                {
                    *oddTail = node;
                    oddTail = &node->next;
                }
            }

            *oddTail = even;
        }

        return head;
    }
};
