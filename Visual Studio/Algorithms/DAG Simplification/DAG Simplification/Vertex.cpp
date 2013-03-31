#include "Vertex.h"

vector<Vertex *> *Vertex::GetSuccessors()
{
  return successors;
}

void Vertex::SetSuccessors(vector<Vertex *> *new_successors)
{
  successors = new_successors;
}
