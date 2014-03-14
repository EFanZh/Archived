#include "AcceptContext.h"

AcceptContext::AcceptContext(DWORD buffer_size, Task *p_task) : IOContext(Type::Accept), p_buffer(new char[buffer_size]), buffer_size(buffer_size), p_task(p_task)
{
}

AcceptContext::~AcceptContext()
{
  delete[] p_buffer;
}

PVOID AcceptContext::GetBuffer() const
{
  return p_buffer;
}

DWORD AcceptContext::GetBufferSize() const
{
  return buffer_size;
}

Task *AcceptContext::GetTask() const
{
  return p_task;
}
