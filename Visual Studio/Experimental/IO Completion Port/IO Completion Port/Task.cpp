#include "Task.h"

Task::Task(SOCKET remote_socket) : remote_socket(remote_socket)
{
}

void Task::Receive(void *p_buffer, size_t received_size)
{
  received_buffer.insert(received_buffer.end(), static_cast<char *>(p_buffer), static_cast<char *>(p_buffer)+received_size); // Format error.
}

SOCKET Task::GetRemoteSocket()
{
  return remote_socket;
}

void Task::Done()
{
  received_buffer.push_back('\0');
  MessageBoxA(NULL, received_buffer.data(), NULL, 0);
}
