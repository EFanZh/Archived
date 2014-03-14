#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include "AcceptContext.h"
#include "ReceiveContext.h"
#include "Task.h"
#include "SpinLock.h"

class MainWindow : public CFrameWindowImpl<MainWindow>
{
  friend CFrameWindowImpl<MainWindow>;

  DECLARE_FRAME_WND_CLASS(TEXT("MainWindow"), 0)

  int buffer_size = 256;
  const int socket_address_length = sizeof(SOCKADDR_IN)+16; // Format error.

  CListViewCtrl main_list_view;
  SOCKET main_listen_socket;
  HANDLE main_io_completion_port;
  LPFN_ACCEPTEX accept_ex;
  std::vector<std::thread> worker_threads;
  std::set<SOCKET> working_sockets;
  SpinLock working_sockets_lock;
  std::set<std::unique_ptr<Task>> tasks;
  SpinLock tasks_lock;
  std::set<std::unique_ptr<AcceptContext>> working_accept_contexts;
  SpinLock working_accept_contexts_lock;
  std::set<std::unique_ptr<ReceiveContext>> working_receive_contexts;
  SpinLock working_receive_contexts_lock;

  BEGIN_MSG_MAP(MainWindow)
    MSG_WM_CREATE(OnCreate)
    MSG_WM_DESTROY(OnDestroy)
    CHAIN_MSG_MAP(CFrameWindowImpl<MainWindow>)
  END_MSG_MAP()

  int OnCreate(LPCREATESTRUCT lpCreateStruct);
  void OnDestroy();

  void ListenThread();
  void PostAccept();
  void PostReceive(SOCKET socket, ReceiveContext *p_receive_context);
  void WorkerThread();
};

#endif // MAINWINDOW_H
