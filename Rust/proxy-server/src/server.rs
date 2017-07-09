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
        let core_handle = &self.core.handle();
        let tcp_listener = TcpListener::bind(&self.configuration.bind_address, core_handle).unwrap();

        let server_future =
            tcp_listener.incoming().for_each(|(client_stream, _)| Ok(Proxy::handle_proxy(core_handle, client_stream)));

        self.core.run(server_future).unwrap();
    }
}
