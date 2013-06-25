#ifndef PREVIEWVIEW_H
#define PREVIEWVIEW_H

class PreviewView : public Win32GUILibrary::UserWindow<PreviewView>
{
  LPCTSTR text;

public:
  DECLARE_USER_WINDOW_CLASS(0, 0, 0, NULL, NULL, GetSysColorBrush(COLOR_WINDOW), NULL, TEXT("PreviewView"), NULL);

  LRESULT WindowProc(UINT uMsg, WPARAM wParam, LPARAM lParam);
  void OnPaint();
};

#endif // PREVIEWVIEW_H
