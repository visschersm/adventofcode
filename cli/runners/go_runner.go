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
		"run",
		`.\Solutions\go\main.go`,
		"-date",
		date.Format())
	stdout, err := cmd.Output()

	if err != nil {
		log.Fatal(err)
	}
	fmt.Println(string(stdout))
}
