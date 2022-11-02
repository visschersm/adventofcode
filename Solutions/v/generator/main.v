import os

fn main() {
	generate_code()
}

fn generate_code() {
	get_years()
}

fn get_years() {
	mut years := []int{}
	os.walk_with_context("Solutions/v", &years, directory_callback)
	println()
}

fn directory_callback(context voidptr, path string) {
	cleanup_path := path.replace("Solutions\\v\\", "")
	if cleanup_path.starts_with("y") {
		println("$path, ${cleanup_path.substr(1, 5).int()}")
		year := cleanup_path.substr(1, 5).int()
		context << year
	}
}

// import y2015

// struct Registry {
// 	mut:
// 		registry map[string]Solution
// }

// fn (mut r Registry) init() {
// 	r.registry["Solutions/v/y2015/Solution01"] = y2015.Solution01{}
// }

// pub fn (r Registry) get_solution(date Date) Solution {
// 	return r.registry['Solutions/v/y$date.year/Solution${date.day:02}']
// }
