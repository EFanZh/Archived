#include "TestScene.h"
#include "TestController.h"

using namespace osg;
using namespace osgFX;

Node *TestScene::CreateSelectableNode(Geode *p_geode)
{
  Outline *p_outline = new Outline();
  p_outline->setColor(Vec4(1.0f, 0.5f, 0.2f, 1.0f));
  p_outline->setWidth(3.0f);
  p_outline->addChild(p_geode);
  p_outline->setEnabled(false);
  return p_outline;
}

Node *TestScene::GetSceneData()
{
  Group *p_group = new Group();

  Geode *p_geode = new Geode();
  p_geode->addDrawable(new ShapeDrawable(new Box(Vec3(), 1.0f)));

  MatrixTransform *p_t1 = new MatrixTransform(Matrix::translate(Vec3(-4.0f, 0.0f, 0.0f)));
  p_t1->addChild(CreateSelectableNode(p_geode));

  MatrixTransform *p_t2 = new MatrixTransform(Matrix::translate(Vec3(4.0f, 0.0f, 0.0f)));
  p_t2->addChild(CreateSelectableNode(p_geode));

  p_group->addChild(p_t1);
  p_group->addChild(p_t2);

  return p_group;
}
