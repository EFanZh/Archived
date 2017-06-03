use std::mem::*;
use std::ptr::*;
use user32::*;

pub fn message_loop() -> i32 {
    unsafe {
        let mut msg = uninitialized();

        loop {
            let result = GetMessageW(&mut msg, null_mut(), 0, 0);

            debug_assert!(result != -1);

            if result == 0 {
                return msg.wParam as _;
            }

            TranslateMessage(&mut msg);
            DispatchMessageW(&mut msg);
        }
    }
}

pub fn to_utf_16<T: ?Sized + AsRef<str>>(s: &T) -> Vec<u16> {
    return s.as_ref().encode_utf16().chain(Some(0)).collect();
}

pub fn remove_by_indexes<T>(container: &mut Vec<T>, indexes: &[usize]) {
    if indexes.len() > 0 {
        let mut slot = indexes[0];

        for i in 0..indexes.len() - 1 {
            for j in (indexes[i] + 1)..indexes[i + 1] {
                container.swap(slot, j);

                slot += 1;
            }
        }

        for i in (indexes[indexes.len() - 1] + 1)..container.len() {
            container.swap(slot, i);

            slot += 1;
        }

        for _ in 0..indexes.len() {
            container.pop();
        }
    }
}

