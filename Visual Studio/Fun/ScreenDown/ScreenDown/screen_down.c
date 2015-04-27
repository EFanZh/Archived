#include "screen_down.h"

int DrawScreenBitmap1(HDC hdc_screen_save, HDC hdc_screen, HDC hdc_screen_memory, int *arr, int arr_count, int split_width, int width, int height, double accelerate, HBRUSH hbr_background);
int DrawScreenBitmap2(HDC hdc_screen_save, HDC hdc_screen, HDC hdc_screen_memory, int *arr, int arr_count, int split_width, int width, int height, double accelerate, HBRUSH hbr_background);
int RoundToInt(double x);
double CalcOffset(int n, double accelerate);

void ScreenDown(HDC hdc, HDC hdc_src, int width, int height, int split_width, int delay, double accelerate, COLORREF background_color)
{
  HBITMAP hbm_buffer = CreateCompatibleBitmap(hdc, width, height);
  HDC hdc_buffer = CreateCompatibleDC(hdc);
  HBRUSH hbr_backgound = CreateSolidBrush(background_color);
  int *arr, arr_count = width / split_width + (width % split_width == 0 ? 0 : 1);
  int i, t = 0;

  SelectBitmap(hdc_buffer, hbm_buffer);

  arr = (int *)malloc(sizeof(*arr) * arr_count);

  if (!arr)
  {
    return;
  }

  for (i = 0; i < arr_count; i++)
  {
    arr[i] = -i * delay;
  }

  BitBlt(hdc_buffer, 0, 0, width, height, hdc_src, 0, 0, SRCCOPY);

  while (t < arr_count)
  {
    t = DrawScreenBitmap1(hdc_src, hdc, hdc_buffer, arr, arr_count, split_width, width, height, accelerate, hbr_backgound);
    for (i = 0; i < arr_count; i++)
    {
      arr[i]++;
    }
  }

  for (i = 0; i < arr_count; i++)
  {
    arr[i] = -i * delay;
  }

  t = 0;
  while (t < arr_count)
  {
    t = DrawScreenBitmap2(hdc_src, hdc, hdc_buffer, arr, arr_count, split_width, width, height, accelerate, hbr_backgound);
    for (i = 0; i < arr_count; i++)
    {
      arr[i]++;
    }
  }

  DeleteBrush(hbr_backgound);
  DeleteDC(hdc_buffer);
  DeleteBitmap(hbm_buffer);

  free(arr);
}

int DrawScreenBitmap1(HDC hdc_screen_save, HDC hdc_screen, HDC hdc_screen_memory, int *arr, int arr_count, int split_width, int width, int height, double accelerate, HBRUSH hbr_background)
{
  static RECT rect;

  int i, count = 0;

  for (i = 0; i < arr_count; i++)
  {
    if (arr[i] > 0)
    {
      int old_offset = RoundToInt(CalcOffset(arr[i] - 1, accelerate));
      int new_offset = RoundToInt(CalcOffset(arr[i], accelerate));

      if (new_offset > height)
      {
        new_offset = height;
        count++;
      }

      if (new_offset > old_offset)
      {
        rect.left = i * split_width;
        rect.top = old_offset;
        rect.right = rect.left + split_width;
        rect.bottom = new_offset;
        FillRect(hdc_screen_memory, &rect, hbr_background);
        BitBlt(hdc_screen_memory, rect.left, new_offset, split_width, height - new_offset, hdc_screen_save, rect.left, 0, SRCCOPY);
      }
    }
  }
  BitBlt(hdc_screen, 0, 0, width, height, hdc_screen_memory, 0, 0, SRCCOPY);

  return count;
}

int DrawScreenBitmap2(HDC hdc_screen_save, HDC hdc_screen, HDC hdc_screen_memory, int *arr, int arr_count, int split_width, int width, int height, double accelerate, HBRUSH hbr_background)
{
  int i, count = 0;

  UNREFERENCED_PARAMETER(hbr_background);

  for (i = 0; i < arr_count; i++)
  {
    if (arr[i] > 0)
    {
      int old_offset = RoundToInt(CalcOffset(arr[i] - 1, accelerate));
      int new_offset = RoundToInt(CalcOffset(arr[i], accelerate));

      if (new_offset > height)
      {
        new_offset = height;
        count++;
      }

      if (new_offset > old_offset)
      {
        BitBlt(hdc_screen_memory, i * split_width, 0, split_width, new_offset, hdc_screen_save, i * split_width, height - new_offset, SRCCOPY);
      }
    }
  }
  BitBlt(hdc_screen, 0, 0, width, height, hdc_screen_memory, 0, 0, SRCCOPY);

  return count;
}

int RoundToInt(double x)
{
  return (int)x;
}

double CalcOffset(int n, double accelerate)
{
  return accelerate * n * n;
}
