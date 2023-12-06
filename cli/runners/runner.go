package runners

import (
	"aoc/util"
	"aoc/util/languages"
	"fmt"
)

func Run(language languages.Language, date util.Date, inputFile string) {
	for _, command := range language.Commands {
		for index, arg := range command.Command.Args {
			if arg == "<date>" {
				command.Command.Args[index] = date.Format()
			} else if arg == "<input_file>" {
				command.Command.Args[index] = inputFile
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
