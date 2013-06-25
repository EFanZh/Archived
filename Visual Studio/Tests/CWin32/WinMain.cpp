static LPCTSTR main_window_class_name = TEXT("MainWindow");

ATOM RegisterMainWindowClass();
LRESULT CALLBACK MainWindowProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam);

int CALLBACK wWinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPWSTR lpCmdLine, int nCmdShow)
{
  RegisterMainWindowClass();
  ShowWindow(CreateWindowEx(0, main_window_class_name, TEXT("CWin32"), WS_OVERLAPPEDWINDOW, CW_USEDEFAULT, CW_USEDEFAULT, CW_USEDEFAULT, CW_USEDEFAULT, HWND_DESKTOP, NULL, HINST_THISCOMPONENT, NULL), nCmdShow);

  MSG msg;

  while (GetMessage(&msg, NULL, 0, 0) != 0)
  {
    TranslateMessage(&msg);
    DispatchMessage(&msg);
  }

  return msg.wParam;
}

ATOM RegisterMainWindowClass()
{
  WNDCLASSEX wcex = { sizeof(wcex) };

  wcex.lpfnWndProc = MainWindowProc;
  wcex.hInstance = HINST_THISCOMPONENT;
  wcex.hIcon = static_cast<HICON>(LoadImage(NULL, IDI_APPLICATION, IMAGE_ICON, 0, 0, LR_SHARED));
  wcex.hCursor = static_cast<HCURSOR>(LoadImage(NULL, IDC_ARROW, IMAGE_CURSOR, 0, 0, LR_SHARED));
  wcex.hbrBackground = GetSysColorBrush(COLOR_WINDOW);
  wcex.lpszClassName = main_window_class_name;
  wcex.hIconSm = wcex.hIcon;

  return RegisterClassEx(&wcex);
}

LRESULT CALLBACK MainWindowProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
{
  switch (uMsg)
  {
  case WM_DESTROY:
    PostQuitMessage(0);
    return 0;
  default:
    return DefWindowProc(hWnd, uMsg, wParam, lParam);
  }
}
