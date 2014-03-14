#ifndef TASK_H
#define TASK_H

class Task
{
  SOCKET remote_socket;
  std::vector<char> received_buffer;

public:
  Task(SOCKET accept_socket);

  SOCKET GetRemoteSocket();
  void Receive(void *p_buffer, size_t received_size);
  void Done();
};

#endif // TASK_H
