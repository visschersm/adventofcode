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
		"adventofcode.kt",
		"2015/01",
		// "-include-runtime",
		// "-d",
		// "Solutions/kotlin",
		// "-jar",
		// "adventofcode.jar",
		date.Format())
	stdout, err := cmd.Output()

	if err != nil {
		log.Fatal(err)
	}
	fmt.Println(string(stdout))
}
