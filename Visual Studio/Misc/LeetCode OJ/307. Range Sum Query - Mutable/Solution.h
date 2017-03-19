#pragma once

class NumArray
{
    struct Node
    {
        vector<int>::size_type from;
        vector<int>::size_type to;

        vector<int>::size_type getRoot() const
        {
            if (from + 1 == to)
            {
                return from;
            }
            else
            {
                return from + (to - from + 1) / 4 * 2 - 1;
            }
        }
    };

    vector<int> tree;

    template <class InputIterator, class OutputIterator>
    int makeTree(InputIterator left, InputIterator right, OutputIterator output)
    {
        const auto length = right - left;

        if (length == 0)
        {
            return 0;
        }
        else if (length == 1)
        {
            *output = *left;

            return *output;
        }
        else
        {
            const auto middle = left + length / 2;
            const auto middleOutput = output + (length / 2 * 2 - 1);
            const auto leftResult = makeTree(left, middle, output);
            const auto rightResult = makeTree(middle, right, middleOutput + 1);

            *middleOutput = leftResult + rightResult;

            return *middleOutput;
        }
    }

    void updateHelper(Node node, vector<int>::size_type i, int offset)
    {
        for (;;)
        {
            auto root = node.getRoot();

            tree[root] += offset;

            if (i < root)
            {
                node.to = root;
            }
            else if (i > root)
            {
                node.from = root + 1;
            }
            else
            {
                break;
            }
        }
    }

    int sumRangeHelper(Node node, vector<int>::size_type i, vector<int>::size_type j)
    {
        for (;;)
        {
            auto root = node.getRoot();

            if (i == node.from && j == node.to)
            {
                return tree[root];
            }

            if (j <= root)
            {
                node.to = root;
            }
            else if (i > root)
            {
                node.from = root + 1;
            }
            else
            {
                return sumRangeHelper({ node.from, root }, i, root) +
                       sumRangeHelper({ root + 1, node.to }, root + 1, j);
            }
        }
    }

public:
    explicit NumArray(const vector<int> &nums) : tree(nums.size() < 2 ? nums.size() : nums.size() * 2 - 1)
    {
        makeTree(nums.cbegin(), nums.cend(), tree.begin());
    }

    void update(int i, int val)
    {
        updateHelper({ 0, tree.size() }, i * 2, val - tree[i * 2]);
    }

    int sumRange(int i, int j)
    {
        return sumRangeHelper({ 0, tree.size() }, i * 2, j * 2 + 1);
    }
};

// Your NumArray object will be instantiated and called as such:
// NumArray numArray(nums);
// numArray.sumRange(0, 1);
// numArray.update(1, 10);
// numArray.sumRange(1, 2);
