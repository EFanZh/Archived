#![windows_subsystem = "windows"]

extern crate rand;
extern crate direct2d;
extern crate dwrite;
extern crate user32;
extern crate winapi;

mod backend;
mod com_pointer;
mod configuration;
mod painter;
mod raindrop;
mod resource;
mod utilities;
mod window;
mod window_class;

use std::process::*;
use utilities::*;
use window_class::*;

fn real_main() -> i32
{
    let main_window_class = WindowClass::new();
    let _main_window = main_window_class.create_window();

    return message_loop();
}

fn main()
{
    exit(real_main());
}

