#include "UIHelper.h"

static int dpi_x, dpi_y;
static HFONT hf_message;

void InitializeUIHelper(void)
{
  HDC hdc = GetDC(NULL);
  NONCLIENTMETRICS nc = { sizeof(nc) };

  dpi_x = GetDeviceCaps(hdc, LOGPIXELSX);
  dpi_y = GetDeviceCaps(hdc, LOGPIXELSY);
  ReleaseDC(NULL, hdc);

  SystemParametersInfo(SPI_GETNONCLIENTMETRICS, sizeof(nc), &nc, 0);
  hf_message = CreateFontIndirect(&nc.lfMessageFont);
}

void UninitializeUIHelper(void)
{
  DeleteFont(hf_message);
}

int ScaleX(int x)
{
  return MulDiv(x, dpi_x, 96);
}

int ScaleY(int y)
{
  return MulDiv(y, dpi_y, 96);
}

int UnscaleX(int x)
{
  return MulDiv(96, x, dpi_x);
}

int UnscaleY(int y)
{
  return MulDiv(96, y, dpi_y);
}

int XToY(int x)
{
  return MulDiv(x, dpi_y, dpi_x);
}

int YToX(int y)
{
  return MulDiv(y, dpi_x, dpi_y);
}

HFONT GetMessageFont(void)
{
  return hf_message;
}
