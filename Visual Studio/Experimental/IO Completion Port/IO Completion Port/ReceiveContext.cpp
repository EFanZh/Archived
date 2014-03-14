#include "ReceiveContext.h"

ReceiveContext::ReceiveContext(u_long buffer_size) : IOContext(Type::Receive)
{
  buffer.len = buffer_size;
  buffer.buf = new CHAR[buffer_size];
}

ReceiveContext::~ReceiveContext()
{
  delete[] buffer.buf;
}

LPWSABUF ReceiveContext::GetBuffer()
{
  return &buffer;
}
