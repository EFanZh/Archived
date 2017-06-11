use mio::*;
use mio::net::*;
use std::net::SocketAddr;

pub enum State
{
    Working,
    Done
}

pub struct ProxyHandler
{
    stream: TcpStream,
    socket_address: SocketAddr
}

impl ProxyHandler
{
    pub fn new(stream: TcpStream, socket_address: SocketAddr) -> ProxyHandler
    {

        return ProxyHandler { stream,
                       socket_address };
    }

    pub fn get_stream(&self) -> &TcpStream
    {
        return &self.stream;
    }

    pub fn handle_event(&mut self, poll: &Poll, event: Event) -> State
    {
        return State::Done;
    }
}

