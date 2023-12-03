// https://cli.urfave.org/
package main

import (
	"aoc_cli/runners"
	"aoc_cli/util"
	"aoc_cli/util/languages"
	"fmt"
	"log"
	"os"
	"sort"
	"strings"

	"github.com/urfave/cli/v2"
	"golang.org/x/exp/slices"
)

var app = cli.NewApp()

func main() {
	info()
	commands()

	err := app.Run(os.Args)
	if err != nil {
		log.Fatal(err)
	}
}

func info() {
	app.Name = "aoc"
	app.Usage = "Tool to generate advent of code solution files in multiple languages."
	app.Version = "0.0.5"
}

func commands() {
	app.Commands = []*cli.Command{
		{
			Name:   "random",
			Usage:  "Ask for random language",
			Action: getRandomSuggestion,
		},
		{
			Name:  "generate",
			Usage: "Generate Solution file for language",
			Flags: []cli.Flag{
				&cli.StringFlag{
					Name:    "language",
					Usage:   "language to generate solution in",
					Aliases: []string{"l"},
				},
				&cli.StringFlag{
					Name:    "date",
					Usage:   "date to generate solution for",
					Aliases: []string{"d"},
				},
			},
			Action: GenerateCodeFile,
			Subcommands: []*cli.Command{
				{
					Name:   "random",
					Usage:  "Generate next solution file for random language",
					Action: GenerateRandomCodeFile,
					Flags: []cli.Flag{
						&cli.StringFlag{
							Name:    "date",
							Usage:   "date to generate solution for",
							Aliases: []string{"d"},
						},
					},
				},
			},
		},
		{
			Name:  "solve",
			Usage: "Solve a challenge for a specific language",
			Flags: []cli.Flag{
				&cli.StringFlag{
					Name:    "language",
					Usage:   "language to generate solution in",
					Aliases: []string{"l"},
				},
				&cli.StringFlag{
					Name:    "date",
					Usage:   "date to generate solution for",
					Aliases: []string{"d"},
				},
				&cli.StringFlag{
					Name:  "input_file",
					Usage: "Input file instead the challenge input",
				},
			},
			Action: solveChallenge,
			Subcommands: []*cli.Command{
				{
					Name:   "languages",
					Usage:  "Get a list of supported languages",
					Action: printSupportedLanguageList,
				},
			},
		},
	}
}

func solve(language languages.Language, date util.Date, inputFile string) {
	fmt.Printf("Solve for %s in \"%s\"\n", date.Format(), language.Name)

	runners.Run(language, date, inputFile)
}

func getRandomSuggestion(c *cli.Context) error {
	fmt.Printf("Why don't you try some \"%s\" today?", languages.GetRandomLanguage().Name)
	return nil
}

func solveChallenge(c *cli.Context) error {
	languageName := c.String("language")
	inputFile := c.String("input_file")

	if languageName == "" {
		fmt.Println("Provide a language for which to generate the next solution file")
		return nil
	}

	availableLanguages := languages.GetAvailableLanguages()
	if !slices.Contains(availableLanguages, languageName) {
		fmt.Printf("Language: %s is not supported.\n", languageName)
		fmt.Printf("Supported languages: %s\n", strings.Join(availableLanguages, "\n"))
		return nil
	}

	language, err := languages.GetLanguage(languageName)

	if err != nil {
		return fmt.Errorf(err.Error())
	}

	datestr := c.String("date")

	if datestr == "" {
		fmt.Println("Provide a date for which to generate a solution")
		return nil
	}

	date := util.GetDate(datestr)

	if inputFile == "" {
		inputFile = getInputFile(date)
	}

	solve(*language, date, inputFile)

	return nil
}

func printSupportedLanguageList(c *cli.Context) error {
	var supportedLanguages = languages.GetRegisteredLanguageNames()
	sort.Strings(supportedLanguages)
	fmt.Println(strings.Join(supportedLanguages, "\n"))
	return nil
}

func getInputFile(date util.Date) string {
	return fmt.Sprintf("Inputs/%d/%02d.txt", date.Year, date.Day)
}
