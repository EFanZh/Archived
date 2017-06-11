use std::net::*;
use std::str::*;

pub struct Configuration
{
    pub bind_address: SocketAddr,
}

impl Configuration
{
    pub fn new() -> Configuration
    {
        return Configuration { bind_address: SocketAddr::from_str("0.0.0.0:9988").unwrap() };
    }
}

