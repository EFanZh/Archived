#pragma once

/**
* Definition for singly-linked list with a random pointer.
* struct RandomListNode {
*     int label;
*     RandomListNode *next, *random;
*     RandomListNode(int x) : label(x), next(NULL), random(NULL) {}
* };
*/
class Solution
{
public:
    RandomListNode *copyRandomList(RandomListNode *head)
    {
        if (head == nullptr)
        {
            return nullptr;
        }

        unordered_map<const RandomListNode *, RandomListNode *> oldToNew;

        for (RandomListNode *p = head; p != nullptr; p = p->next)
        {
            oldToNew.emplace(p, new RandomListNode(p->label));
        }

        for (auto &item : oldToNew)
        {
            if (item.first->next != nullptr)
            {
                item.second->next = oldToNew.at(item.first->next);
            }

            if (item.first->random != nullptr)
            {
                item.second->random = oldToNew.at(item.first->random);
            }
        }

        return oldToNew.at(head);
    }
};
