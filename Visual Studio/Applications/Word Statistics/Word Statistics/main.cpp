#include <algorithm>
#include <iostream>
#include <string>
#include <unordered_map>

using namespace std;

int main()
{
    wstring s;

    while (!wcin.eof())
    {
        auto ch = wcin.get();

        if (!iswspace(ch))
        {
            s += ch;
        }
    }

    const size_t n = 12;

    unordered_map<wstring, int> dict;

    for (size_t length = 1; length <= n; ++length)
    {
        for (size_t i = 0; i < s.length() - 1 - length; ++i)
        {
            ++dict[s.substr(i, length)];
        }
    }

    vector<pair<wstring, int>> v(dict.size());

    move(dict.begin(), dict.end(), v.begin());
    sort(v.begin(),
         v.end(),
         [](const pair<wstring, int> &lhs, const pair<wstring, int> &rhs)
         {
             return rhs.first.length() * rhs.second < lhs.first.length() * lhs.second;
         });

    for(const auto &item : v)
    {
        wcout << item.first << L": " << item.second << L'\n';
    }
}
