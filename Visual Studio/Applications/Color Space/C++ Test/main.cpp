#include <iostream>
#include <Windows.h>
#include <Icm.h>

#pragma comment(lib, "Mscms.lib")

using namespace std;

int main()
{
    TCHAR buffer[1000];
    DWORD size = ARRAYSIZE(buffer);

    /*    WcsGetDefaultColorProfile(WCS_PROFILE_MANAGEMENT_SCOPE::WCS_PROFILE_MANAGEMENT_SCOPE_CURRENT_USER,
            NULL,
            COLORPROFILETYPE::CPT_ICC,
            CPST_NONE,

            )*/;
    GetStandardColorSpaceProfile(NULL, LCS_WINDOWS_COLOR_SPACE, buffer, &size);

    HCOLORSPACE hcs = GetColorSpace(GetDC(NULL));

    LOGCOLORSPACE lcs;

    GetLogColorSpace(hcs, &lcs, sizeof(lcs));

    wcout << buffer << endl;
}
