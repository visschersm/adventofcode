use std::env;
use std::fs;

fn main() {
    let filename: String = env::args().nth(1).unwrap();
    println!("Filename: {}", filename);
    
    let text = fs::read_to_string(filename).expect("File not found");

    println!("{}", text);

    println!("Hello World");
}
