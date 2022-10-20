package runners

import (
	"aoc_cli/util"
	"fmt"
	"log"
	"os/exec"
)

type PHPRunner struct{}

func (runner *PHPRunner) Run(date util.Date, input_file string) {
	fmt.Printf("PHP runner for date: %s\n", date.Format())

	cmd := exec.Command("php", "Solutions/php/adventofcode.php", "-d", date.Format())
	stdout, err := cmd.Output()

	if err != nil {
		log.Fatal(err)
	}
	fmt.Println(string(stdout))
}
