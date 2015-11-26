#include <array>
#include <codecvt>
#include <iostream>
#include <fstream>
#include <cvt/cp936>

using namespace std;
using namespace stdext::cvt;

wstring ReadFile(const char *file, const locale &loc)
{
    const size_t bufferSize = 1024;

    wstring result(bufferSize, L'\0');
    wifstream stream(file, ios_base::in | ios_base::binary);

    stream.imbue(loc);

    while (stream.read(&result[result.length() - bufferSize], bufferSize).good())
    {
        result.resize(result.size() + bufferSize);
    }

    if (stream.rdstate() == ios_base::eofbit) // <---- It doesn't work.
    {
        const streamsize extraLength = bufferSize - stream.gcount();
        const string::difference_type length = static_cast<string::difference_type>(result.length() - extraLength);

        result.erase(result.cbegin() + length, result.cend());

        return result;
    }
    else
    {
        throw exception("Failed.");
    }
}

int main()
{
    locale gbk(locale(), new codecvt_cp936<wchar_t>());
    locale utf8(locale(), new codecvt_utf8<wchar_t>());

    ReadFile("cp936.txt", locale());
}
