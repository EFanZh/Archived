#include "TestScene.h"

using namespace osg;
using namespace osgGA;
using namespace osgViewer;

TestScene::TestScene() : rp_viewer(new Viewer()), p_graphics_window(nullptr)
{
}

Node *TestScene::GetRootNode()
{
  Group *p_group = new Group();

  Geode *p_geode = new Geode();
  p_geode->addDrawable(new ShapeDrawable(new Box(Vec3(), 1.0f)));

  MatrixTransform *p_t1 = new MatrixTransform(Matrix::translate(Vec3(-2.0f, 0.0f, 0.0f)));
  p_t1->addChild(p_geode);

  MatrixTransform *p_t2 = new MatrixTransform(Matrix::translate(Vec3(2.0f, 0.0f, 0.0f)));
  p_t2->addChild(p_geode);

  p_group->addChild(p_t1);
  p_group->addChild(p_t2);

  return p_group;
}

void TestScene::BeginRender(HWND hWnd)
{
  RECT rect;
  GetClientRect(hWnd, &rect);

  GraphicsContext::Traits *p_traits = new GraphicsContext::Traits();
  p_traits->x = 0;
  p_traits->y = 0;
  p_traits->width = rect.right;
  p_traits->height = rect.bottom;
  p_traits->samples = 4;
  p_traits->doubleBuffer = true;
  p_traits->inheritedWindowData = new GraphicsWindowWin32::WindowData(hWnd, false);

  p_graphics_window = new GraphicsWindowWin32(p_traits);

  Camera *p_camera = rp_viewer->getCamera();
  p_camera->setGraphicsContext(p_graphics_window);
  p_camera->setViewport(p_traits->x, p_traits->y, p_traits->width, p_traits->height);
  p_camera->setProjectionMatrixAsPerspective(45.0, (double)p_traits->width / p_traits->height, 1.0, 1000.0);

  rp_viewer->setSceneData(GetRootNode());
  rp_viewer->setCameraManipulator(new TrackballManipulator());

  render_thread_handle = CreateThread(NULL, 0, RenderThread, rp_viewer, 0, NULL);
}

void TestScene::EndRender()
{
  rp_viewer->setDone(true);
  WaitForSingleObject(render_thread_handle, INFINITE);
}

LRESULT TestScene::HandleNativeMessage(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
{
  if (p_graphics_window != nullptr)
  {
    return p_graphics_window->handleNativeWindowingEvent(hWnd, uMsg, wParam, lParam);
  }
  else
  {
    return ::DefWindowProc(hWnd, uMsg, wParam, lParam);
  }
}

DWORD WINAPI TestScene::RenderThread(LPVOID lpParameter)
{
  Viewer *p_viewer = static_cast<Viewer *>(lpParameter);

  while (!p_viewer->done())
  {
    p_viewer->frame();
  }

  return 0;
}
