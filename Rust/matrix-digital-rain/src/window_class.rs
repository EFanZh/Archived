use std::mem::*;
use std::ptr::*;
use winapi::*;
use utilities::*;
use window::*;

pub struct WindowClass {
    handle: ::ATOM,
}

extern "system" fn initial_window_proc(h_wnd: ::HWND,
                                       u_msg: ::UINT,
                                       w_param: ::WPARAM,
                                       l_param: ::LPARAM)
                                       -> ::LRESULT {
    unsafe {
        if u_msg == ::WM_CREATE {
            let create_struct = l_param as *const ::CREATESTRUCTW;
            let window_pointer = (*create_struct).lpCreateParams as *mut Window;
            let window = &mut *window_pointer;

            window.set_handle(h_wnd);

            ::SetWindowLongPtrW(h_wnd, ::GWLP_USERDATA, window_pointer as ::LONG_PTR);
            ::SetWindowLongPtrW(h_wnd, ::GWLP_WNDPROC, window_proc as ::LONG_PTR);

            return window.window_proc(u_msg, w_param, l_param);

        } else {
            return ::DefWindowProcW(h_wnd, u_msg, w_param, l_param);
        }
    }
}

extern "system" fn window_proc(h_wnd: ::HWND,
                               u_msg: ::UINT,
                               w_param: ::WPARAM,
                               l_param: ::LPARAM)
                               -> ::LRESULT {
    unsafe {
        let window_struct = ::GetWindowLongPtrW(h_wnd, ::GWLP_USERDATA) as *mut Window;

        debug_assert!(window_struct != null_mut());

        return (*window_struct).window_proc(u_msg, w_param, l_param);
    }
}

impl WindowClass {
    pub fn new() -> WindowClass {
        let class_name = to_utf_16("Main Window");

        let parameters = WNDCLASSEXW {
            cbSize: size_of::<WNDCLASSEXW>() as ::UINT,
            style: 0,
            lpfnWndProc: Some(initial_window_proc),
            cbClsExtra: 0,
            cbWndExtra: 0,
            hInstance: null_mut(),
            hIcon: null_mut(),
            hCursor: null_mut(),
            hbrBackground: null_mut(),
            lpszMenuName: null(),
            lpszClassName: class_name.as_ptr(),
            hIconSm: null_mut(),
        };

        unsafe {
            let handle = ::RegisterClassExW(&parameters);

            debug_assert!(handle != 0);

            return WindowClass { handle };
        }
    }

    pub fn create_window(self: &WindowClass) -> Box<Window> {
        let result = Box::new(Window::new());
        let window_title = to_utf_16("Matrix Digital Rain");

        unsafe {
            let handle = ::CreateWindowExW(0,
                                           self.handle as ::LPCWSTR,
                                           window_title.as_ptr(),
                                           ::WS_OVERLAPPEDWINDOW | ::WS_VISIBLE,
                                           ::CW_USEDEFAULT,
                                           ::CW_USEDEFAULT,
                                           ::CW_USEDEFAULT,
                                           ::CW_USEDEFAULT,
                                           null_mut(),
                                           null_mut(),
                                           null_mut(),
                                           result.as_ref() as *const Window as ::LPVOID);

            debug_assert!(handle != null_mut());
        }

        return result;
    }
}

