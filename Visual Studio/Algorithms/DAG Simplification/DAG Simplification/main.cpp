#include "DirectedAcyclicGraph.h"

void SimplifyDAG(DirectedAcyclicGraph *dag)
{
  for (auto v : *dag->GetStartVertices())
  {
    std::function<void (Vertex *)> search = [&search] (Vertex *vertex) -> void
    {
      for (auto v : *vertex->GetSuccessors())
      {
      }
    };
  }
}

int main()
{
}
