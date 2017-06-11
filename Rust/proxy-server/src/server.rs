use configuration::*;
use mio::*;
use mio::net::*;
use proxy::*;
use token_pool::*;

pub struct Server
{
    configuration: Configuration,
    tcp_listener: TcpListener,
    poll: Poll,
    token_pool: TokenPool,
    proxy: Proxy
}

impl Server
{
    pub fn new(configuration: Configuration) -> Server
    {
        let result = Server { tcp_listener: TcpListener::bind(&configuration.bind_address).unwrap(),
                 poll: Poll::new().unwrap(),
                 token_pool: TokenPool::new(),
                 proxy: Proxy::new(),
                 configuration };

        assert!(result.poll
                      .register(&result.tcp_listener,
                                result.token_pool.get_server_token(),
                                Ready::readable(),
                                PollOpt::edge())
                      .is_ok());

        return result;
    }

    pub fn run(&mut self)
    {
        let server_token = self.token_pool.get_server_token();
        let mut events = Events::with_capacity(1024);

        loop
        {
            self.poll.poll(&mut events, None).unwrap();

            for event in events.iter()
            {
                if event.token() == server_token
                {
                    self.proxy.handle_proxy(&self.poll, &mut self.token_pool, self.tcp_listener.accept().unwrap());
                }
                else
                {
                    self.proxy.handle_event(&self.poll, event);
                }
            }
        }
    }
}

