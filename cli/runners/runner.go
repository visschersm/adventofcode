package runners

import (
	"aoc_cli/util"
	"aoc_cli/util/languages"
	"fmt"
)

func Run(language languages.Language, date util.Date, inputFile string) {
	for _, command := range language.Commands {
		for index, arg := range command.Command.Args {
			if arg == "<date>" {
				command.Command.Args[index] = date.Format()
			}
		}

		stdout, err := command.Command.Output()

		if err != nil {
			fmt.Println(command.Command.String())
			fmt.Println(err.Error())
			fmt.Println(string(stdout))
		}

		if command.Print {
			fmt.Println(string(stdout))
		}
	}
}
