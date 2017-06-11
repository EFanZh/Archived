use configuration::*;
use httparse::*;
use mio::*;
use mio::net::*;
use std::io::Read;
use std::net::SocketAddr;

pub enum State
{
    Working,
    Done
}

enum InternalState
{
    ReadRequestHead
    {
        buffer: Vec<u8>,
        read_offset: usize
    },
    Proxy{
        buffer:[u8; WRITE_BUFFER_SIZE]
    },
    Error
}

pub struct ProxyHandler
{
    stream: TcpStream,
    socket_address: SocketAddr,
    state: InternalState
}

impl ProxyHandler
{
    pub fn new(stream: TcpStream, socket_address: SocketAddr, configuration: &Configuration) -> ProxyHandler
    {
        return ProxyHandler { stream,
                       socket_address,
                       state: InternalState::ReadRequestHead { buffer: vec![0; INITIAL_BUFFER_SIZE],
                                                        read_offset: 0 } };
    }

    pub fn get_stream(&self) -> &TcpStream
    {
        return &self.stream;
    }

    pub fn handle_event(&mut self, poll: &Poll, event: Event, configuration: &Configuration) -> State
    {
        let (internal_state, result) = match self.state
        {
            InternalState::ReadRequestHead {ref mut buffer,
                                             ref mut read_offset } =>
            {
                let mut headers = [EMPTY_HEADER; HEADER_COUNT];

                match self.stream.read(&mut buffer[*read_offset..])
                {
                    Ok(size) =>
                    {
                        match parse_headers(buffer, &mut headers)
                        {
                            Ok(Status::Partial) => {
                                *read_offset+=size;

                                return State::Working;
                            },
                            Ok(Status::Complete((head_size, headers))) =>
                            {
                                (InternalState::Proxy{buffer: [0; WRITE_BUFFER_SIZE]}, State::Working)
                            },
                            Err(Error::TooManyHeaders)=> (InternalState::Error, State::Done),
                            Err(_)=> (InternalState::Error, State::Done)
                        }
                    },
                    Err(_)=> (InternalState::Error, State::Done)
                }
            },
            InternalState::Proxy{buffer}=>
            {
                return State::Working;
            }
            InternalState::Error => unreachable!()
        };

        self.state = internal_state;

        return result;
    }
}

