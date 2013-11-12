#define STRICT
#define WIN32_LEAN_AND_MEAN

#include <Windows.h>
#include <windowsx.h>
#include <CommCtrl.h>

LRESULT CALLBACK WindowProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam);
BOOL OnCreate(HWND hWnd, LPCREATESTRUCT lpCreateStruct);
void OnDestroy(HWND hWnd);

int WINAPI wWinMain(_In_ HINSTANCE hInstance, _In_opt_ HINSTANCE hPrevInstance, _In_ LPWSTR lpCmdLine, _In_ int nCmdShow)
{
  UNREFERENCED_PARAMETER(hPrevInstance);
  UNREFERENCED_PARAMETER(lpCmdLine);

  WNDCLASSEX wcex = { sizeof(wcex) };

  wcex.lpfnWndProc = WindowProc;
  wcex.hInstance = hInstance;
  wcex.hIcon = static_cast<HICON>(LoadImage(NULL, IDI_APPLICATION, IMAGE_ICON, 0, 0, LR_SHARED));
  wcex.hCursor = static_cast<HCURSOR>(LoadImage(NULL, IDC_ARROW, IMAGE_CURSOR, 0, 0, LR_SHARED));
  wcex.lpszClassName = TEXT("MainWindow");
  wcex.hIconSm = wcex.hIcon;

  RegisterClassEx(&wcex);

  HWND hWnd = CreateWindowEx(0, wcex.lpszClassName, TEXT("Win32 Application"), WS_OVERLAPPEDWINDOW, CW_USEDEFAULT, CW_USEDEFAULT, CW_USEDEFAULT, CW_USEDEFAULT, HWND_DESKTOP, NULL, hInstance, NULL);
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
    HANDLE_MSG(hWnd, WM_CREATE, OnCreate);
    HANDLE_MSG(hWnd, WM_DESTROY, OnDestroy);
  default:
    return DefWindowProc(hWnd, uMsg, wParam, lParam);
  }
}

BOOL OnCreate(HWND hWnd, LPCREATESTRUCT lpCreateStruct)
{
  return TRUE;
}

void OnDestroy(HWND hWnd)
{
  PostQuitMessage(0);
}
