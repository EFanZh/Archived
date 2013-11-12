#include "MyScene.h"

int WINAPI wWinMain(_In_ HINSTANCE hInstance, _In_opt_ HINSTANCE hPrevInstance, _In_ LPWSTR lpCmdLine, _In_ int nCmdShow)
{
  UNREFERENCED_PARAMETER(hInstance);
  UNREFERENCED_PARAMETER(hPrevInstance);
  UNREFERENCED_PARAMETER(lpCmdLine);
  UNREFERENCED_PARAMETER(nCmdShow);

  using namespace osgViewer;

  Viewer viewer;

  viewer.setSceneData(MyScene().GetSceneData());
  viewer.apply(new SingleWindow(100, 100, 960, 540));

  return viewer.run();
}
