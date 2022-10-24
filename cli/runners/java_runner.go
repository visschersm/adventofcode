package runners

import (
	"aoc_cli/util"
	"fmt"
	"log"
	"os/exec"
)

type JavaRunner struct{}

func (runner *JavaRunner) Run(date util.Date, input_file string) {
	cmd := exec.Command(
		"javac",
		"-cp",
		".",
		"Solutions/java/*.java")
	Execute(cmd)

	cmd = exec.Command(
		"java",
		"-cp",
		".",
		"Solutions/java/AdventOfCode",
		date.Format())
	Execute(cmd)
}

func Execute(cmd *exec.Cmd) {
	stdout, err := cmd.Output()

	if err != nil {
		log.Fatal(err)
	}
	fmt.Println(string(stdout))
}
