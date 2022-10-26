package runners

import (
	"aoc_cli/util"
	"fmt"
	"log"
	"os/exec"
)

type VRunner struct{}

func (runner *VRunner) Run(date util.Date, input_file string) {
	cmd := exec.Command("v", "run", "Solutions/v", date.Format())
	stdout, err := cmd.Output()

	if err != nil {
		log.Fatal(err)
	}
	fmt.Println(string(stdout))
}
