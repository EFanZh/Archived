#pragma once

class RandomizedCollection
{
    vector<int> values;
    unordered_map<int, unordered_set<size_t>> indexes;
    default_random_engine re = default_random_engine(random_device()());

public:
    /** Initialize your data structure here. */
    RandomizedCollection()
    {
    }

    /** Inserts a value to the collection. Returns true if the collection did not already contain the specified element.
     */
    bool insert(int val)
    {
        const auto it = indexes.find(val);

        if (it == indexes.end())
        {
            indexes[val].emplace(values.size());
            values.emplace_back(val);

            return true;
        }
        else
        {
            it->second.emplace(values.size());
            values.emplace_back(val);

            return false;
        }
    }

    /** Removes a value from the collection. Returns true if the collection contained the specified element. */
    bool remove(int val)
    {
        const auto it = indexes.find(val);

        if (it == indexes.end())
        {
            return false;
        }
        else
        {
            if (val != values.back())
            {
                const auto eraseIndex = *it->second.begin();
                auto &lastValueIndexes = indexes.at(values.back());

                lastValueIndexes.erase(values.size() - 1);
                lastValueIndexes.emplace(eraseIndex);
                values[eraseIndex] = values.back();
                it->second.erase(eraseIndex);
            }
            else
            {
                it->second.erase(values.size() - 1);
            }

            if (it->second.empty())
            {
                indexes.erase(it);
            }

            values.pop_back();

            return true;
        }
    }

    /** Get a random element from the collection. */
    int getRandom()
    {
        return values[uniform_int_distribution<size_t>(0, values.size() - 1)(re)];
    }
};

/**
 * Your RandomizedCollection object will be instantiated and called as such:
 * RandomizedCollection obj = new RandomizedCollection();
 * bool param_1 = obj.insert(val);
 * bool param_2 = obj.remove(val);
 * int param_3 = obj.getRandom();
 */
