use std::mem::*;
use std::ptr::*;

pub fn message_loop() -> i32 {
    unsafe {
        let mut msg = uninitialized();

        loop {
            let result = ::GetMessageW(&mut msg, null_mut(), 0, 0);

            debug_assert!(result != -1);

            if result == 0 {
                return msg.wParam as i32;
            }

            ::TranslateMessage(&mut msg);
            ::DispatchMessageW(&mut msg);
        }
    }
}

pub fn to_utf_16<T: ?Sized + AsRef<str>>(s: &T) -> Vec<u16> {
    return s.as_ref().encode_utf16().collect();
}

