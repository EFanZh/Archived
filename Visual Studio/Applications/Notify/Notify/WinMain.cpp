#define STRICT
#define WIN32_LEAN_AND_MEAN

#include <algorithm>

#include <Windows.h>
#include <windowsx.h>
#include <CommCtrl.h>
#include <shellapi.h>

#pragma comment(lib, "Comctl32.lib")
#pragma comment(linker, "\"/manifestdependency:type='win32' name='Microsoft.Windows.Common-Controls' version='6.0.0.0' processorArchitecture='*' publicKeyToken='6595b64144ccf1df' language='*'\"")

LRESULT CALLBACK WindowProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam);
BOOL OnCreate(HWND hWnd, LPCREATESTRUCT lpCreateStruct);
void OnDestroy(HWND hWnd);
void OnCommand(HWND hWnd, int id, HWND hWndCtl, UINT codeNotify);
void OnTimer(HWND hWnd, UINT id);
LRESULT OnNotifyIcon(HWND hWnd, WPARAM wParam, LPARAM lParam);

#define WM_USER_NOTIFYICON (WM_USER + 1)

static const UINT notify_icon_id = 0u;
static const UINT exit_command_id = 0u;
static const UINT timer_id = 0u;
static HMENU hMenu;

int WINAPI wWinMain(_In_ HINSTANCE hInstance, _In_opt_ HINSTANCE hPrevInstance, _In_ LPWSTR lpCmdLine, _In_ int nCmdShow)
{
  UNREFERENCED_PARAMETER(hPrevInstance);
  UNREFERENCED_PARAMETER(lpCmdLine);
  UNREFERENCED_PARAMETER(nCmdShow);

  WNDCLASSEX wcex = { sizeof(wcex) };

  wcex.lpfnWndProc = WindowProc;
  wcex.hInstance = hInstance;
  wcex.lpszClassName = TEXT("Notify Message Window");

  RegisterClassEx(&wcex);

  CreateWindowEx(0, wcex.lpszClassName, TEXT("Notify"), WS_OVERLAPPEDWINDOW, CW_USEDEFAULT, CW_USEDEFAULT, CW_USEDEFAULT, CW_USEDEFAULT, HWND_MESSAGE, NULL, hInstance, NULL);

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
    HANDLE_MSG(hWnd, WM_CREATE, OnCreate);
    HANDLE_MSG(hWnd, WM_DESTROY, OnDestroy);
    HANDLE_MSG(hWnd, WM_COMMAND, OnCommand);
    HANDLE_MSG(hWnd, WM_TIMER, OnTimer);
  case WM_USER_NOTIFYICON:
    return OnNotifyIcon(hWnd, wParam, lParam);
  default:
    return DefWindowProc(hWnd, uMsg, wParam, lParam);
  }
}

BOOL OnCreate(HWND hWnd, LPCREATESTRUCT lpCreateStruct)
{
  UNREFERENCED_PARAMETER(lpCreateStruct);

  NOTIFYICONDATA nid = { sizeof(nid) };
  nid.hWnd = hWnd;
  nid.uID = notify_icon_id;
  nid.uFlags = NIF_ICON | NIF_MESSAGE | NIF_TIP | NIF_SHOWTIP;
  nid.uCallbackMessage = WM_USER_NOTIFYICON;
  LoadIconMetric(NULL, IDI_INFORMATION, LIM_SMALL, &nid.hIcon);
  TCHAR tooltip[] = TEXT("Notify");
  std::copy(std::begin(tooltip), std::end(tooltip), nid.szTip);
  nid.uVersion = NOTIFYICON_VERSION_4;

  Shell_NotifyIcon(NIM_ADD, &nid);
  Shell_NotifyIcon(NIM_SETVERSION, &nid);

  hMenu = CreatePopupMenu();
  AppendMenu(hMenu, MF_ENABLED, exit_command_id, TEXT("Exit"));

  SetTimer(hWnd, timer_id, 6000, NULL);

  return TRUE;
}

void OnDestroy(HWND hWnd)
{
  KillTimer(hWnd, timer_id);

  DestroyMenu(hMenu);

  NOTIFYICONDATA nid = { sizeof(nid) };
  nid.hWnd = hWnd;
  nid.uID = notify_icon_id;
  Shell_NotifyIcon(NIM_DELETE, &nid);

  PostQuitMessage(0);
}

void OnCommand(HWND hWnd, int id, HWND hWndCtl, UINT codeNotify)
{
  UNREFERENCED_PARAMETER(hWndCtl);
  UNREFERENCED_PARAMETER(codeNotify);

  if (id == exit_command_id)
  {
    SendMessage(hWnd, WM_CLOSE, 0, 0);
  }
}

void OnTimer(HWND hWnd, UINT id)
{
  if (id == timer_id)
  {
    static bool notified = false;

    SYSTEMTIME st;
    GetLocalTime(&st);

    if (st.wMinute < 50)
    {
      if (notified)
      {
        notified = false;
      }
    }
    else
    {
      if (!notified)
      {
        NOTIFYICONDATA nid = { sizeof(nid) };
        nid.hWnd = hWnd;
        nid.uID = notify_icon_id;
        nid.uFlags = NIF_INFO | NIF_SHOWTIP;
        nid.dwInfoFlags = NIIF_INFO | NIIF_RESPECT_QUIET_TIME;
        TCHAR info[] = TEXT("It’s time to take a break.");
        std::copy(std::begin(info), std::end(info), nid.szInfo);
        TCHAR info_title[] = TEXT("Notify");
        std::copy(std::begin(info_title), std::end(info_title), nid.szInfoTitle);

        Shell_NotifyIcon(NIM_MODIFY, &nid);

        notified = true;
      }
    }
  }
}

LRESULT OnNotifyIcon(HWND hWnd, WPARAM wParam, LPARAM lParam)
{
  if (LOWORD(lParam) == WM_CONTEXTMENU)
  {
    SetForegroundWindow(hWnd);
    TrackPopupMenu(hMenu, TPM_LEFTALIGN, GET_X_LPARAM(wParam), GET_Y_LPARAM(wParam), 0, hWnd, NULL);
  }
  return 0;
}
