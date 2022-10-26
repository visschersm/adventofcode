import y2015

struct Registry {
	mut:
		registry map[string]Solution
}

fn (mut r Registry) init() {
	r.registry["Solutions/v/y2015/Solution01"] = y2015.Solution01{}
}

pub fn (r Registry) get_solution(date Date) Solution {
	return r.registry['Solutions/v/y$date.year/Solution${date.day:02}']
}
