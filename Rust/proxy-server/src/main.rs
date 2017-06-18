extern crate futures;
extern crate httparse;
extern crate tokio_core;

mod configuration;
mod proxy;
mod server;

use configuration::*;
use server::*;

fn main()
{
    Server::new(Configuration::load()).run();
}

