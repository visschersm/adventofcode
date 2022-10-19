package runners

import (
	"aoc_cli/util"
	"fmt"
	"log"
	"os/exec"
)

type CSharpRunner struct{}

func (runner *CSharpRunner) Run(date util.Date, input_file string) {
	cmd := exec.Command("dotnet", "run", "--project", "Solutions/csharp/adventofcode.csproj", date.Format())
	stdout, err := cmd.Output()

	if err != nil {
		log.Fatal(err)
	}
	fmt.Println(string(stdout))
}
