package registration

import (
	"adventofcode/lib"
	"adventofcode/y2015"
	"adventofcode/y2024"

	"fmt"
)

var r = make(map[string]lib.Solution)

func GetSolution(year, day int) lib.Solution {
	return r[fmt.Sprintf("%d/%02d", year, day)]
}

func init() {
	r["2015/01"] = &y2015.Solution01{}
	r["2015/02"] = &y2015.Solution02{}
	r["2024/01"] = &y2024.Solution01{}
}
	