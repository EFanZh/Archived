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
    ListNode *head;
    size_t length;
    default_random_engine re;
    uniform_int_distribution<size_t> d;

    static size_t getLength(const ListNode *head)
    {
        auto result = size_t(1);

        for (; head->next != nullptr; head = head->next)
        {
            ++result;
        }

        return result;
    }

public:
    /** @param head The linked list's head.
        Note that the head is guaranteed to be not null, so it contains at least one node. */
    Solution(ListNode *head) : head(head), length(getLength(head)), d(0, length - 1)
    {
    }

    /** Returns a random node's value. */
    int getRandom()
    {
        auto n = d(re);
        auto node = head;

        for (auto i = size_t(0); i < n; ++i)
        {
            node = node->next;
        }

        return node->val;
    }
};

/**
 * Your Solution object will be instantiated and called as such:
 * Solution obj = new Solution(head);
 * int param_1 = obj.getRandom();
 */
