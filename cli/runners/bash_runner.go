package runners

import (
	"aoc_cli/util"
	"fmt"
	"log"
	"os/exec"
)

type BashRunner struct{}

func (runner *BashRunner) Run(date util.Date, input_file string) {
	cmd := exec.Command("bash", "Solutions/bash/AdventOfCode.sh", date.Format())
	stdout, err := cmd.Output()

	if err != nil {
		log.Fatal(err)
	}
	fmt.Println(string(stdout))
}
