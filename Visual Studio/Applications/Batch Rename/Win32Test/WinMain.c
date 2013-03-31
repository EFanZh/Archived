#define _CRT_SECURE_NO_DEPRECATE
#define WIN32_LEAN_AND_MEAN
#define STRICT

#include <tchar.h>
#include <Windows.h>
#include <windowsx.h>
#include <CommCtrl.h>
#include <Uxtheme.h>

#pragma comment(lib, "uxtheme.lib")
#pragma comment(linker, "\"/manifestdependency:type='win32' name='Microsoft.Windows.Common-Controls' version='6.0.0.0' processorArchitecture='*' publicKeyToken='6595b64144ccf1df' language='*'\"")

LRESULT CALLBACK WindowProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam);
BOOL MainWindow_OnCreate(HWND hWnd, LPCREATESTRUCT lpCreateStruct);
void MainWindow_OnDestroy(HWND hWnd);
void MainWindow_OnSize(HWND hWnd, UINT state, int cx, int cy);
void MainWindow_OnPaint(HWND hWnd);
LRESULT MainWindow_OnNotify(HWND hWnd, int idFrom, LPNMHDR pnmhdr);

#define ID_LISTVIEW 100

int APIENTRY _tWinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPTSTR lpCmdLine, int nCmdShow)
{
  WNDCLASSEX wcex = { 0 };
  HWND hWnd;
  BOOL ret;
  MSG msg;

  wcex.cbSize = sizeof(wcex);
  wcex.lpfnWndProc = WindowProc;
  wcex.hInstance = hInstance;
  wcex.hIcon = (HICON)LoadImage(NULL, IDI_APPLICATION, IMAGE_ICON, 0, 0, LR_SHARED);
  wcex.hCursor = (HCURSOR)LoadImage(NULL, IDC_ARROW, IMAGE_CURSOR, 0, 0, LR_SHARED);
  wcex.lpszClassName = TEXT("MainWindow");

  if (!RegisterClassEx(&wcex))
  {
    return 1;
  }

  hWnd = CreateWindow(wcex.lpszClassName, TEXT("Win32Test"), WS_OVERLAPPEDWINDOW | WS_CLIPCHILDREN, CW_USEDEFAULT, CW_USEDEFAULT, CW_USEDEFAULT, CW_USEDEFAULT, HWND_DESKTOP, NULL, hInstance, NULL);
  if (!hWnd)
  {
    return 1;
  }

  ShowWindow(hWnd, nCmdShow);
  UpdateWindow(hWnd);

  while ((ret = GetMessage(&msg, NULL, 0, 0)) != 0)
  {
    if (ret == -1)
    {
      return 1;
    }
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
    HANDLE_MSG(hWnd, WM_SIZE, MainWindow_OnSize);
    HANDLE_MSG(hWnd, WM_PAINT, MainWindow_OnPaint);
    HANDLE_MSG(hWnd, WM_NOTIFY, MainWindow_OnNotify);
  default:
    return DefWindowProc(hWnd, uMsg, wParam, lParam);
  }
}

BOOL MainWindow_OnCreate(HWND hWnd, LPCREATESTRUCT lpCreateStruct)
{
  HWND h_listview = CreateWindowEx(WS_EX_CLIENTEDGE, WC_LISTVIEW, NULL, WS_CHILD | WS_VISIBLE, CW_USEDEFAULT, CW_USEDEFAULT, CW_USEDEFAULT, CW_USEDEFAULT, hWnd, (HMENU)ID_LISTVIEW, GetModuleHandle(NULL), NULL);
  LVCOLUMN lvc = { 0 };

  // BufferedPaintInit();

  SetWindowTheme(h_listview, TEXT("Explorer"), NULL);
  ListView_SetExtendedListViewStyle(h_listview, LVS_EX_DOUBLEBUFFER);
  ListView_SetView(h_listview, LV_VIEW_DETAILS);
  lvc.mask = LVCF_TEXT;
  lvc.pszText = TEXT("Column 1");
  ListView_InsertColumn(h_listview, 0, &lvc);
  ListView_SetColumnWidth(h_listview, 0, LVSCW_AUTOSIZE_USEHEADER);

  return TRUE;
}

void MainWindow_OnDestroy(HWND hWnd)
{
  //BufferedPaintUnInit();
  PostQuitMessage(0);
}

void MainWindow_OnSize(HWND hWnd, UINT state, int cx, int cy)
{
  MoveWindow(GetDlgItem(hWnd, ID_LISTVIEW), 11, 11, cx - 22, cy - 22, TRUE);
}

void MainWindow_OnPaint(HWND hWnd)
{
  PAINTSTRUCT ps;
  //HPAINTBUFFER hpb;
  //HDC hdc;

  BeginPaint(hWnd, &ps);
  //hpb = BeginBufferedPaint(ps.hdc, &ps.rcPaint, BPBF_COMPATIBLEBITMAP, NULL, &hdc);

  FillRect(ps.hdc, &ps.rcPaint, GetSysColorBrush(COLOR_3DFACE));

  //EndBufferedPaint(hpb, TRUE);
  EndPaint(hWnd, &ps);
}

LRESULT MainWindow_OnNotify(HWND hWnd, int idFrom, LPNMHDR pnmhdr)
{
  switch (pnmhdr->code)
  {
  case LVN_GETEMPTYMARKUP:
    {
      if (pnmhdr->hwndFrom == GetDlgItem(hWnd, ID_LISTVIEW))
      {
        NMLVEMPTYMARKUP *pmarkup = (NMLVEMPTYMARKUP *)pnmhdr;
        _tcscpy(pmarkup->szMarkup, TEXT("This is an empty ListView."));
        return TRUE;
      }
      return FORWARD_WM_NOTIFY(hWnd, idFrom, pnmhdr, DefWindowProc);
    }
  default:
    return FORWARD_WM_NOTIFY(hWnd, idFrom, pnmhdr, DefWindowProc);
  }
}
