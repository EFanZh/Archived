extern crate user32;
extern crate winapi;

mod utilities;
mod window;
mod window_class;

use std::process::*;
use user32::*;
use winapi::*;
use window_class::*;
use utilities::*;

fn main() {
    let main_window_class = WindowClass::new();
    let _ = main_window_class.create_window();

    exit(message_loop());
}

