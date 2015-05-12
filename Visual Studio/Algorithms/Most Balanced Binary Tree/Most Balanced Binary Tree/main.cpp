#include <algorithm>
#include <iostream>
#include <utility>
#include <vector>
#include <map>
#include <cassert>

using namespace std;

struct TKey
{
    int key;

    TKey(int key) : key(key)
    {
    }

    bool operator <(const TKey &rhs) const
    {
        return key < rhs.key;
    }
};

struct TValue
{
    int value;

    TValue(int value) : value(value)
    {
    }
};

class TreeNode
{
    pair<const TKey, TValue> core;
    size_t count;
    TreeNode *leftChild;
    TreeNode *rightChild;

    template<bool Left>
    TreeNode *&GetChildReference();

    template<>
    TreeNode *&GetChildReference<true>()
    {
        return leftChild;
    }

    template<>
    TreeNode *&GetChildReference<false>()
    {
        return rightChild;
    }

    template<bool Left>
    static void InsertBound(TreeNode *&root, TreeNode *node)
    {
        if (root == nullptr)
        {
            root = node;
            root->count = 1;
            root->leftChild = nullptr;
            root->rightChild = nullptr;
        }
        else
        {
            if (root->GetChildReference<Left>()->Count() > root->GetChildReference<!Left>()->Count())
            {
                TreeNode *newRoot = InsertAndExtractBound<Left>(root->GetChildReference<Left>(), node);

                newRoot->leftChild = root->leftChild;
                newRoot->rightChild = root->rightChild;
                newRoot->count = root->count + 1;
                InsertBound<Left>(newRoot->GetChildReference<!Left>(), root);
                root = newRoot;
            }
            else
            {
                ++root->count;
                InsertBound<Left>(root->GetChildReference<Left>(), node);
            }
        }
    }

    template<bool Left>
    static TreeNode *ExtractBound(TreeNode *&root)
    {
        assert(root != nullptr);

        if (root->GetChildReference<Left>()->Count() > root->GetChildReference<!Left>()->Count())
        {
            TreeNode *newRoot = ExtractBound<Left>(root->GetChildReference<Left>());

            newRoot->leftChild = root->leftChild;
            newRoot->rightChild = root->rightChild;
            newRoot->count = root->count - 1;

            TreeNode *result = InsertAndExtractBound<Left>(newRoot->GetChildReference<!Left>(), root);

            root = newRoot;

            return result;
        }
        else
        {
            if (root->GetChildReference<!Left>() == nullptr)
            {
                TreeNode *result = root;

                root = root->GetChildReference<Left>();

                return result;
            }
            else
            {
                --root->count;

                return ExtractBound<Left>(root->GetChildReference<!Left>());
            }
        }
    }

    template<bool Left>
    static TreeNode *InsertAndExtractBound(TreeNode *&root, TreeNode *node)
    {
        if (root == nullptr)
        {
            return node;
        }
        else
        {
            if (root->GetChildReference<Left>()->Count() < root->GetChildReference<!Left>()->Count())
            {
                InsertBound<Left>(root->GetChildReference<Left>(), node);

                return ExtractBound<Left>(root->GetChildReference<!Left>());
            }
            else
            {
                TreeNode *newRoot = InsertAndExtractBound<Left>(root->GetChildReference<Left>(), node);

                newRoot->leftChild = root->leftChild;
                newRoot->rightChild = root->rightChild;
                newRoot->count = root->count;

                TreeNode *result = InsertAndExtractBound<Left>(newRoot->GetChildReference<!Left>(), root);

                root = newRoot;

                return result;
            }
        }
    }

    template<bool Left>
    static bool CompareKey(const TKey &lhs, const TKey &rhs);

    template<>
    static bool CompareKey<true>(const TKey &lhs, const TKey &rhs)
    {
        return lhs < rhs;
    }

    template<>
    static bool CompareKey<false>(const TKey &lhs, const TKey &rhs)
    {
        return rhs < lhs;
    }

