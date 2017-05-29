use std::ptr::*;

pub struct Window {
    handle: ::HWND,
}

impl Window {
    pub fn new() -> Window {
        return Window { handle: null_mut() };
    }

    pub fn set_handle(&mut self, handle: ::HWND) {
        debug_assert!(self.handle == null_mut());
        debug_assert!(handle != null_mut());

        self.handle = handle;
    }

    pub fn window_proc(&mut self,
                       u_msg: ::UINT,
                       w_param: ::WPARAM,
                       l_param: ::LPARAM)
                       -> ::LRESULT {
        return match u_msg {
            ::WM_DESTROY => self.on_destroy(),
            _ => unsafe { ::DefWindowProcW(self.handle, u_msg, w_param, l_param) },
        };
    }

    fn on_destroy(&self) -> ::LRESULT {
        unsafe {
            ::PostQuitMessage(0);
        }

        return 0;
    }
}

