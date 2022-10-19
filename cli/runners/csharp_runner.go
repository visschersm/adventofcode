package runners

import (
	"aoc_cli/util"
	"fmt"
	"log"
	"os/exec"
)

type CSharpRunner struct{}

func (runner *CSharpRunner) Run(date util.Date, input_file string) {
	// if($input_file)
	// {
	// 	dotnet run --project .\Solutions\csharp\adventofcode.csproj -- $date $input_file
	// }
	// else
	// {
	// 	dotnet run --project .\Solutions\csharp\adventofcode.csproj -- $date
	// }

	fmt.Println("Run dotnet")
	cmd := exec.Command("dotnet", "run", "--project", "Solutions/csharp/adventofcode.csproj", date.Format())
	stdout, err := cmd.Output()

	if err != nil {
		log.Fatal(err)
	}
	fmt.Println(string(stdout))
	fmt.Println("Done running dotnet")
}
