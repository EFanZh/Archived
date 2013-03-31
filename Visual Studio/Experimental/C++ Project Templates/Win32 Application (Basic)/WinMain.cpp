#define STRICT
#define WIN32_LEAN_AND_MEAN

#include <Windows.h>

EXTERN_C IMAGE_DOS_HEADER __ImageBase;
#define HINST_THISCOMPONENT reinterpret_cast<HINSTANCE>(&__ImageBase)

LRESULT CALLBACK WindowProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam);

static LPCTSTR main_window_class_name = TEXT("MainWindow");

int WINAPI wWinMain(_In_ HINSTANCE hInstance, _In_opt_ HINSTANCE hPrevInstance, _In_ LPWSTR lpCmdLine, _In_ int nCmdShow)
{
  UNREFERENCED_PARAMETER(hPrevInstance);
  UNREFERENCED_PARAMETER(lpCmdLine);

  WNDCLASSEX wcex = { sizeof(wcex) };

  wcex.lpfnWndProc = WindowProc;
  wcex.hInstance = hInstance;
  wcex.hIcon = reinterpret_cast<HICON>(LoadImage(NULL, IDI_APPLICATION, IMAGE_ICON, 0, 0, LR_SHARED));
  wcex.hCursor = reinterpret_cast<HCURSOR>(LoadImage(NULL, IDC_ARROW, IMAGE_CURSOR, 0, 0, LR_SHARED));
  wcex.lpszClassName = main_window_class_name;
  wcex.hIconSm = wcex.hIcon;

  ATOM atom = RegisterClassEx(&wcex);
  if (atom == NULL)
  {
    return 1;
  }

  HWND hWnd = CreateWindowEx(0, main_window_class_name, TEXT("Win32 Application"), WS_OVERLAPPEDWINDOW, CW_USEDEFAULT, CW_USEDEFAULT, CW_USEDEFAULT, CW_USEDEFAULT, HWND_DESKTOP, NULL, hInstance, NULL);
  if (hWnd == NULL)
  {
    return 1;
  }

  ShowWindow(hWnd, nCmdShow);
  UpdateWindow(hWnd);

  BOOL ret;
  MSG msg;
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
  case WM_DESTROY:
    PostQuitMessage(0);
    break;
  default:
    return DefWindowProc(hWnd, uMsg, wParam, lParam);
  }
  return 0;
}
