use configuration::*;
use futures::future::{Future, FutureResult, IntoFuture, Loop, err, lazy, loop_fn};
use futures::stream::repeat;
use httparse::{EMPTY_HEADER, Header, Status, parse_headers};
use std::io::{Error, ErrorKind, Read};
use std::mem::forget;
use std::rc::Rc;
use std::net::SocketAddr;
use std::result::Result;
use tokio_core::net::TcpStream;
use tokio_core::reactor::Handle;

enum ProxyError {
    OtherError,
}

struct Environment<'a, 'b> {
    configuration: &'a Configuration,
    core_handle: &'b Handle,
    client_stream: TcpStream,
    client_socket_address: SocketAddr,
}

struct ReadClientHeaderState {
    client_header_buffer: Box<[u8; HEADER_BUFFER_SIZE]>,
    read_offset: usize,
}

struct WriteClientHeaderState {
    client_header_buffer: Box<[u8; HEADER_BUFFER_SIZE]>,
    used_buffer_size: usize,
    header_size: usize,
    write_offset: usize,
}

pub struct Proxy;

impl Proxy {
    pub fn handle_proxy(
        configuration: &Configuration,
        core_handle: &Handle,
        mut client_stream: TcpStream,
        client_socket_address: SocketAddr,
    ) {
        core_handle.spawn(
            loop_fn(
                ReadClientHeaderState {
                    client_header_buffer: Box::new([0; HEADER_BUFFER_SIZE]),
                    read_offset: 0,
                },
                move |mut state| {
                    client_stream.read(&mut state.client_header_buffer[state.read_offset..]).into_future().then(
                        |client_read_result| match client_read_result {
                            Ok(0) => {
                                if state.read_offset == state.client_header_buffer.len() {
                                    Err(ProxyError::OtherError)
                                } else {
                                    Err(ProxyError::OtherError)
                                }
                            }
                            Ok(read_size) => {
                                state.read_offset += read_size;

                                match parse_headers(
                                    &state.client_header_buffer[..state.read_offset],
                                    &mut [EMPTY_HEADER; HEADER_COUNT],
                                ) {
                                    Ok(Status::Complete((position, _))) => {
                                        Ok(Loop::Break(WriteClientHeaderState {
                                            client_header_buffer: state.client_header_buffer,
                                            used_buffer_size: state.read_offset,
                                            write_offset: 0,
                                            header_size: position,
                                        }))
                                    }
                                    Ok(Status::Partial) => Ok(Loop::Continue(state)),
                                    Err(error) => Err(ProxyError::OtherError),
                                }
                            }
                            Err(error) => Err(ProxyError::OtherError),
                        },
                    )
                },
            ).then(|_| Ok(())),
        );
    }
}
