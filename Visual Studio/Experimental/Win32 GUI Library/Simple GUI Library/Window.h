class Window
{
public:
  Window();
  ~Window();

private:
  static LRESULT CALLBACK StaticWindowProc(HWND hWNd, UINT uMsg, WPARAM wParam, LPARAM lParam)
  {
  }
};
