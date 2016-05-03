#include <iostream>

using namespace std;

inline void PrintEscapsedCharacter(char ch)
{
    switch (ch)
    {
        case '\"':
            cout << '\\' << '"';
            break;

        case '\\':
            cout << '\\' << '\\';
            break;

        case '\a':
            cout << '\\' << 'a';
            break;

        case '\b':
            cout << '\\' << 'b';
            break;

        case '\f':
            cout << '\\' << 'f';
            break;

        case '\n':
            cout << '\\' << 'n';
            break;

        case '\r':
            cout << '\\' << 'r';
            break;

        case '\t':
            cout << '\\' << 't';
            break;

        case '\v':
            cout << '\\' << 'v';
            break;

        default:
            cout << ch;
            break;
    }
}

int main(int argc, char *argv[])
{
    for (auto i = 0; i < argc; ++i)
    {
        cout << '"';

        for (auto p = argv[i]; *p != '\0'; ++p)
        {
            PrintEscapsedCharacter(*p);
        }

        cout << '"' << '\n';
    }
}
