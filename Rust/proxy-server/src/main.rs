extern crate httparse;
extern crate mio;

mod configuration;
mod proxy;
mod proxy_handler;
mod server;
mod token_pool;

use configuration::*;
use server::*;

fn main()
{
    Server::new(Configuration::new()).run();
}

