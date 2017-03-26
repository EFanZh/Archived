#pragma once

class Solution
{
    using Iterator = string::const_iterator;

    static size_t parseInteger(Iterator &first)
    {
        auto result = static_cast<size_t>(*first - '0');

        for (++first; isdigit(*first); ++first)
        {
            result *= 10;
            result += static_cast<size_t>(*first - '0');
        }

        return result;
    }

    static void parseItem(Iterator &first, Iterator last, string &result)
    {
        if (isdigit(*first))
        {
            const auto count = parseInteger(first);

            // Skip opening bracket.
            ++first;

            const auto savedLength = result.length();

            parseItemList(first, last, result);

            const auto bodyLength = result.length() - savedLength;

            for (auto i = size_t(1); i < count; ++i)
            {
                result.insert(result.length(), result, savedLength, bodyLength);
            }

            // Skip closing bracket.
            ++first;
        }
        else
        {
            result += *first;

            for (++first; islower(*first); ++first)
            {
                result += *first;
            }
        }
    }

    static void parseItemList(Iterator &first, Iterator last, string &result)
    {
        while (first != last && isalnum(*first))
        {
            parseItem(first, last, result);
        }
    }

public:
    string decodeString(const string &s)
    {
        auto result = string();
        auto first = s.cbegin();

        parseItemList(first, s.cend(), result);

        return result;
    }
};
