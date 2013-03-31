#ifndef DIRECTEDACYCLICGRAPH_H
#define DIRECTEDACYCLICGRAPH_H

#include "Vertex.h"

using std::vector;

class DirectedAcyclicGraph
{
  vector<Vertex *> *start_vertices;

public:
  vector<Vertex *> *GetStartVertices();
  void SetStartVertices(vector<Vertex *> *new_start_vertices);
};

#endif // DIRECTEDACYCLICGRAPH_H
