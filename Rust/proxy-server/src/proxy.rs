use configuration::*;
use futures::{Async, Poll};
use futures::future::Future;
use httparse::{EMPTY_HEADER, Request, Status};
use proxy_method::ProxyMethod;
use proxy_rule::*;
use std::io::Error;
use std::mem::replace;
use std::result::Result;
use tokio_core::net::TcpStream;
use tokio_core::reactor::Handle;
use tokio_io::AsyncRead;
use tokio_io::io::{ReadHalf, WriteHalf, copy, write_all};

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

impl From<ProxyRuleError> for ProxyError
{
    fn from(_: ProxyRuleError) -> ProxyError
    {
        return ProxyError::OtherError;
    }
}

#[derive(Clone, Copy)]
enum ReadHttpHeaderState
{
    Start,
    Cr,
    CrLf,
    CrLfCr
}

struct ReadHttpHeader<T: AsyncRead>
{
    reader: Option<T>,
    buffer: Box<[u8]>,
    read_offset: usize,
    state: ReadHttpHeaderState
}

impl<T: AsyncRead> Future for ReadHttpHeader<T>
{
    type Item = (T, Box<[u8]>, usize, usize);
    type Error = ProxyError;

    fn poll(&mut self) -> Poll<Self::Item, Self::Error>
    {
        const ASCII_LF: u8 = 10;
        const ASCII_CR: u8 = 13;

        let (data_size, header_size) = {
            let reader = self.reader.as_mut().unwrap();

            'the_loop: loop
            {
                match try_nb!(reader.read(&mut self.buffer[self.read_offset..]))
                {
                    0 => return Err(ProxyError::OtherError),
                    size =>
                    {
                        let new_read_offset = self.read_offset + size;
                        let mut new_state = self.state;

                        for i in self.read_offset..new_read_offset
                        {
                            new_state = match (new_state, self.buffer[i])
                            {
                                (ReadHttpHeaderState::Start, ASCII_CR) => ReadHttpHeaderState::Cr,
                                (ReadHttpHeaderState::Cr, ASCII_CR) => ReadHttpHeaderState::Cr,
                                (ReadHttpHeaderState::Cr, ASCII_LF) => ReadHttpHeaderState::CrLf,
                                (ReadHttpHeaderState::CrLf, ASCII_CR) => ReadHttpHeaderState::CrLfCr,
                                (ReadHttpHeaderState::CrLfCr, ASCII_CR) => ReadHttpHeaderState::Cr,
                                (ReadHttpHeaderState::CrLfCr, ASCII_LF) => break 'the_loop (new_read_offset, i + 1),
                                _ => ReadHttpHeaderState::Start
                            };
                        }

                        if new_read_offset == self.buffer.len()
                        {
                            return Err(ProxyError::OtherError);
                        }
                        else
                        {
                            self.read_offset = new_read_offset;
                            self.state = new_state;
                        }
                    }
                }
            }
        };

        return Ok(Async::Ready((replace(&mut self.reader, None).unwrap(),
                                replace(&mut self.buffer, Default::default()),
                                data_size,
                                header_size)));
    }
}

fn read_http_header<T: AsyncRead>(reader: T) -> ReadHttpHeader<T>
{
    return ReadHttpHeader { reader: Some(reader),
                            buffer: Box::new([0; HEADER_BUFFER_SIZE]),
                            read_offset: 0,
                            state: ReadHttpHeaderState::Start };
}

fn get_proxy_method(buffer: &[u8]) -> Result<ProxyMethod, ProxyError>
{
    let mut headers = [EMPTY_HEADER; HEADER_COUNT];
    let mut request = Request::new(&mut headers);

    return match request.parse(buffer)
    {
        Ok(Status::Complete(_)) => Ok(select_proxy_method(&request)?),
        _ => Err(ProxyError::OtherError)
    };
}

fn dispatch_proxy(handle: Handle,
                  client_reader: ReadHalf<TcpStream>,
                  client_writer: WriteHalf<TcpStream>,
                  buffer: Box<[u8]>,
                  data_size: usize,
                  header_size: usize)
                  -> Result<(), ProxyError>
{
    let proxy_method = get_proxy_method(&buffer)?;

    match proxy_method
    {
        ProxyMethod::DirectConnect(server_address) =>
        {
            handle.spawn(
                TcpStream::connect(&server_address, &handle)
                    .and_then(move |server_stream| {
                        let (server_reader, server_writer) = server_stream.split();

                        // TODO: Avoid copying the slice.
                        let client_to_server = write_all(server_writer, Box::from(&buffer[header_size..data_size]))
                            .and_then(|(server_writer, _)| copy(client_reader, server_writer));

                        let server_to_client = write_all(client_writer, "HTTP/1.1 200 OK\r\n\r\n")
                            .and_then(|(client_writer, _)| copy(server_reader, client_writer));

                        return client_to_server.join(server_to_client);
                    })
                    .then(|_| Ok(()))
            );
        },
        ProxyMethod::DirectOther(server_address) =>
        {
            handle.spawn(
                TcpStream::connect(&server_address, &handle)
                    .and_then(move |server_stream| {
                        let (server_reader, server_writer) = server_stream.split();

                        // TODO: Avoid copying the slice.
                        let client_to_server = write_all(server_writer, Box::from(&buffer[..data_size]))
                            .and_then(|(server_writer, _)| copy(client_reader, server_writer));

                        let server_to_client = copy(server_reader, client_writer);

                        return client_to_server.join(server_to_client);
                    })
                    .then(|_| Ok(()))
            );
        },
        _ => return Err(ProxyError::OtherError)
    }

    return Ok(());
}

pub struct Proxy;

impl Proxy
{
    pub fn handle_proxy(core_handle: &Handle, client_stream: TcpStream)
    {
        let cloned_handle = core_handle.clone();
        let (client_reader, client_writer) = client_stream.split();

        core_handle.spawn(
            read_http_header(client_reader)
                .and_then(|(client_reader, buffer, data_size, header_size)|
                    dispatch_proxy(cloned_handle, client_reader, client_writer, buffer, data_size, header_size))
                .then(|_| Ok(()))
        );
    }
}
