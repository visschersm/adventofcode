package solutions

type Solution struct {
	Part1 interface{}
	Part2 interface{}
}

type Solver func(string) *Solution

type Solutions map[string]Solver

func (solutions Solutions) Get(date string) Solver {
	return solutions[date]
}

var solutions Solutions

func init() {
	solutions = make(map[string]Solver)
}
