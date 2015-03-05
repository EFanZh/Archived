#include "MainWindow.h"
#include "AcceptContext.h"
#include "Task.h"
#include "ReceiveContext.h"

using namespace std;

int MainWindow::OnCreate(LPCREATESTRUCT lpCreateStruct)
{
    UNREFERENCED_PARAMETER(lpCreateStruct);

    // Create ListView.
    this->m_hWndClient = main_list_view.Create(*this, NULL, NULL, WS_CHILD | WS_VISIBLE | WS_CLIPSIBLINGS | WS_CLIPCHILDREN, WS_EX_CLIENTEDGE);

    main_listen_socket = WSASocket(AF_INET, SOCK_STREAM, IPPROTO_TCP, NULL, 0, WSA_FLAG_OVERLAPPED);

    {
        SOCKADDR_IN listen_address = { AF_INET, htons(7000) };
        listen_address.sin_addr.s_addr = INADDR_ANY;

        ::bind(main_listen_socket, reinterpret_cast<sockaddr *>(&listen_address), sizeof(listen_address));
    }

    listen(main_listen_socket, SOMAXCONN);

    main_io_completion_port = CreateIoCompletionPort(INVALID_HANDLE_VALUE, NULL, NULL, 0);
    CreateIoCompletionPort(reinterpret_cast<HANDLE>(main_listen_socket), main_io_completion_port, NULL, 0);

    {
        // Get AcceptEx function pointer.
        GUID guid = WSAID_ACCEPTEX;
        DWORD accept_ex_length;
        WSAIoctl(main_listen_socket, SIO_GET_EXTENSION_FUNCTION_POINTER, &guid, sizeof(guid), &accept_ex, sizeof(accept_ex), &accept_ex_length, NULL, NULL);
    }

    // Create worker threads.
    int worker_thread_count = thread::hardware_concurrency() * 2;
    worker_threads.reserve(worker_thread_count);
    for (int i = 0; i < worker_thread_count; ++i)
    {
        worker_threads.push_back(thread(&MainWindow::WorkerThread, this));
    }

    PostAccept(); // Initial accept request.

    return 0;
}

void MainWindow::OnDestroy()
{
    this->SetMsgHandled(FALSE);

    // Close sockets.
    closesocket(main_listen_socket);
    while (working_sockets.size() > 0)
    {
        auto i_working_socket = working_sockets.cbegin();

        closesocket(*i_working_socket);
        working_sockets.erase(i_working_socket);
    }

    // Close I/O completion port.
    CloseHandle(main_io_completion_port);

    // Wait for worker threads to finish.
    for (auto &up_worker_thread : worker_threads)
    {
        up_worker_thread.join();
    }
    worker_threads.clear();

    // Release additional resources.
    tasks.clear();
    working_accept_contexts.clear();
    working_receive_contexts.clear();
}

void MainWindow::PostAccept()
{
    SOCKET remote_socket = WSASocket(AF_INET, SOCK_STREAM, IPPROTO_TCP, NULL, 0, WSA_FLAG_OVERLAPPED);
    Task *p_task = new Task(remote_socket);

    tasks_lock.Lock();
    tasks.emplace(p_task);
    tasks_lock.Unlock();

    CreateIoCompletionPort(reinterpret_cast<HANDLE>(remote_socket), main_io_completion_port, reinterpret_cast<ULONG_PTR>(p_task), 0);

    AcceptContext *p_accept_context = new AcceptContext(socket_address_length * 2, p_task);

    working_accept_contexts_lock.Lock();
    working_accept_contexts.emplace(p_accept_context);
    working_accept_contexts_lock.Unlock();

    accept_ex(main_listen_socket, remote_socket, p_accept_context->GetBuffer(), 0, socket_address_length, socket_address_length, NULL, p_accept_context);

    working_sockets_lock.Lock();
    working_sockets.insert(remote_socket);
    working_sockets_lock.Unlock();
}

void MainWindow::PostReceive(SOCKET socket, ReceiveContext *p_receive_context)
{
    DWORD flags = 0;
    WSARecv(socket, p_receive_context->GetBuffer(), 1, NULL, &flags, p_receive_context, NULL);
}

void MainWindow::WorkerThread()
{
    DWORD transfered_size;
    ULONG_PTR p_completion_key;
    IOContext *p_io_context;

    while (GetQueuedCompletionStatus(main_io_completion_port, &transfered_size, &p_completion_key, reinterpret_cast<LPOVERLAPPED *>(&p_io_context), INFINITE))
    {
        switch (p_io_context->GetType())
        {
            case IOContext::Type::Accept:
            {
                AcceptContext *p_accept_context = static_cast<AcceptContext *>(p_io_context);
                ReceiveContext *p_receive_context = new ReceiveContext(buffer_size);

                working_receive_contexts_lock.Lock();
                working_receive_contexts.emplace(p_receive_context);
                working_receive_contexts_lock.Unlock();

                PostReceive(p_accept_context->GetTask()->GetRemoteSocket(), p_receive_context);

                working_accept_contexts_lock.Lock();
                for (auto it_ctx = working_accept_contexts.begin(); it_ctx != working_accept_contexts.end(); ++it_ctx)
                {
                    if (it_ctx->get() == p_accept_context)
                    {
                        working_accept_contexts.erase(it_ctx);
                        break;
                    }
                }
                working_accept_contexts_lock.Unlock();

                PostAccept(); // Begin to accept next socket.

                break;
            }

            case IOContext::Type::Receive:
            {
                Task *p_task = reinterpret_cast<Task *>(p_completion_key);
                ReceiveContext *p_receive_context = static_cast<ReceiveContext *>(p_io_context);

                p_task->Receive(p_receive_context->GetBuffer()->buf, transfered_size);
                if (transfered_size < p_receive_context->GetBuffer()->len)
                {
                    working_receive_contexts_lock.Lock();
                    for (auto it_ctx = working_receive_contexts.begin(); it_ctx != working_receive_contexts.end(); ++it_ctx)
                    {
                        if (it_ctx->get() == p_receive_context)
                        {
                            working_receive_contexts.erase(it_ctx);
                            break;
                        }
                    }
                    working_receive_contexts_lock.Unlock();

                    p_task->Done();
                }
                else
                {
                    PostReceive(p_task->GetRemoteSocket(), p_receive_context); // Re-use receive context.
                }

                break;
            }
        }
    }
}
