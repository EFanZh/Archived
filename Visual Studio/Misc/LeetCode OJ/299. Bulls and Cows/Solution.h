#pragma once

class Solution
{
public:
    string getHint(const string &secret, const string &guess)
    {
        const size_t digits = 10;
        const size_t length = secret.length();
        size_t bulls = 0;

        array<size_t, digits> secretMap = { 0 };
        array<size_t, digits> guessMap = { 0 };

        for (size_t i = 0; i < length; ++i)
        {
            if (secret[i] == guess[i])
            {
                ++bulls;
            }
            else
            {
                ++secretMap[secret[i] - '0'];
                ++guessMap[guess[i] - '0'];
            }
        }

        size_t cows = 0;

        for (size_t i = 0; i < digits; ++i)
        {
            cows += min(secretMap[i], guessMap[i]);
        }

        ostringstream result;

        result << bulls << 'A' << cows << 'B';

        return result.str();
    }
};
