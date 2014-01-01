#ifndef TESTCONTROLLER_H
#define TESTCONTROLLER_H

class TestController : public osgGA::GUIEventHandler
{
  HWND hWnd;
  osgFX::Outline *tc_current_selection;
  float saved_x, saved_y;
  double saved_time;

  void ChangeSelection(osgFX::Outline *new_selection);

public:
  TestController();

  virtual bool handle(const osgGA::GUIEventAdapter &ea, osgGA::GUIActionAdapter &aa) override;
};

#endif // TESTCONTROLLER_H
