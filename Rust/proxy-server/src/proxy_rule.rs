use httparse::Request;
use proxy_method::ProxyMethod;
use std::io::Error;
use std::net::{SocketAddr, ToSocketAddrs};
use url::{ParseError, Url};

pub enum ProxyRuleError
{
    OtherError
}

impl From<Error> for ProxyRuleError
{
    fn from(_: Error) -> ProxyRuleError
    {
        return ProxyRuleError::OtherError;
    }
}

impl From<ParseError> for ProxyRuleError
{
    fn from(_: ParseError) -> ProxyRuleError
    {
        return ProxyRuleError::OtherError;
    }
}

fn get_request_path<'h, 'b>(http_request: &Request<'h, 'b>) -> Result<&'b str, ProxyRuleError>
{
    return http_request.path.ok_or(ProxyRuleError::OtherError);
}

fn get_socket_address<T: ToSocketAddrs>(source: T) -> Result<SocketAddr, ProxyRuleError>
{
    return source.to_socket_addrs().map_err(|_| ProxyRuleError::OtherError)?.next().ok_or(ProxyRuleError::OtherError);
}


pub fn select_proxy_method<'h, 'b>(http_request: &Request<'h, 'b>) -> Result<ProxyMethod, ProxyRuleError>
{
    let result = match http_request.method.ok_or(ProxyRuleError::OtherError)?
    {
        "CONNECT" => ProxyMethod::DirectConnect(get_socket_address(get_request_path(http_request)?)?),
        _ => ProxyMethod::DirectOther(get_socket_address(Url::parse(get_request_path(http_request)?)?)?)
    };

    return Ok(result);
}
