package runners

import "aoc_cli/util"

type Runner interface {
	Run(date util.Date, inputFile string)
}
