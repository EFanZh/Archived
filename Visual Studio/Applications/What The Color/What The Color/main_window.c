#include "main_window.h"
#include "context_menu.h"

#define ID_TIMER 1

static LPCTSTR main_window_class_name = TEXT("MainWindow");
static HMENU hmenu;
static HDC hdc_screen;
static HFONT hfont_message, hfont_fixed;
static COLORREF color;
static int color_r, color_g, color_b;
static int hold = 0;

LRESULT CALLBACK MainWindowProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam);
BOOL MainWindow_OnCreate(HWND hWnd, LPCREATESTRUCT lpCreateStruct);
void MainWindow_OnDestroy(HWND hWnd);
void MainWindow_OnPaint(HWND hWnd);
void MainWindow_OnContextMenu(HWND hWnd, HWND hWndContext, UINT xPos, UINT yPos);
void MainWindow_OnKeyDown(HWND hWnd, UINT vk, BOOL fDown, int cRepeat, UINT flags);
void MainWindow_OnCommand(HWND hWnd, int id, HWND hWndCtl, UINT codeNotify);
void MainWindow_OnTimer(HWND hWnd, UINT id);

ATOM RegisterMainWindowClass()
{
    WNDCLASSEX wcex = { sizeof(wcex) };

    wcex.lpfnWndProc = MainWindowProc;
    wcex.hInstance = GetModuleHandle(NULL);
    LoadIconMetric(NULL, IDI_APPLICATION, LIM_LARGE, &wcex.hIcon);
    wcex.hCursor = (HCURSOR)LoadImage(NULL, IDC_ARROW, IMAGE_CURSOR, 0, 0, LR_SHARED);
    wcex.lpszClassName = main_window_class_name;
    LoadIconMetric(NULL, IDI_APPLICATION, LIM_SMALL, &wcex.hIconSm);

    return RegisterClassEx(&wcex);
}

HWND CreateMainWindow()
{
    return CreateWindowEx(WS_EX_TOPMOST, main_window_class_name, TEXT("What The Color"), WS_CAPTION | WS_SYSMENU, CW_USEDEFAULT, CW_USEDEFAULT, 240, 120, HWND_DESKTOP, NULL, GetModuleHandle(NULL), NULL);
}

LRESULT CALLBACK MainWindowProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
{
    switch (uMsg)
    {
        HANDLE_MSG(hWnd, WM_CREATE, MainWindow_OnCreate);
        HANDLE_MSG(hWnd, WM_DESTROY, MainWindow_OnDestroy);
        HANDLE_MSG(hWnd, WM_PAINT, MainWindow_OnPaint);
        HANDLE_MSG(hWnd, WM_CONTEXTMENU, MainWindow_OnContextMenu);
        HANDLE_MSG(hWnd, WM_KEYDOWN, MainWindow_OnKeyDown);
        HANDLE_MSG(hWnd, WM_COMMAND, MainWindow_OnCommand);
        HANDLE_MSG(hWnd, WM_TIMER, MainWindow_OnTimer);
    default:
        return DefWindowProc(hWnd, uMsg, wParam, lParam);
    }
}

BOOL MainWindow_OnCreate(HWND hWnd, LPCREATESTRUCT lpCreateStruct)
{
    LOGFONT lf = { 0 };
    NONCLIENTMETRICS ncm = { sizeof(ncm) };

    UNREFERENCED_PARAMETER(lpCreateStruct);

    // Create context menu
    hmenu = CreateContextMenu();

    // Set Timer
    SetTimer(hWnd, ID_TIMER, USER_TIMER_MINIMUM, NULL);

    // Get fonts
    SystemParametersInfo(SPI_GETNONCLIENTMETRICS, ncm.cbSize, &ncm, 0);
    hfont_message = CreateFontIndirect(&ncm.lfMessageFont);
    _tcscpy_s(lf.lfFaceName, ARRAYSIZE(lf.lfFaceName), TEXT("Consolas"));
    hfont_fixed = CreateFontIndirect(&lf);

    // Get screen DC
    hdc_screen = CreateDC(TEXT("DISPLAY"), NULL, NULL, NULL);

    // Initialize buffered paint functions
    BufferedPaintInit();

    return TRUE;
}

void MainWindow_OnDestroy(HWND hWnd)
{
    UNREFERENCED_PARAMETER(hWnd);

    BufferedPaintUnInit();
    DeleteDC(hdc_screen);
    DeleteFont(hfont_fixed);
    DeleteFont(hfont_message);
    KillTimer(hWnd, ID_TIMER);
    DestroyMenu(hmenu);

    PostQuitMessage(0);
}

