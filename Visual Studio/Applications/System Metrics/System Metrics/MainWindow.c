#include "MainWindow.h"
#include "SystemMetrics.h"

LRESULT CALLBACK MainWindowProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam);
BOOL MainWindow_OnCreate(HWND hWnd, LPCREATESTRUCT lpCreateStruct);
void MainWindow_OnDestroy(HWND hWnd);
void MainWindow_OnSize(HWND hWnd, UINT state, int cx, int cy);
void MainWindow_OnPaint(HWND hWnd);
void MainWindow_OnKeyDown(HWND hWnd, UINT vk, BOOL fDown, int cRepeat, UINT flags);
void MainWindow_OnVScroll(HWND hWnd, HWND hWndCtl, UINT code, int pos);
void MainWindow_OnMouseWheel(HWND hWnd, int xPos, int yPos, int zDelta, UINT fwKeys);
void MainWindow_OnPrintClient(HWND hWnd, HDC hdc, UINT uFlags);
void PaintContent(HWND hWnd, HDC hdc, LPCRECT lpRect);

static LPCTSTR main_window_class_name = TEXT("MainWindow");
static double text_positions[] = { 0.0, 0.38, 1.0 };
static int text_positions_int[ARRAYSIZE(text_positions)];
static HFONT hf_text;
static int line_height;
static int wheel_scroll_lines;

HWND CreateMainWindow(void)
{
  return CreateWindow(main_window_class_name, TEXT("System Metrics"), WS_OVERLAPPEDWINDOW | WS_VSCROLL, CW_USEDEFAULT, CW_USEDEFAULT, CW_USEDEFAULT, CW_USEDEFAULT, HWND_DESKTOP, NULL, HINST_THISCOMPONENT, NULL);
}

ATOM RegisterMainWindowClass(void)
{
  WNDCLASSEX wcex = { sizeof(wcex) };

  wcex.lpfnWndProc = MainWindowProc;
  wcex.hInstance = HINST_THISCOMPONENT;
  LoadIconMetric(NULL, IDI_APPLICATION, LIM_LARGE, &wcex.hIcon);
  wcex.hCursor = (HCURSOR)LoadImage(NULL, IDC_ARROW, IMAGE_CURSOR, 0, 0, LR_SHARED);
  wcex.lpszClassName = main_window_class_name;
  LoadIconMetric(NULL, IDI_APPLICATION, LIM_SMALL, &wcex.hIconSm);

  return RegisterClassEx(&wcex);
}

LRESULT CALLBACK MainWindowProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
{
  switch (uMsg)
  {
    HANDLE_MSG(hWnd, WM_CREATE, MainWindow_OnCreate);
    HANDLE_MSG(hWnd, WM_DESTROY, MainWindow_OnDestroy);
    HANDLE_MSG(hWnd, WM_SIZE, MainWindow_OnSize);
    HANDLE_MSG(hWnd, WM_PAINT, MainWindow_OnPaint);
    HANDLE_MSG(hWnd, WM_KEYDOWN, MainWindow_OnKeyDown);
    HANDLE_MSG(hWnd, WM_VSCROLL, MainWindow_OnVScroll);
    HANDLE_MSG(hWnd, WM_MOUSEWHEEL, MainWindow_OnMouseWheel);
    HANDLE_MSG(hWnd, WM_PRINTCLIENT, MainWindow_OnPrintClient);
  default:
    return DefWindowProc(hWnd, uMsg, wParam, lParam);
  }
}

BOOL MainWindow_OnCreate(HWND hWnd, LPCREATESTRUCT lpCreateStruct)
{
  LOGFONT lf = { 0 };
  HDC hdc;
  TEXTMETRIC tm;
  int i;
  SCROLLINFO si;

  UNREFERENCED_PARAMETER(lpCreateStruct);

  lf.lfHeight = -12;
  _tcscpy_s(lf.lfFaceName, ARRAYSIZE(lf.lfFaceName), TEXT("Consolas"));
  hf_text = CreateFontIndirect(&lf);

  hdc = GetDC(hWnd);
  SelectFont(hdc, hf_text);
  GetTextMetrics(hdc, &tm);
  ReleaseDC(hWnd, hdc);

  // Text info
  line_height = tm.tmHeight + tm.tmExternalLeading;

  // Metrics info
  for (i = 0; i < ARRAYSIZE(system_metrics); i++)
  {
    system_metrics[i].value = GetSystemMetrics(system_metrics[i].index);
  }

  // Scroll Bar
  si.cbSize = sizeof(si);
  si.fMask = SIF_RANGE;
  si.nMin = 0;
  si.nMax = ARRAYSIZE(system_metrics) - 1;
  SetScrollInfo(hWnd, SB_VERT, &si, TRUE);

  // Wheel scroll lines
  SystemParametersInfo(SPI_GETWHEELSCROLLLINES, 0, &wheel_scroll_lines, 0);

  BufferedPaintInit();

  return TRUE;
}

void MainWindow_OnDestroy(HWND hWnd)
{
  UNREFERENCED_PARAMETER(hWnd);

  BufferedPaintUnInit();
  DeleteFont(hf_text);

  PostQuitMessage(0);
}

void MainWindow_OnSize(HWND hWnd, UINT state, int cx, int cy)
{
  int i;
  SCROLLINFO si = { sizeof(si) };

  UNREFERENCED_PARAMETER(state);
  UNREFERENCED_PARAMETER(cx);

  for (i = 0; i < ARRAYSIZE(text_positions); i++)
  {
    text_positions_int[i] = (int)(cx * text_positions[i]);
  }

  si.fMask = SIF_PAGE;
  si.nPage = cy / line_height;
  SetScrollInfo(hWnd, SB_VERT, &si, TRUE);

  InvalidateRect(hWnd, NULL, FALSE);
}

