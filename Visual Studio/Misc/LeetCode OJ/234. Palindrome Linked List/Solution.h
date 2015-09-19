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
        ListNode *reversed = tail;

        while (head != tail)
        {
            ListNode *temp = head->next;

            head->next = reversed;
            reversed = head;
            head = temp;
        }

        return reversed;
    }

    static bool isPalindromeHelper(const ListNode *head1, const ListNode *tail1, const ListNode *head2)
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
        ListNode *slow = head;
        ListNode *fast = head;
        ListNode *head2;

        for (;;)
        {
            if (fast == nullptr)
            {
                head2 = slow;

                break;
            }

            fast = fast->next;

            if (fast == nullptr)
            {
                head2 = slow->next;

                break;
            }

            slow = slow->next;
            fast = fast->next;
        }

        ListNode *reversed = reverse(head, slow);
        bool result = isPalindromeHelper(reversed, slow, head2);

        // reverse(reversed, slow);

        return result;
    }
};
