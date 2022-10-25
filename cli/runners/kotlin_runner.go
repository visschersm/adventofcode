package runners

import (
	"aoc_cli/util"
	"fmt"
	"log"
	"os/exec"
)

type KotlinRunner struct{}

// kotlinc -cp . -d Solutions/kotlin/build Solutions/kotlin
func (runner *KotlinRunner) Run(date util.Date, input_file string) {
	cmd := exec.Command(
		"kotlinc",
		"-cp",
		".",
		"-d",
		"Solutions/kotlin/build",
		"Solutions/kotlin")

	runner.Execute(cmd)

	// kotlin -cp Solutions/kotlin/build Solutions.kotlin.AdventOfCodeKt 2015/01
	cmd = exec.Command(
		"kotlin",
		"-cp",
		"Solutions/kotlin/build",
		"Solutions.kotlin.AdventOfCodeKt",
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
