module y2015

import os

pub struct Solution01 {

}

pub fn (s Solution01) part1(input_file string) {
	data := os.read_file(input_file) or {
		println(err)
		return
	}

	mut result := 0
	for c in data {
		result += if c == '('.bytes()[0] { 1 } else { -1 }
	}

	println("Santa is on the ${result}th floor")
}

pub fn (s Solution01) part2(input_file string) {
	data := os.read_file(input_file) or {
		println(err)
		return
	}

	mut result := 0
	mut try_counter := 0

	for c in data {
		result += if c == '('.bytes()[0] { 1 } else { -1 }
		try_counter++

		if result < 0 {
			break
		}
	}

	println("Santa found the basement after $try_counter tries")
}