use std::ascii::*;
use std::net::*;
use std::io::*;

fn print_char(c: u8) {
    if c.is_ascii() {
        print!("{}", char::from(c));
    } else {
        print!("·");
    }
}

fn handle_client(mut stream: TcpStream) {
    const BUFFER_SIZE: usize = 16;

    let mut buffer = [0; BUFFER_SIZE];

    println!("I've got a connection.");
    println!("====");

    while let Ok(length) = stream.read(&mut buffer) {
        for c in &buffer[0..length] {
            print_char(*c);
        }

        if length < BUFFER_SIZE {
            break;
        }
    }

    println!("====");
}

fn main() {
    let listener = TcpListener::bind("0.0.0.0:9988").unwrap();

    for stream in listener.incoming() {
        match stream {
            Ok(stream) => handle_client(stream),
            Err(_) => (),
        }
    }
}

