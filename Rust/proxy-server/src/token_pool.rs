use mio::*;

const SERVER_TOKEN_VALUE: usize = 0;

pub struct TokenPool
{
    previous_token: usize
}

impl TokenPool
{
    pub fn new() -> TokenPool
    {
        return TokenPool { previous_token: usize::max_value() };
    }

    pub fn get_server_token(&self) -> Token
    {
        return Token(SERVER_TOKEN_VALUE);
    }

    pub fn get_client_token(&mut self) -> Token
    {
        self.previous_token = self.previous_token.wrapping_add(1);

        if self.previous_token == SERVER_TOKEN_VALUE
        {
            self.previous_token = self.previous_token.wrapping_add(1);
        }

        return Token(self.previous_token);
    }
}

