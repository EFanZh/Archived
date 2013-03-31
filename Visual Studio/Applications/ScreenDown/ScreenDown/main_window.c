#include "screen_down.h"

LPCTSTR main_window_class_name = TEXT("MainWindow");
BOOL MainWindow_OnCreate(HWND hWnd, LPCREATESTRUCT lpCreateStruct);
void MainWindow_OnDestroy(HWND hWnd);
void MainWindow_OnPaint(HWND hWnd);
DWORD WINAPI thread_proc(LPVOID lpParameter);

LRESULT CALLBACK main_window_proc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam);

int screen_cx, screen_cy;
HBITMAP hbm_screen;
HDC hdc_bitmap;

ATOM register_main_window_class(void)
{
  WNDCLASSEX wcex = { sizeof(wcex) };

  wcex.lpfnWndProc = main_window_proc;
  wcex.hInstance = GetModuleHandle(NULL);
  wcex.hIcon = (HICON)LoadImage(NULL, IDI_APPLICATION, IMAGE_ICON, 0, 0, LR_SHARED);
  wcex.hCursor = (HCURSOR)LoadImage(NULL, IDC_ARROW, IMAGE_CURSOR, 0, 0, LR_SHARED);
  wcex.lpszClassName = main_window_class_name;

  return RegisterClassEx(&wcex);
}

HWND create_main_window(void)
{
  screen_cx = GetSystemMetrics(SM_CXSCREEN);
  screen_cy = GetSystemMetrics(SM_CYSCREEN);
  return CreateWindowEx(WS_EX_TOPMOST, main_window_class_name, TEXT("ScreenDown"), WS_POPUP, 0, 0, screen_cx, screen_cy, HWND_DESKTOP, NULL, GetModuleHandle(NULL), NULL);
}

LRESULT CALLBACK main_window_proc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
{
  switch (uMsg)
  {
    HANDLE_MSG(hWnd, WM_CREATE, MainWindow_OnCreate);
    HANDLE_MSG(hWnd, WM_DESTROY, MainWindow_OnDestroy);
    HANDLE_MSG(hWnd, WM_PAINT, MainWindow_OnPaint);
  default:
    return DefWindowProc(hWnd, uMsg, wParam, lParam);
  }
}

BOOL MainWindow_OnCreate(HWND hWnd, LPCREATESTRUCT lpCreateStruct)
{
  HDC hdc_screen = GetDC(NULL);

  UNREFERENCED_PARAMETER(lpCreateStruct);

  hbm_screen = CreateCompatibleBitmap(hdc_screen, screen_cx, screen_cy);
  hdc_bitmap = CreateCompatibleDC(hdc_screen);
  SelectBitmap(hdc_bitmap, hbm_screen);
  BitBlt(hdc_bitmap, 0, 0, screen_cx, screen_cy, hdc_screen, 0, 0, SRCCOPY);

  ReleaseDC(NULL, hdc_screen);

  CreateThread(NULL, 0, thread_proc, hWnd, 0, NULL);

  return TRUE;
}

void MainWindow_OnDestroy(HWND hWnd)
{
  UNREFERENCED_PARAMETER(hWnd);

  DeleteDC(hdc_bitmap);
  DeleteBitmap(hbm_screen);
  PostQuitMessage(EXIT_SUCCESS);
}

void MainWindow_OnPaint(HWND hWnd)
{
  PAINTSTRUCT ps;

  BeginPaint(hWnd, &ps);

  EndPaint(hWnd, &ps);
}

DWORD WINAPI thread_proc(LPVOID lpParameter)
{
  HWND hWnd = (HWND)lpParameter;
  HDC hdc = GetDC(hWnd);

  ScreenDown(hdc, hdc_bitmap, screen_cx, screen_cy, 20, 20, 0.005, RGB(16, 32, 64));

  ReleaseDC(hWnd, hdc);
  PostMessage(hWnd, WM_CLOSE, 0, 0);
  return EXIT_SUCCESS;
}
