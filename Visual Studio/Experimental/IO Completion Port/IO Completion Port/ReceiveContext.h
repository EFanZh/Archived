#ifndef RECEIVECONTEXT_H
#define RECEIVECONTEXT_H

#include "IOContext.h"

class ReceiveContext : public IOContext
{
  WSABUF buffer;

public:
  ReceiveContext(u_long buffer_size);
  ~ReceiveContext();

  LPWSABUF GetBuffer();
};

#endif // RECEIVECONTEXT_H
