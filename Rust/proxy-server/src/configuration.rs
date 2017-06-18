use std::net::*;
use std::str::*;

pub const EVENT_QUEUE_SIZE: usize = 1024;
pub const HEADER_BUFFER_SIZE: usize = 1024;
pub const HEADER_COUNT: usize = 256;
pub const PROXY_CLIENT_BUFFER_SIZE: usize = 1024;
pub const PROXY_SERVER_BUFFER_SIZE: usize = 1024;

pub struct Configuration
{
    pub bind_address: SocketAddr
}

impl Configuration
{
    pub fn load() -> Configuration
    {
        return Configuration { bind_address: SocketAddr::from_str("0.0.0.0:9988").unwrap() };
    }
}

