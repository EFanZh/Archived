#ifndef ROTATECALLBACK_H
#define ROTATECALLBACK_H

class RotateCallback : public osg::NodeCallback
{
  double angle;

public:
  RotateCallback() : angle(0)
  {
  }

  virtual void operator ()(osg::Node *node, osg::NodeVisitor *nv)
  {
    using namespace osg;

    static_cast<MatrixTransform *>(node)->setMatrix(Matrix::rotate(angle, Vec3(0.0f, 0.0f, 1.0f)));

    angle += 0.1;

    this->traverse(node, nv);
  }
};

#endif // ROTATECALLBACK_H
