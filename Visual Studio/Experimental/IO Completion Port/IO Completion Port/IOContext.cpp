#include "IOContext.h"

IOContext::IOContext(Type type) : OVERLAPPED(), type(type)
{
}

IOContext::Type IOContext::GetType()
{
  return type;
}
