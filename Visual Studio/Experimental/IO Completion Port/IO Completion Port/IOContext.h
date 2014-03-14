#ifndef IOCONTEXT_H
#define IOCONTEXT_H

class IOContext : public OVERLAPPED
{
public:
  enum class Type
  {
    Accept,
    Receive
  };

private:
  Type type;

protected:
  IOContext(Type type);

public:
  Type GetType();
};

#endif // IOCONTEXT_H
