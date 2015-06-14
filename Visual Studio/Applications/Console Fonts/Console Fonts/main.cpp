// See http://support.microsoft.com/kb/247815 for more information.

#include <algorithm>
#include <iostream>
#include <map>
#include <string>
#include <sstream>

#define WIN32_LEAN_AND_MEAN

#include <Windows.h>

using namespace std;

int CALLBACK EnumFontFamExProc(const LOGFONT *lpelfe, const TEXTMETRIC *lpntme, DWORD FontType, LPARAM lParam);

int main()
{
    ostringstream locale_name_stream;

    locale_name_stream << "." << GetConsoleCP();
    setlocale(LC_ALL, locale_name_stream.str().c_str());

    map<wstring, bool> font_table;
    LOGFONT lf = {};

    lf.lfCharSet = DEFAULT_CHARSET;
    EnumFontFamiliesEx(GetDC(HWND_DESKTOP), &lf, EnumFontFamExProc, reinterpret_cast<LPARAM>(&font_table), 0);

    auto max_font_name_length = max_element(font_table.begin(), font_table.end(), [](const pair<wstring, bool> &x, const pair<wstring, bool> &y) { return x.first.length() < y.first.length(); })->first.length();

    for (auto &font_item : font_table)
    {
        wcout.setf(ios::left);
        wcout.width(max_font_name_length);
        wcout << font_item.first << "  " << font_item.second << endl;
    }
}

int CALLBACK EnumFontFamExProc(const LOGFONT *lpelfe, const TEXTMETRIC *lpntme, DWORD FontType, LPARAM lParam)
{
    UNREFERENCED_PARAMETER(lpntme);

    bool is_truetype = (FontType & TRUETYPE_FONTTYPE) != 0;

    auto p_font_table = reinterpret_cast<map<wstring, bool> *>(lParam);

    if ((lpelfe->lfPitchAndFamily & 0x3) == FIXED_PITCH && lpelfe->lfItalic != TRUE)
    {
        if (is_truetype ? (lpelfe->lfPitchAndFamily & 0xf0) == FF_MODERN : lpelfe->lfCharSet == OEM_CHARSET)
        {
            // Should decect if the font has negative A or C widths.

            if (is_truetype ? lpelfe->lfCharSet == GB2312_CHARSET : lpelfe->lfFaceName == wstring(L"Terminal"))
            {
                (*p_font_table)[lpelfe->lfFaceName] = true;
            }
            else
            {
                if (p_font_table->find(lpelfe->lfFaceName) == p_font_table->end())
                {
                    (*p_font_table)[lpelfe->lfFaceName] = false;
                }
            }
        }
    }

    return 1;
}
