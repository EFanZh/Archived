use configuration::*;
use futures::stream::*;
use proxy::*;
use tokio_core::net::*;
use tokio_core::reactor::*;

pub struct Server
{
    configuration: Configuration,
    core: Core
}

impl Server
{
    pub fn new(configuration: Configuration) -> Server
    {
        let core = Core::new().unwrap();

        return Server { configuration,
                        core: core };
    }

    pub fn run(&mut self)
    {
        let configuration = &self.configuration;
        let core_handle = &self.core.handle();
        let tcp_listener = TcpListener::bind(&self.configuration.bind_address, core_handle).unwrap();

        let server_future = tcp_listener.incoming().for_each(|(client_stream, client_socket_address)| {
            Proxy::handle_proxy(configuration, core_handle, client_stream, client_socket_address);

            return Ok(());
        });

        self.core.run(server_future).unwrap();
    }
}
