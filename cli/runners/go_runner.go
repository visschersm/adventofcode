package runners

import (
	"aoc_cli/util"
	"fmt"
	"log"
	"os/exec"
)

type GoRunner struct{}

func (runner *GoRunner) Run(date util.Date, input_file string) {
	cmd := exec.Command(
		"go",
		"generate",
		`.\Solutions\go\adventofcode.go`)

	executeCommand(cmd, false)

	cmd = exec.Command(
		"go",
		"run",
		`.\Solutions\go\adventofcode.go`,
		"-date",
		date.Format())

	executeCommand(cmd, true)
}

func executeCommand(cmd *exec.Cmd, print bool) {
	stdout, err := cmd.Output()

	if err != nil {
		log.Fatal(err)
	}

	if print {
		fmt.Println(string(stdout))
	}
}
