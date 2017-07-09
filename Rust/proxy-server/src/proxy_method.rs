use std::net::SocketAddr;

pub enum ProxyMethod
{
    DirectConnect(SocketAddr),
    DirectOther(SocketAddr),
    HttpProxy(SocketAddr)
}
