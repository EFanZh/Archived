#include "Layout.h"
#include "UIHelper.h"

BOOL GetAppropriateSize(HWND hWnd, LPSIZE pSize)
{
  HDC hdc = GetDC(hWnd);
  TCHAR buffer[256];
  BOOL result;

  SelectFont(hdc, GetWindowFont(hWnd));
  result = GetTextExtentPoint32(hdc, buffer, GetWindowText(hWnd, buffer, ARRAYSIZE(buffer)), pSize);

  ReleaseDC(hWnd, hdc);
  return result;
}

void MoveToWorkAreaCenter(HWND hWnd)
{
  RECT rect, rect_work_area;

  GetWindowRect(hWnd, &rect);
  SystemParametersInfo(SPI_GETWORKAREA, 0, &rect_work_area, 0);

  SetWindowPos(hWnd, NULL, rect_work_area.left + (rect_work_area.right - rect_work_area.left - rect.right + rect.left) / 2, rect_work_area.top + (rect_work_area.bottom - rect_work_area.top - rect.bottom + rect.top) / 2, 0, 0, SWP_NOSIZE | SWP_NOZORDER);
}
