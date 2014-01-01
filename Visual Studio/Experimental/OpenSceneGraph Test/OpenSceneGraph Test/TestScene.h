#ifndef TESTSCENE_H
#define TESTSCENE_H

class TestScene
{
  osg::ref_ptr<osgViewer::Viewer> rp_viewer;
  osgViewer::GraphicsWindowWin32 *p_graphics_window;
  HANDLE render_thread_handle;

  // Build scene data.
  osg::Node *GetRootNode();

  static DWORD WINAPI RenderThread(LPVOID lpParameter);

public:
  TestScene();
  void BeginRender(HWND hWnd);
  void EndRender();
  LRESULT HandleNativeMessage(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam);
};

#endif // TESTSCENE_H
