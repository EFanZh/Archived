use configuration::*;
use futures::{Async, Poll};
use futures::future::{Future, FutureResult, IntoFuture, Loop, err, lazy, loop_fn};
use futures::stream::repeat;
use httparse::{EMPTY_HEADER, Header, Request, Status, parse_headers};
use std::io::{Error, ErrorKind, Read};
use std::mem::forget;
use std::net::SocketAddr;
use std::rc::Rc;
use std::result::Result;
use tokio_core::net::TcpStream;
use tokio_core::reactor::Handle;
use tokio_io::AsyncRead;

enum ProxyError
{
    OtherError
}

struct Environment<'a, 'b>
{
    configuration: &'a Configuration,
    core_handle: &'b Handle,
    client_stream: TcpStream,
    client_socket_address: SocketAddr
}

struct ReadClientHeaderState
{
    client_header_buffer: Box<[u8; HEADER_BUFFER_SIZE]>,
    read_offset: usize
}

struct WriteClientHeaderState
{
    client_header_buffer: Box<[u8; HEADER_BUFFER_SIZE]>,
    used_buffer_size: usize,
    header_size: usize,
    write_offset: usize
}

pub struct Proxy;

impl Proxy
{
    pub fn handle_proxy(
        configuration: &Configuration,
        core_handle: &Handle,
        mut client_stream: TcpStream,
        client_socket_address: SocketAddr,
    )
    {
        core_handle.spawn(
            ProxyFuture { client_stream,
                          client_socket_address,
                          client_header_buffer: [0; HEADER_BUFFER_SIZE],
                          read_offset: 0 }
            .then(|_| Ok(()))
        );
    }
}

struct ProxyFuture
{
    client_stream: TcpStream,
    client_socket_address: SocketAddr,
    client_header_buffer: [u8; HEADER_BUFFER_SIZE],
    read_offset: usize
}

impl Future for ProxyFuture
{
    type Item = ();
    type Error = ProxyError;

    fn poll(&mut self) -> Poll<Self::Item, Self::Error>
    {
        loop
        {
            match self.client_stream.read(&mut self.client_header_buffer[self.read_offset..])
            {
                Ok(0) => return Ok(Async::Ready(())),
                Ok(size) =>
                {
                    self.read_offset += size;

                    let mut client_header_placeholders = [EMPTY_HEADER; HEADER_COUNT];
                    let mut client_request = Request::new(&mut client_header_placeholders);

                    match client_request.parse(&mut self.client_header_buffer[..self.read_offset])
                    {
                        Ok(Status::Complete(position)) =>
                        {
                            println!("Header size: {}", position);

                            return Ok(Async::Ready(()));
                        },
                        Ok(Status::Partial) =>
                        {
                            if self.read_offset == HEADER_BUFFER_SIZE
                            {
                                return Err(ProxyError::OtherError);
                            }
                            else
                            {
                                continue;
                            }
                        },
                        Err(error) => return Err(ProxyError::OtherError),
                    };
                },
                Err(ref e) if e.kind() == ErrorKind::WouldBlock => return Ok(Async::NotReady),
                Err(e) => return Err(ProxyError::OtherError),
            }
        }
    }
}
