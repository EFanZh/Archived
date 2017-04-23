#pragma once

#include <array>
#include <functional>
#include <iomanip>
#include <iostream>
#include <map>
#include <memory>
#include <numeric>
#include <queue>
#include <random>
#include <set>
#include <sstream>
#include <stack>
#include <type_traits>
#include <unordered_map>
#include <unordered_set>

#include <cstdint>

using namespace std;

template <class T>
using Pool = vector<unique_ptr<T>>;

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

class NestedInteger
{
    bool isIntegerValue;
    int integer;
    vector<NestedInteger> list;

public:
    NestedInteger() : isIntegerValue(false)
    {
    }

    NestedInteger(int value) : isIntegerValue(true), integer(value)
    {
    }

    bool isInteger() const
    {
        return isIntegerValue;
    }

    int getInteger() const
    {
        return integer;
    }

    void add(const NestedInteger &ni)
    {
        list.emplace_back(ni);
    }

    const vector<NestedInteger> &getList() const
    {
        return list;
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

ListNode *MakeList(Pool<ListNode> &pool, initializer_list<int> input)
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

template <class T = TreeNode>
T *MakeTree(Pool<T> &pool, istream &input)
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

    T *root = nullptr;
    queue<T **> q;

    q.emplace(&root);

    while (!q.empty())
    {
        T **item = q.front();

        q.pop();

        if (c == '#')
        {
            c = input.get();
        }
        else
        {
            int x;

            tie(x, c) = GetInteger(c, input);

            pool.emplace_back(make_unique<T>(x));

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
