#include <stdio.h>
#include <stdlib.h>

typedef struct node_struct
{
  int value;
  struct node_struct *next;
} node;

// Create a new node with value.
node *create_node(int value)
{
  node *n = (node *)malloc(sizeof(node));

  if (n != NULL)
  {
    n->value = value;
    n->next = NULL;
  }

  return n;
}

// Insert a existing node into an ORDERED list.
void insert_node(node **list, node *node)
{
  if (*list == NULL)
  {
    *list = node;
  }
  else if (node->value < (*list)->value)
  {
    node->next = *list;
    *list = node;
  }
  else
  {
    insert_node(&(*list)->next, node);
  }
}

// Delete a node from a list. Return value indicates whether a node is deleted.
int delete_node(node **list, node *node)
{
  if (*list == NULL)
  {
    return 0;
  }
  else if (*list == node)
  {
    *list = node->next;
    free(node);
    return 1;
  }
  else
  {
    return delete_node(&(*list)->next, node);
  }
}

// Find the first node whose value equals the given value.
node *find_node(node *list, int value)
{
  if (list == NULL)
  {
    return NULL;
  }
  else if (list->value == value)
  {
    return list;
  }
  else
  {
    return find_node(list->next, value);
  }
}

// Tests.
int main(void)
{
  node *list = create_node(7);

  insert_node(&list, create_node(4));
  insert_node(&list, create_node(8));
  insert_node(&list, create_node(1));
  insert_node(&list, create_node(1));
  insert_node(&list, create_node(3));

  delete_node(&list, find_node(list, 4));

  return EXIT_SUCCESS;
}