void MainWindow_OnPaint(HWND hWnd)
{
    PAINTSTRUCT ps;
    HPAINTBUFFER hpb;
    HDC hdc;

    BeginPaint(hWnd, &ps);
    hpb = BeginBufferedPaint(ps.hdc, &ps.rcPaint, BPBF_COMPATIBLEBITMAP, NULL, &hdc);

    {
        HBRUSH hbr = CreateSolidBrush(color);
        LPCTSTR info;
        TCHAR buffer[32];
        RECT rc_client;

        FillRect(hdc, &ps.rcPaint, hbr);
        DeleteBrush(hbr);

        SetTextColor(hdc, color_r + color_g + color_b < 383 ? RGB(255, 255, 255) : RGB(0, 0, 0));
        SetBkMode(hdc, TRANSPARENT);

        SelectFont(hdc, hfont_message);
        if (hold)
        {
            info = TEXT("Holding...");
        }
        else
        {
            info = TEXT("Press “H” key to hold.");
        }
        TextOut(hdc, 11, 11, info, _tcslen(info));

        SelectFont(hdc, hfont_fixed);
        StringCchPrintf(buffer, ARRAYSIZE(buffer), TEXT("#%02x%02x%02x (%d, %d, %d)"), color_r, color_g, color_b, color_r, color_g, color_b);
        GetClientRect(hWnd, &rc_client);
        DrawText(hdc, buffer, -1, &rc_client, DT_SINGLELINE | DT_CENTER | DT_VCENTER);
    }

    EndBufferedPaint(hpb, TRUE);
    EndPaint(hWnd, &ps);
}

void MainWindow_OnContextMenu(HWND hWnd, HWND hWndContext, UINT xPos, UINT yPos)
{
    UNREFERENCED_PARAMETER(hWndContext);

    if (xPos == -1)
    {
        RECT rc_client;
        POINT point;

        GetClientRect(hWnd, &rc_client);
        point.x = rc_client.right / 2;
        point.y = rc_client.bottom / 2;
        ClientToScreen(hWnd, &point);
        xPos = point.x;
        yPos = point.y;
    }
    TrackPopupMenu(hmenu, 0, xPos, yPos, 0, hWnd, NULL);
}

void MainWindow_OnKeyDown(HWND hWnd, UINT vk, BOOL fDown, int cRepeat, UINT flags)
{
    UNREFERENCED_PARAMETER(fDown);
    UNREFERENCED_PARAMETER(cRepeat);
    UNREFERENCED_PARAMETER(flags);

    switch (vk)
    {
    case 0x48: // H key
        hold = !hold;
        if (hold)
        {
            KillTimer(hWnd, ID_TIMER);
        }
        else
        {
            SetTimer(hWnd, ID_TIMER, USER_TIMER_MINIMUM, NULL);
        }
        break;
    default:
        break;
    }
}

void MainWindow_OnCommand(HWND hWnd, int id, HWND hWndCtl, UINT codeNotify)
{
    TCHAR buffer[32];
    HGLOBAL hglobal;
    int size;
    LPVOID target_mem;

    UNREFERENCED_PARAMETER(hWndCtl);
    UNREFERENCED_PARAMETER(codeNotify);

    switch (id)
    {
    case ID_COPY_HEX:
    case ID_COPY_DEC:
        if (id == ID_COPY_DEC)
        {
            StringCchPrintf(buffer, ARRAYSIZE(buffer), TEXT("%d, %d, %d"), color_r, color_g, color_b);
        }
        else if (id == ID_COPY_HEX)
        {
            StringCchPrintf(buffer, ARRAYSIZE(buffer), TEXT("#%02x%02x%02x"), color_r, color_g, color_b);
        }
        size = _tcslen(buffer) + 1;
        hglobal = GlobalAlloc(GMEM_MOVEABLE, size * sizeof(TCHAR));

        target_mem = GlobalLock(hglobal);
        memcpy(target_mem, buffer, size * sizeof(TCHAR));
        GlobalUnlock(hglobal);

        OpenClipboard(hWnd);
        SetClipboardData(CF_UNICODETEXT, hglobal);
        CloseClipboard();
        break;
    default:
        break;
    }
}

void MainWindow_OnTimer(HWND hWnd, UINT id)
{
    POINT point;

    switch (id)
    {
    case ID_TIMER:
        GetCursorPos(&point);
        color = GetPixel(hdc_screen, point.x, point.y);
        color_r = GetRValue(color);
        color_g = GetGValue(color);
        color_b = GetBValue(color);
        InvalidateRect(hWnd, NULL, TRUE);
        break;
    }
}
