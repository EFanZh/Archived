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
    static ListNode *reverse(ListNode *head, ListNode *tail)
    {
        auto reversed = tail;

        while (head != tail)
        {
            const auto temp = head->next;

            head->next = reversed;
            reversed = head;
            head = temp;
        }

        return reversed;
    }

    static bool isPalindromeHelper(const ListNode *head1, const ListNode *head2)
    {
        for (; head2 != nullptr; head2 = head2->next)
        {
            if (head1->val != head2->val)
            {
                return false;
            }

            head1 = head1->next;
        }

        return true;
    }

public:
    bool isPalindrome(ListNode *head)
    {
        auto slow = head;
        auto fast = head;

        for (;;)
        {
            if (fast == nullptr)
            {
                break;
            }

            fast = fast->next;

            if (fast == nullptr)
            {
                break;
            }

            slow = slow->next;
            fast = fast->next;
        }

        const auto reversed = reverse(head, slow);
        const auto result = isPalindromeHelper(reversed, slow);

        // reverse(reversed, slow);

        return result;
    }
};
