package runners

import (
	"aoc_cli/util"
	"fmt"
	"log"
	"os/exec"
)

type JavaRunner struct{}

func (runner *JavaRunner) Run(date util.Date, input_file string) {
	// 	cmd := exec.Command(
	// 		"javac",
	// 		"-cp",
	// 		".",
	// 		"Solutions/java/*.java")
	// for /r %%a in (.) do (javac %%a\*.java)
	cmd := exec.Command(
		"CMD",
		"/C",
		`cli\runners\java_runner.bat`)
	runner.Execute(cmd, false)

	cmd = exec.Command(
		"java",
		"-cp",
		".",
		"Solutions/java/AdventOfCode",
		date.Format())
	runner.Execute(cmd, true)
}

func (runner *JavaRunner) Execute(cmd *exec.Cmd, print bool) {
	stdout, err := cmd.Output()

	if err != nil {
		log.Fatal(err)
	}

	if print {
		fmt.Println(string(stdout))
	}
}
