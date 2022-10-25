package runners

import (
	"aoc_cli/util"
	"fmt"
	"log"
	"os/exec"
)

type KotlinRunner struct{}

func (runner *KotlinRunner) Run(date util.Date, input_file string) {
	cmd := exec.Command(
		"kotlinc",
		"-cp",
		".",
		"Solutions/kotlin")

	runner.Execute(cmd)

	cmd = exec.Command(
		"kotlin",
		"-cp",
		".",
		"Solutions/kotlin/AdventofcodeKt.class",
		date.Format())

	runner.Execute(cmd)
}

func (runner *KotlinRunner) Execute(cmd *exec.Cmd) {
	stdout, err := cmd.Output()

	if err != nil {
		log.Fatal(err)
	}
	fmt.Println(string(stdout))
}
