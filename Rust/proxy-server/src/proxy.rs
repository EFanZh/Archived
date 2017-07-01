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

enum ProxyError
{
    OtherError
}

impl From<Error> for ProxyError
{
    fn from(_: Error) -> ProxyError
    {
        return ProxyError::OtherError;
    }
}

enum State
{
    ReadClientRequest
}

pub struct Proxy;

impl Proxy
{
    pub fn handle_proxy(configuration: &Configuration,
                        core_handle: &Handle,
                        mut client_stream: TcpStream,
                        client_socket_address: SocketAddr)
    {
        core_handle.spawn(ProxyFuture { client_stream,
                                        client_socket_address,
                                        client_header_buffer: [0; HEADER_BUFFER_SIZE],
                                        read_offset: 0,
                                        state: State::ReadClientRequest }
                          .then(|_| Ok(())));
    }
}

struct ProxyFuture
{
    client_stream: TcpStream,
    client_socket_address: SocketAddr,
    client_header_buffer: [u8; HEADER_BUFFER_SIZE],
    read_offset: usize,
    state: State
}

impl Future for ProxyFuture
{
    type Item = ();
    type Error = ProxyError;

    fn poll(&mut self) -> Poll<Self::Item, Self::Error>
    {
        match self.state
        {
            State::ReadClientRequest =>
            {
                loop
                {
                    match try_nb!(self.client_stream.read(&mut self.client_header_buffer[self.read_offset..]))
                    {
                        0 => return Err(ProxyError::OtherError),
                        size =>
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
                    }
                }
            },
        }
    }
}
