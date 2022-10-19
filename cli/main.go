package main

import (
	"fmt"
	"log"
	"math/rand"
	"os"
	"strings"
	"time"

	"github.com/urfave/cli/v2"
	"golang.org/x/exp/slices"
)

var app = cli.NewApp()

var minYear = 2015
var maxYear = 2022
var minDay = 1
var maxDay = 25

var supportedYears []int
var supportedDays []int

func main() {

	info()
	commands()

	err := app.Run(os.Args)
	if err != nil {
		log.Fatal(err)
	}
}

func init() {
	fmt.Println("Init called")
	for i := minYear; i <= maxYear; i++ {
		supportedYears = append(supportedYears, i)
	}

	for i := minDay; i <= maxDay; i++ {
		supportedDays = append(supportedDays, i)
	}
}

func info() {
	app.Name = "AdventOfCode CLI"
	app.Usage = "Tool to generate solutions files"
	app.Version = "0.0.4"
}

func commands() {
	app.Commands = []*cli.Command{
		{
			Name:    "random",
			Aliases: []string{"r"},
			Usage:   "Ask for random language",
			Action: func(c *cli.Context) error {
				availableLanguages := get_available_languages()
				rand.Seed(time.Now().UnixNano())
				min := 1
				max := len(availableLanguages)
				randomValue := rand.Intn(max-min) + min
				fmt.Printf("Why don't you try some \"%s\" today?", availableLanguages[randomValue-1])
				return nil
			},
		},
		{
			Name:    "generate",
			Aliases: []string{"g"},
			Usage:   "Generate Solution file for language",
			Action: func(c *cli.Context) error {
				availableLanguages := get_available_languages()
				languageName := c.Args().First()

				if languageName == "" {
					fmt.Println("Provide a language for which to generate the next solution file")
					return nil
				}

				if !slices.Contains(availableLanguages, languageName) {
					fmt.Printf("Language: %s is not supported.\n", languageName)
					fmt.Printf("Supported languages: %s\n", strings.Join(availableLanguages, "\n"))
					return nil
				}

				language := ConvertLanguage(languageName)
				generate_code_file(language)

				return nil
			},
		},
	}
}

func generate_code_file(language Language) error {
	fmt.Printf("Generating code file for: %s\n", language.name)

	nextDate := GetNextDate(language)
	fmt.Printf("Next date found: %s\n", nextDate.Format())

	CreateCodeFile(language, nextDate)

	return nil
}
