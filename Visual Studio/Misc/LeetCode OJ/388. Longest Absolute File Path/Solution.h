#pragma once

class Solution
{
public:
    int lengthLongestPath(const string &input)
    {
        const auto inputSize = input.size();
        auto pathLengths = vector<size_t>{ static_cast<size_t>(-1) };
        auto result = size_t(0);

        for (auto i = size_t(0);;)
        {
            auto tabs = size_t(0);

            for (; input[i] == '\t'; ++i)
            {
                ++tabs;
            }

            auto fileNameLength = size_t(1);

            for (++i;; ++i)
            {
                if (i == inputSize)
                {
                    return static_cast<int>(result);
                }
                else if (input[i] == '\n')
                {
                    auto currentLength = pathLengths[tabs] + 1 + fileNameLength;

                    pathLengths.resize(tabs + 2);
                    pathLengths.back() = currentLength;

                    ++i;

                    break;
                }
                else
                {
                    ++fileNameLength;

                    if (input[i] == '.')
                    {
                        // It's a file.

                        for (++i;; ++i)
                        {
                            if (i == inputSize)
                            {
                                return static_cast<int>(max(result, pathLengths[tabs] + 1 + fileNameLength));
                            }
                            else if (input[i] == '\n')
                            {
                                pathLengths.resize(tabs + 1);
                                result = max(result, pathLengths[tabs] + 1 + fileNameLength);

                                ++i;

                                goto NextLine;
                            }
                            else
                            {
                                ++fileNameLength;
                            }
                        }
                    }
                }
            }

        NextLine:;
        }
    }
};
