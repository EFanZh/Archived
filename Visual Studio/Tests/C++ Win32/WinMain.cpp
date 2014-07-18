#define WIN32_LEAN_AND_MEAN

#include <Windows.h>
#include <windowsx.h>
#include <CommCtrl.h>
#include <dwmapi.h>

#pragma comment(lib, "ComCtl32.Lib")
#pragma comment(lib, "dwmapi.lib")
#pragma comment(linker, "\"/manifestdependency:type='win32' name='Microsoft.Windows.Common-Controls' version='6.0.0.0' processorArchitecture='*' publicKeyToken='6595b64144ccf1df' language='*'\"")

LRESULT CALLBACK WindowProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam);
BOOL MainWindow_OnCreate(HWND hWnd, LPCREATESTRUCT lpCreateStruct);
void MainWindow_OnDestroy(HWND hWnd);
void MainWindow_OnCommand(HWND hWnd, int id, HWND hWndCtl, UINT codeNotify);
LRESULT MainWindow_OnNotify(HWND hWnd, int idFrom, NMHDR *pnmhdr);

#define ID_TREEVIEW 100
#define ID_EXPAND 101

HTREEITEM root_item;

int WINAPI wWinMain(_In_ HINSTANCE hInstance, _In_opt_ HINSTANCE hPrevInstance, _In_ LPWSTR lpCmdLine, _In_ int nCmdShow)
{
    WNDCLASSEX wcex = { sizeof(wcex) };

    wcex.lpfnWndProc = WindowProc;
    wcex.hInstance = hInstance;
    LoadIconMetric(NULL, IDI_APPLICATION, LIM_LARGE, &wcex.hIcon);
    wcex.hCursor = static_cast<HCURSOR>(LoadImage(NULL, IDC_ARROW, IMAGE_CURSOR, 0, 0, LR_SHARED));
    wcex.lpszClassName = TEXT("MainWindow");
    LoadIconMetric(NULL, IDI_APPLICATION, LIM_SMALL, &wcex.hIconSm);

    RegisterClassEx(&wcex);

    HWND hWnd = CreateWindowEx(0, wcex.lpszClassName, TEXT("TreeView Test"), WS_OVERLAPPEDWINDOW & ~WS_CAPTION, CW_USEDEFAULT, CW_USEDEFAULT, CW_USEDEFAULT, CW_USEDEFAULT, HWND_DESKTOP, NULL, hInstance, NULL);
    ShowWindow(hWnd, nCmdShow);

    MSG msg;
    while (GetMessage(&msg, NULL, 0, 0))
    {
        TranslateMessage(&msg);
        DispatchMessage(&msg);
    }

    return msg.wParam;
}

LRESULT CALLBACK WindowProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
{
    switch (uMsg)
    {
        HANDLE_MSG(hWnd, WM_CREATE, MainWindow_OnCreate);
        HANDLE_MSG(hWnd, WM_DESTROY, MainWindow_OnDestroy);
        HANDLE_MSG(hWnd, WM_NOTIFY, MainWindow_OnNotify);
        HANDLE_MSG(hWnd, WM_COMMAND, MainWindow_OnCommand);
    default:
        return DefWindowProc(hWnd, uMsg, wParam, lParam);
    }
}

BOOL MainWindow_OnCreate(HWND hWnd, LPCREATESTRUCT lpCreateStruct)
{
    HWND h_treeview = CreateWindowEx(WS_EX_CLIENTEDGE, WC_TREEVIEW, TEXT("TreeView"), WS_CHILD | WS_VISIBLE | TVS_HASBUTTONS | TVS_LINESATROOT, 10, 10, 400, 400, hWnd, reinterpret_cast<HMENU>(ID_TREEVIEW), GetModuleHandle(NULL), NULL);

    TVINSERTSTRUCT tvis = { TVI_ROOT, TVI_LAST, { TVIF_TEXT | TVIF_CHILDREN } };
    tvis.item.pszText = TEXT("Root");
    tvis.item.cChildren = 1;
    root_item = TreeView_InsertItem(h_treeview, &tvis);

    CreateWindow(WC_BUTTON, TEXT("Expand"), WS_CHILD | WS_VISIBLE, 420, 10, 75, 23, hWnd, reinterpret_cast<HMENU>(ID_EXPAND), GetModuleHandle(NULL), NULL);

    return TRUE;
}

void MainWindow_OnDestroy(HWND hWnd)
{
    PostQuitMessage(0);
}

LRESULT MainWindow_OnNotify(HWND hWnd, int idFrom, NMHDR *pnmhdr)
{
    switch (pnmhdr->code)
    {
    case TVN_ITEMEXPANDING:
    {
        LPNMTREEVIEW pnmtv = reinterpret_cast<LPNMTREEVIEW>(pnmhdr);
        if (pnmtv->action == TVE_EXPAND)
        {
            LPCTSTR items[] = { TEXT("Item 1"), TEXT("Item 2"), TEXT("Item 3") };
            for (LPCTSTR item : items)
            {
                TVINSERTSTRUCT tvis = { pnmtv->itemNew.hItem, TVI_LAST, { TVIF_TEXT } };
                tvis.item.pszText = const_cast<LPTSTR>(item);
                TreeView_InsertItem(pnmhdr->hwndFrom, &tvis);
            }
        }
        break;
    }
    case TVN_ITEMEXPANDED:
    {
        LPNMTREEVIEW pnmtv = reinterpret_cast<LPNMTREEVIEW>(pnmhdr);
        if (pnmtv->action == TVE_COLLAPSE)
        {
            PostMessage(pnmhdr->hwndFrom, TVM_EXPAND, TVE_COLLAPSE | TVE_COLLAPSERESET, reinterpret_cast<LPARAM>(pnmtv->itemNew.hItem));
        }
        break;
    }
    }
    return 0;
}

void MainWindow_OnCommand(HWND hWnd, int id, HWND hWndCtl, UINT codeNotify)
{
    if (id == ID_EXPAND)
    {
        TreeView_Expand(GetDlgItem(hWnd, ID_TREEVIEW), root_item, TVE_EXPAND);
    }
}
