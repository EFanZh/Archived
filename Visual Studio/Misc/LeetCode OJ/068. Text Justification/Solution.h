#pragma once

class Solution
{
public:
    vector<string> fullJustify(vector<string> words, int L)
    {
        vector<string> result;

        for (size_t i = 0; i != words.size();)
        {
            size_t line_start = i;
            size_t length = words[i].length();
            size_t n = 0;

            ++i;

            for (; i != words.size() && length + 1 + words[i].length() <= static_cast<size_t>(L); ++i)
            {
                length += 1 + words[i].length();
                ++n;
            }

            string line = words[line_start];

            if (n > 0)
            {
                if (i != words.size())
                {
                    size_t available_spaces = L - length;
                    size_t spaces = 1 + available_spaces / n;
                    size_t additional_spaces = available_spaces % n;

                    for (size_t j = line_start + 1; j <= line_start + additional_spaces; ++j)
                    {
                        line += string(spaces + 1, ' ');
                        line += words[j];
                    }

                    for (size_t j = line_start + additional_spaces + 1; j != i; ++j)
                    {
                        line += string(spaces, ' ');
                        line += words[j];
                    }
                }
                else
                {
                    for (size_t j = line_start + 1; j != i; ++j)
                    {
                        line += ' ';
                        line += words[j];
                    }
                }
            }

            line += string(L - line.length(), ' ');

            result.emplace_back(move(line));
        }

        return result;
    }
};
