#ifndef TESTSCENE_H
#define TESTSCENE_H

class TestScene
{
  osg::ref_ptr<osgViewer::Viewer> rp_viewer;
  osgViewer::GraphicsWindowWin32 *p_graphics_window;
  HANDLE render_thread_handle;

  // Build scene data.
  osg::Node *GetRootNode()
  {
    using namespace osg;

    Group *p_group = new Group();

    Geode *p_shape = new Geode();
    p_shape->addDrawable(new ShapeDrawable(new Box(Vec3(0.0f, 0.0f, 0.0f), 4.0f)));

    LightSource *p_light_source = new LightSource();

    Light *p_light = new Light();

    p_light->setPosition(Vec4(10.0f, 10.0f, 10.0f, 2.0f));
    p_light_source->setLight(p_light);

    p_group->addChild(p_shape);
    // p_group->addChild(p_light_source);

    return p_group;
  }

  static DWORD WINAPI RenderThread(LPVOID lpParameter)
  {
    osgViewer::Viewer *p_viewer = static_cast<osgViewer::Viewer *>(lpParameter);

    while (!p_viewer->done())
    {
      p_viewer->frame();
    }

    return 0;
  }

public:
  TestScene() : rp_viewer(new osgViewer::Viewer()), p_graphics_window(nullptr)
  {
  }

  void BeginRender(HWND hWnd)
  {
    using namespace osg;
    using namespace osgGA;
    using namespace osgViewer;

    RECT rect;
    GetClientRect(hWnd, &rect);

    GraphicsContext::Traits *p_traits = new GraphicsContext::Traits();
    p_traits->x = 0;
    p_traits->y = 0;
    p_traits->width = rect.right;
    p_traits->height = rect.bottom;
    p_traits->samples = 1;
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

  void EndRender()
  {
    rp_viewer->setDone(true);
    WaitForSingleObject(render_thread_handle, INFINITE);
  }

  LRESULT HandleNativeMessage(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
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
};

#endif // TESTSCENE_H
