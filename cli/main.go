package main

import (
	"aoc_cli/runners"
	"aoc_cli/util"
	"fmt"
	"log"
	"math/rand"
	"os"
	"sort"
	"strings"
	"time"

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
	availableLanguages := util.GetAvailableLanguages()

	app.Commands = []*cli.Command{
		{
			Name:  "random",
			Usage: "Ask for random language",
			Action: func(c *cli.Context) error {
				rand.Seed(time.Now().UnixNano())
				min := 1
				max := len(availableLanguages)
				randomValue := rand.Intn(max-min) + min
				fmt.Printf("Why don't you try some \"%s\" today?", availableLanguages[randomValue-1])
				return nil
			},
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
			Action: func(c *cli.Context) error {
				languageName := c.String("language")
				datestr := c.String("date")

				var date *util.Date

				if datestr != "" {
					d := util.GetDate(datestr)
					date = &d
				}

				if languageName == "" {
					fmt.Println("Provide a language for which to generate the next solution file")
					return nil
				}

				if !slices.Contains(availableLanguages, languageName) {
					fmt.Printf("Language: %s is not supported.\n", languageName)
					fmt.Printf("Supported languages: %s\n", strings.Join(availableLanguages, "\n"))
					return nil
				}

				language := util.ConvertLanguage(languageName)
				generate_code_file(language, date)

				return nil
			},
		},
		{
			Name:  "run",
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
			Action: func(c *cli.Context) error {
				languageName := c.String("language")
				inputFile := c.String("input_file")

				if languageName == "" {
					fmt.Println("Provide a language for which to generate the next solution file")
					return nil
				}

				if !slices.Contains(availableLanguages, languageName) {
					fmt.Printf("Language: %s is not supported.\n", languageName)
					fmt.Printf("Supported languages: %s\n", strings.Join(availableLanguages, "\n"))
					return nil
				}

				language := util.ConvertLanguage(languageName)

				datestr := c.String("date")

				if datestr == "" {
					fmt.Println("Provide a date for which to generate a solution")
					return nil
				}

				date := util.GetDate(datestr)
				solve(language, date, inputFile)

				return nil
			},
			Subcommands: []*cli.Command{
				{
					Name:  "supported_languages",
					Usage: "Get a list of supported languages",
					Action: func(c *cli.Context) error {
						var supportedLanguages = runners.GetRegisteredLanguageNames()
						sort.Strings(supportedLanguages)
						fmt.Println(strings.Join(supportedLanguages, "\n"))
						return nil
					},
				},
			},
		},
	}
}

func generate_code_file(language util.Language, date *util.Date) {
	var nextDate util.Date
	if date != nil {
		nextDate = *date
	} else {
		nextDate = util.GetNextDate(language)
	}

	var formattedDate = fmt.Sprintf("%d/%02d", nextDate.Year, nextDate.Day)
	fmt.Printf("Generating code file for: %s, %s\n", language.Name, formattedDate)

	CreateCodeFile(language, nextDate)
}

func solve(language util.Language, date util.Date, inputFile string) {
	fmt.Printf("Solve for %s in \"%s\"\n", date.Format(), language.Name)

	runner := runners.GetRunner(language)

	if runner == nil {
		fmt.Printf("Runner for \"%s\" not found.\n", language.Name)
		return
	}

	runner.Run(date, inputFile)
}
