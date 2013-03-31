#ifndef VERTEX_H
#define VERTEX_H

using std::vector;

class Vertex
{
  vector<Vertex *> *successors;

public:
  vector<Vertex *> *GetSuccessors();
  void SetSuccessors(vector<Vertex *> *new_successors);
};

#endif // VERTEX_H
