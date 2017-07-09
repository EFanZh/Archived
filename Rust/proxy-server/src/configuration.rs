use std::net::*;
use std::str::*;

pub const HEADER_BUFFER_SIZE: usize = 4096;
pub const HEADER_COUNT: usize = 256;

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
