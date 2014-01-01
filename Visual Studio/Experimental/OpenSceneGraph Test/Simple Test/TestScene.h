#ifndef TESTSCENE_H
#define TESTSCENE_H

class TestScene
{
  static osg::Node *CreateSelectableNode(osg::Geode *p_geode);

public:
  osg::Node *GetSceneData();
};

#endif // TESTSCENE_H
