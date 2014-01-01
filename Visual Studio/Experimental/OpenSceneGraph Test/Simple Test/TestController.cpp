#include "TestController.h"

using namespace osgFX;
using namespace osgGA;
using namespace osgUtil;
using namespace osgViewer;

TestController::TestController() : tc_current_selection(nullptr)
{
}

void TestController::ChangeSelection(Outline *new_selection)
{
  if (new_selection != tc_current_selection)
  {
    if (tc_current_selection != nullptr)
    {
      tc_current_selection->setEnabled(false);
    }
    if (new_selection != nullptr)
    {
      new_selection->setEnabled(true);
    }
    tc_current_selection = new_selection;
  }
}

bool TestController::handle(const GUIEventAdapter &ea, GUIActionAdapter &aa)
{
  if (ea.getEventType() == GUIEventAdapter::PUSH)
  {
    saved_x = ea.getX();
    saved_y = ea.getY();
    saved_time = ea.getTime();
  }
  else if (ea.getEventType() == GUIEventAdapter::RELEASE)
  {
    if (ea.getX() == saved_x && ea.getY() == saved_y && ea.getTime() - saved_time <= 0.25)
    {
      LineSegmentIntersector *p_lsi = new LineSegmentIntersector(Intersector::WINDOW, ea.getX(), ea.getY());
      IntersectionVisitor *iv = new IntersectionVisitor(p_lsi);

      aa.asView()->getCamera()->accept(*iv);

      if (p_lsi->containsIntersections())
      {
        ChangeSelection(static_cast<Outline *>(*(p_lsi->getFirstIntersection().nodePath.rbegin() + 1)));
      }
      else
      {
        ChangeSelection(nullptr);
      }
    }
  }
  return false;
}
