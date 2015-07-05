#pragma once

#include <array>
#include <functional>
#include <iostream>
#include <map>
#include <memory>
#include <numeric>
#include <queue>
#include <set>
#include <stack>
#include <sstream>
#include <unordered_map>
#include <unordered_set>

#include <cstdint>

using namespace std;

struct Interval
{
    int start = 0;
    int end = 0;

    Interval()
    {
    }

    Interval(int s, int e) : start(s), end(e)
    {
    }
};

struct ListNode
{
    int val;
    ListNode *next = nullptr;

    ListNode(int x) : val(x)
    {
    }
};

struct Point
{
    int x = 0;
    int y = 0;

    Point()
    {
    }

    Point(int a, int b) : x(a), y(b)
    {
    }
};

struct RandomListNode
{
    int label;
    RandomListNode *next = nullptr;
    RandomListNode *random = nullptr;

    RandomListNode(int x) : label(x)
    {
    }
};

struct TreeLinkNode
{
    int val;
    TreeLinkNode *left = nullptr;
    TreeLinkNode *right = nullptr;
    TreeLinkNode *next = nullptr;

    TreeLinkNode(int x) : val(x)
    {
    }
};

struct TreeNode
{
    int val;
    TreeNode *left = nullptr;
    TreeNode *right = nullptr;

    TreeNode(int x) : val(x)
    {
    }
};

struct UndirectedGraphNode
{
    int label;
    vector<UndirectedGraphNode *> neighbors;

    UndirectedGraphNode(int x) : label(x)
    {
    }
};

namespace Detail
{
    int SkipBlank(int c, istream &input)
    {
        while (isspace(c))
        {
            c = input.get();
        }

        return c;
    }

    pair<int, int> GetInteger(int c, istream &input)
    {
        int x;

        if (c == '-')
        {
            x = 0;
            c = input.get();
            while (isdigit(c))
            {
                x *= 10;
                x -= c - '0';
                c = input.get();
            }
        }
        else if (isdigit(c))
        {
            x = c - '0';
            c = input.get();
            while (isdigit(c))
            {
                x *= 10;
                x += c - '0';
                c = input.get();
            }
        }
        else
        {
            throw exception();
        }

        return { x, c };
    }
}

template <class T>
vector<vector<char>> convertStringToMatrix(const T &container)
{
    vector<vector<char>> result;

    for (auto &&row : container)
    {
        result.emplace_back();

        for (const char *p = row; *p != '\0'; ++p)
        {
            result.back().emplace_back(*p);
        }
    }

    return result;
}

ListNode *MakeList(vector<unique_ptr<ListNode>> &pool, initializer_list<int> input)
{
    ListNode *head = nullptr;
    ListNode **p = &head;

    for (auto i : input)
    {
        pool.emplace_back(make_unique<ListNode>(i));

        *p = pool.back().get();
        p = &(*p)->next;
    }

    return head;
}

TreeNode *MakeTree(vector<unique_ptr<TreeNode>> &pool, istream &input)
{
    using namespace Detail;

    int c = SkipBlank(input.get(), input);

    if (c != '{')
    {
        return nullptr;
    }

    c = SkipBlank(input.get(), input);

    if (c == '}')
    {
        return nullptr;
    }

    TreeNode *root = nullptr;
    queue<TreeNode **> q;

    q.emplace(&root);

    while (!q.empty())
    {
        TreeNode **item = q.front();

        q.pop();

        if (c == '#')
        {
            c = input.get();
        }
        else
        {
            int x;

            tie(x, c) = GetInteger(c, input);

            pool.emplace_back(make_unique<TreeNode>(x));

            *item = pool.back().get();
            q.emplace(&(*item)->left);
            q.emplace(&(*item)->right);
        }

        c = SkipBlank(c, input);

        if (c == '}')
        {
            break;
        }
        else if (c == ',')
        {
            c = SkipBlank(input.get(), input);
        }
    }

    return root;
}
