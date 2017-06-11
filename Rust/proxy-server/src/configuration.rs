use std::net::*;
use std::str::*;

pub const EVENT_QUEUE_SIZE: usize = 1024;
pub const INITIAL_BUFFER_SIZE: usize = 1024;
pub const BUFFER_SIZE_EXPAND: usize = 3;
pub const BUFFER_SIZE_SHRINK: usize = 2;
pub const HEADER_COUNT: usize = 10;
pub const WRITE_BUFFER_SIZE: usize = 1024;

pub struct Configuration
{
    pub bind_address: SocketAddr
}

impl Configuration
{
    pub fn new() -> Configuration
    {
        return Configuration { bind_address: SocketAddr::from_str("0.0.0.0:9988").unwrap() };
    }
}

