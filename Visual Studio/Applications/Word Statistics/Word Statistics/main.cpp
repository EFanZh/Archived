#include <algorithm>
#include <codecvt>
#include <iostream>
#include <locale>
#include <string>
#include <unordered_map>

using namespace std;

int main()
{
    u32string s;
    wbuffer_convert<codecvt_utf16<char32_t, 0x10ffff, codecvt_mode(5)>, char32_t> wbuffer(cin.rdbuf());
    basic_istream<char32_t> wstream(&wbuffer);

    while (!wstream.eof())
    {
        char32_t ch = wstream.get();

        if (!iswspace(ch))
        {
            s += towupper(ch);
        }
    }

    for (auto ch : s)
    {
        cout << ch << endl;
    }

    const size_t n = 12;

    unordered_map<wstring, int> dict;
    wstring_convert<codecvt<wchar_t, char, mbstate_t>, wchar_t> convert;

    for (size_t length = 1; length <= n; ++length)
    {
        for (size_t i = 0; i + (1 + length) < s.length(); ++i)
        {
            auto seg = s.substr(i, length);
            auto k = convert.from_bytes((char *)seg.data(), (char *)(seg.data() + seg.length()));

            ++dict[k];
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

    for (const auto &item : v)
    {
        wcout << item.first << L": " << item.second << L'\n';
    }
}
