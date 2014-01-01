#include "main_window.h"
#include "utilities.h"

#define ID_TIMER 1
#define TIME_SPAN 1

static LPCTSTR main_window_class_name = TEXT("MainWindow");
static const double grav_acc = 100000, ball_width = 1, ball_height = 1;
static const int time_span_millisecond = TIME_SPAN;
static const double time_span_second = TIME_SPAN / 1000.0;
static const COLORREF ball_color = RGB(0, 192, 255);

static int client_width, client_height;
static double ball_x = 0.0, ball_y = 50.0, ball_vx = 10000, ball_vy = 0.0;

LRESULT CALLBACK MainWindowProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam);
BOOL MainWindow_OnCreate(HWND hWnd, LPCREATESTRUCT lpCreateStruct);
void MainWindow_OnDestroy(HWND hWnd);
void MainWindow_OnSize(HWND hWnd, UINT state, int cx, int cy);
void MainWindow_OnPaint(HWND hWnd);
void MainWindow_OnTimer(HWND hWnd, UINT id);

ATOM RegisterMainWindowClass(HINSTANCE hInstance)
{
  WNDCLASSEX wcex;

  wcex.cbSize = sizeof(wcex);
  wcex.style = 0;
  wcex.lpfnWndProc = MainWindowProc;
  wcex.cbClsExtra = 0;
  wcex.cbWndExtra = 0;
  wcex.hInstance = hInstance;
  LoadIconMetric(NULL, IDI_APPLICATION, LIM_LARGE, &wcex.hIcon);
  wcex.hCursor = (HCURSOR)LoadImage(NULL, IDC_ARROW, IMAGE_CURSOR, 0, 0, LR_SHARED);
  wcex.hbrBackground = GetStockBrush(BLACK_BRUSH);
  wcex.lpszMenuName = NULL;
  wcex.lpszClassName = main_window_class_name;
  LoadIconMetric(NULL, IDI_APPLICATION, LIM_SMALL, &wcex.hIconSm);

  return RegisterClassEx(&wcex);
}

HWND CreateMainWindow(HINSTANCE hInstance)
{
  return CreateWindow(main_window_class_name, TEXT("BouncingBall"), WS_OVERLAPPEDWINDOW, CW_USEDEFAULT, CW_USEDEFAULT, CW_USEDEFAULT, CW_USEDEFAULT, HWND_DESKTOP, NULL, hInstance, NULL);
}

LRESULT CALLBACK MainWindowProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
{
  switch (uMsg)
  {
    HANDLE_MSG(hWnd, WM_CREATE, MainWindow_OnCreate);
    HANDLE_MSG(hWnd, WM_DESTROY, MainWindow_OnDestroy);
    HANDLE_MSG(hWnd, WM_SIZE, MainWindow_OnSize);
    HANDLE_MSG(hWnd, WM_PAINT, MainWindow_OnPaint);
    HANDLE_MSG(hWnd, WM_TIMER, MainWindow_OnTimer);
  default:
    return DefWindowProc(hWnd, uMsg, wParam, lParam);
  }
}

BOOL MainWindow_OnCreate(HWND hWnd, LPCREATESTRUCT lpCreateStruct)
{
  UNREFERENCED_PARAMETER(lpCreateStruct);

  SetTimer(hWnd, ID_TIMER, time_span_millisecond, NULL);

  return TRUE;
}

void MainWindow_OnDestroy(HWND hWnd)
{
  UNREFERENCED_PARAMETER(hWnd);

  KillTimer(hWnd, ID_TIMER);

  PostQuitMessage(0);
}

void MainWindow_OnSize(HWND hWnd, UINT state, int cx, int cy)
{
  UNREFERENCED_PARAMETER(hWnd);
  UNREFERENCED_PARAMETER(state);

  client_width = cx;
  client_height = cy;
}

void MainWindow_OnPaint(HWND hWnd)
{
  static PAINTSTRUCT ps;
  static RECT rect;
  static TCHAR buffer[100];
  static double v;

  rect.left = RoundToInt(ball_x);
  rect.top = RoundToInt(ball_y);
  rect.right = RoundToInt(ball_x + ball_width);
  rect.bottom = RoundToInt(ball_y + ball_height);
  BeginPaint(hWnd, &ps);
  SelectObject(ps.hdc, GetStockBrush(HOLLOW_BRUSH));
  SelectPen(ps.hdc, GetStockPen(WHITE_PEN));
  v = sqrt(Square(ball_vx) + Square(ball_vy));
  MoveToEx(ps.hdc, RoundToInt(ball_x + ball_width / 2.0), RoundToInt(ball_y + ball_height / 2.0), NULL);
  LineTo(ps.hdc, RoundToInt(ball_x + ball_width / 2.0 - ball_vx), RoundToInt(ball_y + ball_height / 2.0 - ball_vy));
  SetBkMode(ps.hdc, OPAQUE);
  Ellipse(ps.hdc, rect.left, rect.top, rect.right, rect.bottom);
  _stprintf_s(buffer, 100, TEXT("X = %lf, Y = %lf, Vx = %lf, Vy = %lf, V = %lf"), ball_x, ball_y, ball_vx, ball_vy, v);
  // TextOut(ps.hdc, RoundInt(ball_x + ball_width + 4), RoundInt(ball_y + ball_height + 4), buffer, _tcsclen(buffer));
  EndPaint(hWnd, &ps);
}

void MainWindow_OnTimer(HWND hWnd, UINT id)
{
  UNREFERENCED_PARAMETER(hWnd);

  switch (id)
  {
  case ID_TIMER:
    if (client_width > ball_width && client_height > ball_height)
    {
      // Ball X motion
      ball_x += ball_vx * time_span_second;
      while (ball_x < 0 || ball_x + ball_width > client_width)
      {
        if (ball_x < 0)
        {
          ball_x = -ball_x;
          ball_vx = -ball_vx;
        }
        else if (ball_x + ball_width > client_width)
        {
          ball_x = 2 * (client_width - ball_width) - ball_x;
          ball_vx = -ball_vx;
        }
      }

      // Ball Y motion
      while
        (
        (
        ball_vy < 0
        &&
        Square(ball_vy) > 2 * grav_acc * ball_y
        &&
        (-sqrt(Square(ball_vy) - 2 * grav_acc * ball_y) - ball_vy) / grav_acc < time_span_second
        )
        ||
        (
        ball_vy > 0
        &&
        (sqrt(Square(ball_vy) - 2 * grav_acc * (ball_y - client_height + ball_height)) - ball_vy) / grav_acc < time_span_second
        )
        )
      {
        if (ball_vy < 0)
        {
          double vy_top = sqrt(Square(ball_vy) - 2 * grav_acc * ball_y);
          ball_y = 2 * ball_vy * (ball_vy + vy_top) / grav_acc - 3 * ball_y;
          ball_vy += 2 * vy_top;
        }
        else if (ball_vy > 0)
        {
          double vy_bottom = sqrt(Square(ball_vy) + 2 * grav_acc * (client_height - ball_height - ball_y));
          ball_y = 2 * ball_vy * (ball_vy - vy_bottom) / grav_acc + 4 * (client_height - ball_height) - 3 * ball_y;
          ball_vy -= 2 * vy_bottom;
        }
      }
      ball_y += time_span_second * (0.5 * grav_acc * time_span_second + ball_vy);
      ball_vy += grav_acc * time_span_second;
    }
    InvalidateRect(hWnd, NULL, FALSE);
    break;
  }
}
