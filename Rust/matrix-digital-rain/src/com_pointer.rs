use std::ptr::*;
use std::ops::*;
use winapi::*;

pub trait ComInterface {
    unsafe fn release(&mut self) -> ULONG;
}

pub struct ComPointer<T: ComInterface> {
    pointer: *mut T,
}

impl<T: ComInterface> ComPointer<T> {
    pub fn new() -> ComPointer<T> {
        return ComPointer { pointer: null_mut() };
    }

    pub unsafe fn get_address(&mut self) -> *mut *mut T {
        return &mut self.pointer as _;
    }
}

impl<T: ComInterface> Drop for ComPointer<T> {
    fn drop(&mut self) {
        if self.pointer != null_mut() {
            unsafe {
                (*self.pointer).release();
            }
        }
    }
}

impl<T: ComInterface> Deref for ComPointer<T> {
    type Target = T;

    fn deref(&self) -> &T {
        unsafe {
            return &*self.pointer;
        }
    }
}

impl<T: ComInterface> DerefMut for ComPointer<T> {
    fn deref_mut(&mut self) -> &mut T {
        unsafe {
            return &mut *self.pointer;
        }
    }
}

macro_rules! implement_com_interface {
    ($x:ident) => (
        impl ComInterface for $x {
            unsafe fn release(&mut self) -> ULONG {
                return self.Release();
            }
        }
    )
}

implement_com_interface!{IDWriteFont}
implement_com_interface!{IDWriteFontCollection}
implement_com_interface!{IDWriteFontFace}
implement_com_interface!{IDWriteFontFamily}
