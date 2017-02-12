#pragma once

class RandomizedSet
{
    vector<int> values;
    unordered_map<int, size_t> indexes;
    default_random_engine re = default_random_engine(random_device()());

public:
    /** Initialize your data structure here. */
    RandomizedSet()
    {
    }

    /** Inserts a value to the set. Returns true if the set did not already contain the specified element. */
    bool insert(int val)
    {
        if (indexes.count(val) == 0)
        {
            indexes.emplace(val, values.size());
            values.emplace_back(val);

            return true;
        }
        else
        {
            return false;
        }
    }

    /** Removes a value from the set. Returns true if the set contained the specified element. */
    bool remove(int val)
    {
        const auto it = indexes.find(val);

        if (it == indexes.end())
        {
            return false;
        }
        else
        {
            const auto index = it->second;

            indexes[values.back()] = index;
            values[index] = values.back();

            values.pop_back();
            indexes.erase(it);

            return true;
        }
    }

    /** Get a random element from the set. */
    int getRandom()
    {
        return values[uniform_int_distribution<size_t>(0, values.size() - 1)(re)];
    }
};

/**
 * Your RandomizedSet object will be instantiated and called as such:
 * RandomizedSet obj = new RandomizedSet();
 * bool param_1 = obj.insert(val);
 * bool param_2 = obj.remove(val);
 * int param_3 = obj.getRandom();
 */
