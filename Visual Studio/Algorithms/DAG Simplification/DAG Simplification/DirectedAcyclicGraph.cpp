#include "DirectedAcyclicGraph.h"

vector<Vertex *> *DirectedAcyclicGraph::GetStartVertices()
{
  return start_vertices;
}

void DirectedAcyclicGraph::SetStartVertices(vector<Vertex *> *new_start_vertices)
{
  start_vertices = new_start_vertices;
}
