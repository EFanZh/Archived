use mio::*;
use mio::net::*;
use proxy_handler::*;
use std::collections::*;
use std::net::SocketAddr;
use token_pool::*;

pub struct Proxy
{
    proxy_handlers: HashMap<Token, ProxyHandler>
}

impl Proxy
{
    pub fn new() -> Proxy
    {
        return Proxy { proxy_handlers: HashMap::new() };
    }

    pub fn handle_proxy(&mut self,
                        poll: &Poll,
                        token_pool: &mut TokenPool,
                        (stream, socket_address): (TcpStream, SocketAddr))
    {
        let token = token_pool.get_client_token();

        assert!(poll.register(&stream, token, Ready::readable() | Ready::writable(), PollOpt::edge()).is_ok());

        self.proxy_handlers.insert(token, ProxyHandler::new(stream, socket_address));
    }

    pub fn handle_event(&mut self, poll: &Poll, event: Event)
    {
        {
            let handler = self.proxy_handlers.get_mut(&event.token()).unwrap();

            match handler.handle_event(poll, event)
            {
                State::Done =>
                {
                    assert!(poll.deregister(handler.get_stream()).is_ok());
                },
                _ => return,
            }
        }

        self.proxy_handlers.remove(&event.token());
    }
}

