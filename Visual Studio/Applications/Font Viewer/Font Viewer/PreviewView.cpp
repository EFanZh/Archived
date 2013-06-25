#include "PreviewView.h"

LRESULT PreviewView::WindowProc(UINT uMsg, WPARAM wParam, LPARAM lParam)
{
  switch (uMsg)
  {
  case WM_PAINT:
    OnPaint();

    return 0;

  case WM_SETTEXT:
    {
      LRESULT result = this->DefaultWindowProc(uMsg, wParam, lParam);
      this->InvalidateRect(NULL, TRUE);

      return result;
    }
  default:
    return this->DefaultWindowProc(uMsg, wParam, lParam);
  }
}

void PreviewView::OnPaint()
{
  PAINTSTRUCT ps;

  BeginPaint(*this, &ps);

  HDC hdc;
  HPAINTBUFFER hpb = BeginBufferedPaint(ps.hdc, NULL, BPBF_COMPOSITED, NULL, &hdc);

  RECT rect;
  GetClientRect(*this, &rect);
  FillRect(hdc, &rect, reinterpret_cast<HBRUSH>(GetClassLongPtr(*this, GCLP_HBRBACKGROUND)));

  int font_name_length = GetWindowTextLength(*this);
  std::unique_ptr<TCHAR []> font_name(new TCHAR[font_name_length + 1]);

  GetWindowText(*this, font_name.get(), font_name_length + 1);

  HFONT hf = GetWindowFont(*this);
  LOGFONT lf;
  GetObject(hf, sizeof(lf), &lf);

  SelectFont(hdc, GetWindowFont(*this));
  TextOut(ps.hdc, 0, 0, L"ss\tkk", 5);

  EndBufferedPaint(hpb, TRUE);
  EndPaint(*this, &ps);
}
