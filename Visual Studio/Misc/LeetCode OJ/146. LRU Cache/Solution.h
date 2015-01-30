#pragma once

class LRUCache
{
    size_t capacity;
    unordered_map<int, tuple<int, list<void *>::iterator>> cache;
    list<void *> uses;

public:
    LRUCache(int capacity) : capacity(capacity)
    {
        cache.reserve(capacity);
    }

    int get(int key)
    {
        auto it = cache.find(key);

        if (it == cache.cend())
        {
            return -1;
        }
        else
        {
            uses.erase(std::get<1>(it->second));
            uses.emplace_front(&*it);
            std::get<1>(it->second) = uses.begin();

            return std::get<0>(it->second);
        }
    }

    void set(int key, int value)
    {
        auto it = cache.find(key);

        if (it == cache.cend())
        {
            if (cache.size() == capacity)
            {
                auto *tail = static_cast<pair<int, tuple<int, list<void *>::iterator>> *>(uses.back());

                uses.pop_back();
                cache.erase(tail->first);
            }

            auto it_new_item = cache.emplace(key, make_tuple(value, list<void *>::iterator())).first;

            uses.emplace_front(&*it_new_item);
            std::get<1>(it_new_item->second) = uses.begin();
        }
        else
        {
            std::get<0>(it->second) = value;
            uses.erase(std::get<1>(it->second));
            uses.emplace_front(&*it);
            std::get<1>(it->second) = uses.begin();
        }
    }
};
