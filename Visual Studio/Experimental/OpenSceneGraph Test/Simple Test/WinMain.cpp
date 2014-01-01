#include "TestScene.h"
#include "TestController.h"

using namespace osg;
using namespace osgViewer;

int WINAPI wWinMain(_In_ HINSTANCE hInstance, _In_opt_ HINSTANCE hPrevInstance, _In_ LPWSTR lpCmdLine, _In_ int nCmdShow)
{
  UNREFERENCED_PARAMETER(hInstance);
  UNREFERENCED_PARAMETER(hPrevInstance);
  UNREFERENCED_PARAMETER(lpCmdLine);
  UNREFERENCED_PARAMETER(nCmdShow);

  auto ds = DisplaySettings::instance();
  ds->setMinimumNumStencilBits(1);
  ds->setNumMultiSamples(4);

  TestScene scene;

  Viewer viewer;
  viewer.apply(new SingleWindow(100, 100, 960, 540));
  viewer.setSceneData(scene.GetSceneData());

  Camera *p_camera = viewer.getCamera();
  p_camera->setClearMask(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT | GL_STENCIL_BUFFER_BIT);
  p_camera->setEventCallback(new TestController());

  return viewer.run();
}
