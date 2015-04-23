#ifndef THUNKWINDOW_H
#define THUNKWINDOW_H

#include "WindowHandle.h"

namespace Win32GUILibrary
{
    class ThunkWindow : public WindowHandle
    {
        // Spin lock class.
        class SpinLock
        {
            enum LockState : LONG
            {
                UNLOCKED,
                LOCKED
            };

            LONG lock_state = UNLOCKED;

        public:
            void Lock()
            {
                while (InterlockedExchange(&lock_state, LOCKED) == LOCKED)
                {
                    continue;
                }
            }

            void Unlock()
            {
                InterlockedExchange(&lock_state, UNLOCKED);
            }
        };

        ATL::CStdCallThunk thunk;

        static std::map<DWORD, std::tuple<ThunkWindow *, void *>> create_info_map;
        static SpinLock spin_lock;

    protected:
        // Change window procedure to thunk.
        void CreateFromHWndInternal(HWND hWnd, void *proc, int proc_type)
        {
            this->SetHWnd(hWnd);
            this->thunk.Init(reinterpret_cast<DWORD_PTR>(proc), this);
            this->SetLongPtr(proc_type, reinterpret_cast<LONG_PTR>(thunk.GetCodeAddress()));
        }

        static void AddCreateWindowInfo(ThunkWindow *p_this, void *static_proc)
        {
            spin_lock.Lock();

            create_info_map[GetCurrentThreadId()] = std::make_tuple(p_this, static_proc);

            spin_lock.Unlock();
        }

        static void *InitThunkProc(HWND hWnd, int proc_type)
        {
            ThunkWindow *p_window;
            void *proc;

            // Extract create info.
            spin_lock.Lock();

            const auto &result_iter = create_info_map.find(GetCurrentThreadId());
            std::tie(p_window, proc) = result_iter->second;
            create_info_map.erase(result_iter);

            spin_lock.Unlock();

            p_window->CreateFromHWndInternal(hWnd, proc, proc_type);

            return p_window->thunk.GetCodeAddress();
        }

        static void ProcessWindowClass(WNDCLASSEX &wcex)
        {
            wcex.cbSize = sizeof(wcex);
            wcex.hInstance = HINST_THISCOMPONENT;
        }

    public:
        ~ThunkWindow()
        {
            this->Destroy();
        }
    };
}

#endif // THUNKWINDOW_H