    template<bool Left>
    static TreeNode *InsertAndExtract(TreeNode *&root, TKey key, TValue value)
    {
        if (root == nullptr)
        {
            return new TreeNode(key, value);
        }
        else
        {
            if (CompareKey<Left>(key, root->core.first))
            {
                if (root->GetChildReference<Left>()->Count() < root->GetChildReference<!Left>()->Count())
                {
                    // If a new node is inserted.
                    if (Insert(root->GetChildReference<Left>(), key, value))
                    {
                        return ExtractBound<Left>(root->GetChildReference<!Left>());
                    }
                    else
                    {
                        return nullptr;
                    }
                }
                else
                {
                    TreeNode *newRoot = InsertAndExtract<Left>(root->GetChildReference<Left>(), key, value);

                    // If a new node is inserted.
                    if (newRoot != nullptr)
                    {
                        newRoot->leftChild = root->leftChild;
                        newRoot->rightChild = root->rightChild;
                        newRoot->count = root->count;

                        TreeNode *result = InsertAndExtractBound<Left>(newRoot->GetChildReference<!Left>(), root);

                        root = newRoot;

                        return result;
                    }
                    else
                    {
                        return nullptr;
                    }
                }
            }
            else if (CompareKey<Left>(root->core.first, key))
            {
                return InsertAndExtract<Left>(root->GetChildReference<!Left>(), key, value);
            }
            else
            {
                root->core.second = value;

                return nullptr;
            }
        }
    }

    template<bool Left>
    static bool InsertSegment(TreeNode *&root, TKey key, TValue value)
    {
        if (root->GetChildReference<Left>()->Count() > root->GetChildReference<!Left>()->Count())
        {
            TreeNode *newRoot = InsertAndExtract<Left>(root->GetChildReference<Left>(), key, value);

            // If a new node is inserted.
            if (newRoot != nullptr)
            {
                newRoot->leftChild = root->leftChild;
                newRoot->rightChild = root->rightChild;
                newRoot->count = root->count + 1;
                InsertBound<Left>(newRoot->GetChildReference<!Left>(), root);
                root = newRoot;

                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            if (Insert(root->GetChildReference<Left>(), key, value))
            {
                ++root->count;

                return true;
            }
            else
            {
                return false;
            }
        }
    }

public:
    TreeNode(TKey key, TValue value) :
        core(key, value),
        count(1),
        leftChild(nullptr),
        rightChild(nullptr)
    {
    }

    size_t Count() const
    {
        return this == nullptr ? 0 : count;
    }

    static bool Insert(TreeNode *&root, TKey key, TValue value)
    {
        if (root == nullptr)
        {
            root = new TreeNode(key, value);

            return true;
        }
        else
        {
            if (key < root->core.first)
            {
                return InsertSegment<true>(root, key, value);
            }
            else if (root->core.first < key)
            {
                return InsertSegment<false>(root, key, value);
            }
            else
            {
                root->core.second = value;

                return false;
            }
        }
    }

    pair<bool, int> IsValid(int minValue, int MaxValue) const
    {
        if (this == nullptr)
        {
            return{ true, 0 };
        }
        else
        {
            if (minValue < core.first.key && core.first.key < MaxValue)
            {
                auto leftResult = leftChild->IsValid(minValue, core.first.key);

                if (leftResult.first)
                {
                    auto rightResult = rightChild->IsValid(core.first.key, MaxValue);

                    if (rightResult.first)
                    {
                        if (abs(leftResult.second - rightResult.second) <= 1 && count == leftResult.second + rightResult.second + 1)
                        {
                            return{ true, count };
                        }
                    }
                }
            }

            return{ false, 0 };
        }
    }
};

int main()
{
    TreeNode *root = nullptr;

    for (int i = 0; i < 10000000; ++i)
    {
        TreeNode::Insert(root, i, i);

        if (root->IsValid(numeric_limits<int>::min(), numeric_limits<int>::max()).first)
        {
            cout << i << ": OK\n";
        }
        else
        {
            break;
        }
    }
}
