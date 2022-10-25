package runners

import (
	"aoc_cli/util"
	"fmt"
	"log"
	"os/exec"
)

type PythonRunner struct{}

// python .\Solutions\python\adventofcode.py --date $date
func (runner *PythonRunner) Run(date util.Date, input_file string) {
	cmd := exec.Command(
		"python",
		`.\Solutions\python\adventofcode.py`,
		"--date",
		date.Format())
	stdout, err := cmd.Output()

	if err != nil {
		log.Fatal(err)
	}
	fmt.Println(string(stdout))
}