void MainWindow_OnPaint(HWND hWnd)
{
  PAINTSTRUCT ps;
  HPAINTBUFFER hpb;
  HDC hdc;

  BeginPaint(hWnd, &ps);
  hpb = BeginBufferedPaint(ps.hdc, &ps.rcPaint, BPBF_COMPOSITED, NULL, &hdc);

  PaintContent(hWnd, hdc, &ps.rcPaint);

  EndBufferedPaint(hpb, TRUE);
  EndPaint(hWnd, &ps);
}

void MainWindow_OnKeyDown(HWND hWnd, UINT vk, BOOL fDown, int cRepeat, UINT flags)
{
  UNREFERENCED_PARAMETER(fDown);
  UNREFERENCED_PARAMETER(cRepeat);
  UNREFERENCED_PARAMETER(flags);

  switch (vk)
  {
  case VK_PRIOR:
    SendMessage(hWnd, WM_VSCROLL, SB_PAGEUP, (LPARAM)NULL);
    break;
  case VK_NEXT:
    SendMessage(hWnd, WM_VSCROLL, SB_PAGEDOWN, (LPARAM)NULL);
    break;
  case VK_END:
    SendMessage(hWnd, WM_VSCROLL, SB_BOTTOM, (LPARAM)NULL);
    break;
  case VK_HOME:
    SendMessage(hWnd, WM_VSCROLL, SB_TOP, (LPARAM)NULL);
    break;
  case VK_UP:
    SendMessage(hWnd, WM_VSCROLL, SB_LINEUP, (LPARAM)NULL);
    break;
  case VK_DOWN:
    SendMessage(hWnd, WM_VSCROLL, SB_LINEDOWN, (LPARAM)NULL);
    break;
  }
}

void MainWindow_OnVScroll(HWND hWnd, HWND hWndCtl, UINT code, int pos)
{
  SCROLLINFO si = { sizeof(si) };
  int pos_saved;

  UNREFERENCED_PARAMETER(hWndCtl);

  si.fMask = SIF_RANGE | SIF_PAGE | SIF_POS;
  GetScrollInfo(hWnd, SB_VERT, &si);
  pos_saved = si.nPos;
  switch (code)
  {
  case SB_LINEUP:
    si.nPos--;
    break;
  case SB_LINEDOWN:
    si.nPos++;
    break;
  case SB_PAGEUP:
    si.nPos -= si.nPage;
    break;
  case SB_PAGEDOWN:
    si.nPos += si.nPage;
    break;
  case SB_THUMBPOSITION:
    // Fall through.
  case SB_THUMBTRACK:
    si.nPos = pos;
    break;
  case SB_TOP:
    si.nPos = si.nMin;
    break;
  case SB_BOTTOM:
    si.nPos = si.nMax;
    break;
  }
  si.fMask = SIF_POS;
  SetScrollInfo(hWnd, SB_VERT, &si, TRUE);
  GetScrollInfo(hWnd, SB_VERT, &si);
  if (si.nPos != pos_saved)
  {
    ScrollWindowEx(hWnd, 0, line_height * (pos_saved - si.nPos), NULL, NULL, NULL, NULL, (abs(pos_saved - si.nPos) * 4 << 16) | SW_SMOOTHSCROLL);
    UpdateWindow(hWnd);
  }
}

void MainWindow_OnMouseWheel(HWND hWnd, int xPos, int yPos, int zDelta, UINT fwKeys)
{
  SCROLLINFO si = { sizeof(si) };

  UNREFERENCED_PARAMETER(xPos);
  UNREFERENCED_PARAMETER(yPos);
  UNREFERENCED_PARAMETER(fwKeys);

  si.fMask = SIF_POS;
  GetScrollInfo(hWnd, SB_VERT, &si);
  SendMessage(hWnd, WM_VSCROLL, MAKEWPARAM(SB_THUMBPOSITION, si.nPos - zDelta * wheel_scroll_lines / WHEEL_DELTA), (LPARAM)NULL);
}

void MainWindow_OnPrintClient(HWND hWnd, HDC hdc, UINT uFlags)
{
  RECT rect;

  GetClientRect(hWnd, &rect);
  PaintContent(hWnd, hdc, &rect);
}

void PaintContent(HWND hWnd, HDC hdc, LPCRECT lpRect)
{
  SCROLLINFO si = { sizeof(si) };
  int last_line, i, y;

  FillRect(hdc, lpRect, GetStockBrush(WHITE_BRUSH));
  si.fMask = SIF_POS;
  GetScrollInfo(hWnd, SB_VERT, &si);

  SelectFont(hdc, hf_text);
  last_line = min(ARRAYSIZE(system_metrics) - 1, si.nPos + lpRect->bottom / line_height);
  for (i = si.nPos, y = 0; i <= last_line; y += line_height, i++)
  {
    TCHAR buffer[10];
    TextOut(hdc, text_positions_int[0], y, system_metrics[i].label, _tcslen(system_metrics[i].label));
    SetTextAlign(hdc, TA_TOP | TA_RIGHT);
    TextOut(hdc, text_positions_int[1], y, buffer, wsprintf(buffer, TEXT("%d"), system_metrics[i].index));
    TextOut(hdc, text_positions_int[2], y, buffer, wsprintf(buffer, TEXT("%d"), system_metrics[i].value));
    SetTextAlign(hdc, TA_TOP | TA_LEFT);
  }
}
