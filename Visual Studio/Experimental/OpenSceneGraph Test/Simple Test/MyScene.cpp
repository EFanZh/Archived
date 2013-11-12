#include "MyScene.h"
#include "RotateCallback.h"

osg::Node *MyScene::GetSceneData()
{
  using namespace osg;

  MatrixTransform *p_transform = new MatrixTransform();

  Geode *p_geode = new Geode();

  Geometry *p_geometry = new Geometry();

  Vec3Array *p_vertex_array = new Vec3Array(Array::BIND_PER_PRIMITIVE_SET);
  p_vertex_array->push_back(Vec3(-1.0f, 0.0f, -1.0f));
  p_vertex_array->push_back(Vec3(-1.0f, 0.0f, 1.0f));
  p_vertex_array->push_back(Vec3(1.0f, 0.0f, 1.0f));
  p_vertex_array->push_back(Vec3(1.0f, 0.0f, -1.0f));
  p_geometry->setVertexArray(p_vertex_array);

  Vec3Array *p_normal_array = new Vec3Array(Array::BIND_PER_PRIMITIVE_SET);
  p_normal_array->push_back(Vec3(0.0f, -1.0f, 0.0f));
  p_geometry->setNormalArray(p_normal_array);

  Vec4Array *p_color_array = new Vec4Array(Array::BIND_PER_PRIMITIVE_SET);
  p_color_array->push_back(Vec4(1.0, 1.0, 1.0, 1.0));
  p_geometry->setColorArray(p_color_array);

  p_geometry->addPrimitiveSet(new DrawArrays(GL_POLYGON, 0, 4));

  p_geode->addDrawable(p_geometry);

  p_transform->addChild(p_geode);
  p_transform->addUpdateCallback(new RotateCallback());

  return p_transform;
}
