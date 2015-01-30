#pragma once

class Solution
{
public:
    string simplifyPath(string path)
    {
        vector<pair<size_t, size_t>> components;

        for (size_t i = 0; i < path.length();)
        {
            if (path[i] == '/')
            {
                size_t j = i + 1;

                while (j < path.length() && path[j] != '/')
                {
                    ++j;
                }

                if (j == i + 1 || j - (i + 1) == 1 && path[i + 1] == '.')
                {
                    // Do nothing.
                }
                else if (j - (i + 1) == 2 && path[i + 1] == '.' && path[i + 2] == '.')
                {
                    if (!components.empty())
                    {
                        components.erase(components.end());
                    }
                }
                else
                {
                    components.emplace_back(i, j);
                }

                i = j;
            }
        }

        if (components.empty())
        {
            return "/";
        }
        else
        {
            string result;

            for (auto &item : components)
            {
                result.insert(result.end(), path.cbegin() + item.first, path.cbegin() + item.second);
            }

            return result;
        }
    }
};
