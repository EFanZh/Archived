#pragma once

class Solution
{
public:
    string removeDuplicateLetters(const string &s)
    {
        if (s.length() < 2)
        {
            return s;
        }

        auto availableCharactersData = array<size_t, 26>();
        auto availableCharacters = availableCharactersData.data() - 'a';

        for (auto ch : s)
        {
            ++availableCharacters[ch];
        }

        auto existedCharacterData = array<bool, 26>();
        auto existedCharacter = existedCharacterData.data() - 'a';

        auto result = string();

        for (auto ch : s)
        {
            if (!existedCharacter[ch])
            {
                while (!result.empty() && ch < result.back() && availableCharacters[result.back()] > 0)
                {
                    existedCharacter[result.back()] = false;
                    result.pop_back();
                }

                result += ch;
                existedCharacter[ch] = true;
            }

            --availableCharacters[ch];
        }

        return result;
    }
};
