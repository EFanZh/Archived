#pragma once

class Solution
{
public:
    int firstUniqChar(const string &s)
    {
        static const auto notSeenIndex = numeric_limits<size_t>::max();
        static const auto multipleIndex = notSeenIndex - 1;

        array<size_t, 26> letters;
        auto uniqueCharacters = set<size_t>();

        fill(letters.begin(), letters.end(), notSeenIndex);

        for (auto i = size_t(0); i < s.length(); ++i)
        {
            auto &current = letters[static_cast<size_t>(s[i] - 'a')];

            if (current == notSeenIndex)
            {
                uniqueCharacters.emplace_hint(uniqueCharacters.end(), i);
                current = i;
            }
            else if (current != multipleIndex)
            {
                uniqueCharacters.erase(current);
                current = multipleIndex;
            }
        }

        return uniqueCharacters.empty() ? -1 : static_cast<int>(*uniqueCharacters.begin());
    }
};
