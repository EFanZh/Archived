#ifndef ACCEPTCONTEXT_H
#define ACCEPTCONTEXT_H

#include "IOContext.h"

class Task;

class AcceptContext : public IOContext
{
  DWORD buffer_size;
  PVOID p_buffer;
  Task *p_task;

public:
  AcceptContext(DWORD buffer_size, Task *p_task);
  ~AcceptContext();

  PVOID GetBuffer() const;
  DWORD GetBufferSize() const;
  Task *GetTask() const;
};

#endif // ACCEPTCONTEXT_H
