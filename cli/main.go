// https://cli.urfave.org/
package main

import (
	"aoc/runners"
	"aoc/util"
	"aoc/util/languages"
	"fmt"
	"log"
	"os"
	"os/exec"
	"sort"
	"strings"

	"github.com/urfave/cli/v2"
	"golang.org/x/exp/slices"
)

var app = cli.NewApp()

func main() {
	Info()
	Commands()

	log.New(os.Stdout, "INFO: ", log.Ldate|log.Ltime)

	err := app.Run(os.Args)
	if err != nil {
		log.Fatal(err)
	}
}

func Info() {
	app.Name = "aoc"
	app.Usage = "Tool to generate advent of code solution files in multiple languages."
	app.Version = "0.0.5"
}

func Commands() {
	app.Commands = []*cli.Command{
		{
			Name:   "random",
			Usage:  "Ask for random language",
			Action: GetRandomSuggestion,
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
					Usage:  "Generate random solution file",
					Action: GenerateRandomCodeFile,
					Flags: []cli.Flag{
						&cli.StringFlag{
							Name:    "date",
							Usage:   "if you want to specify the date but not the language",
							Aliases: []string{"d"},
						},
						&cli.StringFlag{
							Name:    "language",
							Usage:   "if you want to specify the language but not the date",
							Aliases: []string{"l"},
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
			Action: SolveChallenge,
			Subcommands: []*cli.Command{
				{
					Name:   "languages",
					Usage:  "Get a list of supported languages",
					Action: PrintSupportedLanguageList,
				},
			},
		},
		{
			Name:   "todo",
			Usage:  "Add todo item",
			Action: AddTodoItem,
		},
		{
			Name:   "open",
			Usage:  "Open solution file",
			Action: OpenSolution,
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
		},
		{
			Name:   "add",
			Usage:  "Add language",
			Action: AddLanguage,
		},
		{
			Name:   "update",
			Usage:  "Update aoc cli tool",
			Action: UpdateCLI,
		},
	}
}

func Solve(language languages.Language, date util.Date, inputFile string) {
	fmt.Printf("Solve for %s in \"%s\"\n", date.Format(), language.Name)

	runners.Run(language, date, inputFile)
}

func GetRandomSuggestion(c *cli.Context) error {
	log.Print("[GetRandomSuggestion]")
	fmt.Printf("Why don't you try some \"%s\" today?", languages.GetRandomLanguage().Name)
	return nil
}

func SolveChallenge(c *cli.Context) error {
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
		inputFile = GetInputFile(date)
	}

	Solve(*language, date, inputFile)

	return nil
}

func PrintSupportedLanguageList(c *cli.Context) error {
	var supportedLanguages = languages.GetRegisteredLanguageNames()
	sort.Strings(supportedLanguages)
	fmt.Println(strings.Join(supportedLanguages, "\n"))
	return nil
}

func GetInputFile(date util.Date) string {
	return fmt.Sprintf("Inputs/%d/%02d.txt", date.Year, date.Day)
}

func AddTodoItem(c *cli.Context) error {
	todoItem := fmt.Sprintf("- [ ] %s\n", c.Args().First())
	filename := "todolist.md"

	// Open the file for appending (create it if it doesn't exist)
	file, err := os.OpenFile(filename, os.O_WRONLY|os.O_CREATE|os.O_APPEND, 0644)
	if err != nil {
		return err
	}
	defer file.Close()

	// Write the data to the file
	_, err = file.WriteString(todoItem)
	if err != nil {
		return err
	}

	fmt.Println("Data appended successfully.")
	return nil
}

func OpenSolution(c *cli.Context) error {
	log.Print("[OpenSolution]")

	filename := c.Args().Get(0)

	log.Printf("[OpenSolution] Provided filename: %s", filename)

	if filename == "" {
		log.Printf("[OpenSolution] No filename provided, check flags")

		language := GetLanguage(c)

		if language == nil {
			log.Printf("[OpenSolution] No language flag provided")
			return nil
		}

		date := GetDate(c, *language)

		if date == nil {
			log.Printf("[OpenSolution] No date flag provided")
			return nil
		}

		filename = GetFullPath(*language, *date)
	}

	cmd := exec.Command("code", filename)
	cmd.Run()
	return nil
}

func AddLanguage(c *cli.Context) error {
	// TODO: Add language
	return nil
}

func UpdateCLI(c *cli.Context) error {
	fmt.Println("Updating cli...")
	cmd := exec.Command("go", "install", "./cli")
	if err := cmd.Run(); err != nil {
		log.Fatal(err)
	}
	fmt.Println("Updated cli")
	return nil
}
