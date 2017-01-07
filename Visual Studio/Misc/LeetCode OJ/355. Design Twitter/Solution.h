#pragma once

class Twitter
{
    typedef int Time;

    struct User
    {
        unordered_set<const User *> following;
        vector<pair<Time, int>> tweets;

        User()
        {
            following.emplace(this);
        }
    };

    Time time = 0;
    unordered_map<int, User> users;

public:
    /** Initialize your data structure here. */
    Twitter()
    {
    }

    /** Compose a new tweet. */
    void postTweet(int userId, int tweetId)
    {
        ++time;

        users[userId].tweets.emplace_back(time, tweetId);
    }

    /** Retrieve the 10 most recent tweet ids in the user's news feed. Each item in the news feed must be posted by
     * users who the user followed or by the user herself. Tweets must be ordered from most recent to least recent. */
    vector<int> getNewsFeed(int userId)
    {
        using Iterator = vector<pair<Time, int>>::const_reverse_iterator;

        struct Comparer
        {
            bool operator()(const pair<Iterator, Iterator> *lhs, const pair<Iterator, Iterator> *rhs) const
            {
                return lhs->first->first < rhs->first->first;
            }
        };

        const auto &user = users[userId];
        auto iterators = vector<pair<Iterator, Iterator>>();
        auto q = priority_queue<pair<Iterator, Iterator> *, vector<pair<Iterator, Iterator> *>, Comparer>();
        auto result = vector<int>();

        iterators.reserve(user.following.size());

        for (const auto otherUser : user.following)
        {
            if (!otherUser->tweets.empty())
            {
                iterators.emplace_back(otherUser->tweets.crbegin(), otherUser->tweets.crend());
                q.emplace(&iterators.back());
            }
        }

        while (!q.empty())
        {
            const auto current = q.top();

            result.emplace_back(current->first->second);

            if (result.size() < 10)
            {
                q.pop();

                ++current->first;
                if (current->first != current->second)
                {
                    q.emplace(current);
                }
            }
            else
            {
                break;
            }
        }

        return result;
    }

    /** Follower follows a followee. If the operation is invalid, it should be a no-op. */
    void follow(int followerId, int followeeId)
    {
        users[followerId].following.emplace(&users[followeeId]);
    }

    /** Follower unfollows a followee. If the operation is invalid, it should be a no-op. */
    void unfollow(int followerId, int followeeId)
    {
        if (followerId != followeeId)
        {
            users[followerId].following.erase(&users[followeeId]);
        }
    }
};

/**
 * Your Twitter object will be instantiated and called as such:
 * Twitter obj = new Twitter();
 * obj.postTweet(userId,tweetId);
 * vector<int> param_2 = obj.getNewsFeed(userId);
 * obj.follow(followerId,followeeId);
 * obj.unfollow(followerId,followeeId);
 */
