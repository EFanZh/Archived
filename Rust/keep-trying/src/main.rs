use std::env::*;
use std::process::*;

fn main()
{
    let arguments = args_os().skip(1).collect::<Vec<_>>();

    match arguments.first()
    {
        None => println!("Usage: {} program arguments...",
                         current_exe().unwrap().file_name().unwrap().to_str().unwrap()),
        Some(program) =>
        {
            let mut command = Command::new(program);

            command.args(&arguments[1..]);

            let mut count: usize = 1;

            loop
            {
                println!("{}", count);

                match command.status()
                {
                    Ok(status) => if status.success()
                                  {
                                      break;
                                  },
                    Err(_) => println!("Failed to start program “{}”.", program.to_str().unwrap())
                }

                count += 1;
            }
        }
    }
}
