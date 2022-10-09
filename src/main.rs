use std::env;
use std::fs;

fn main() {
    let filename: String = env::args().nth(1).unwrap();
    println!("Filename: {}", filename);
    
    assert_neq!(filename, None);

    let text = fs::read_to_string(filename);

    println!("{}", text);
}
