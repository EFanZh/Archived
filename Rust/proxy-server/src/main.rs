extern crate futures;
extern crate httparse;

#[macro_use]
extern crate tokio_core;

extern crate tokio_io;
extern crate url;

mod configuration;
mod proxy;
mod proxy_method;
mod proxy_rule;
mod server;

use configuration::*;
use server::*;

fn main()
{
    Server::new(Configuration::load()).run();
}
