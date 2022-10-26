import os

fn main() {
	if os.args.len < 2 {
		println("Please provide a date in format yyyy/dd")
	}

	mut registry := Registry{}
	registry.init()

	date := get_date(os.args)

	solution := registry.get_solution(date)
	input_file := get_inputfile(date, os.args)
	
	solve(solution, input_file)
}

fn solve(solution Solution, input_file string) {
	solution.part1(input_file)
	solution.part2(input_file)
}

fn get_inputfile(date Date, args []string) string {
	if args.len >= 3 {
		return args[2]
	}
         
	return "Inputs/$date.year/${date.day:02}.txt"
}
