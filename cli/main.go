package main

import (
	"fmt"
	"log"
	"math/rand"
	"os"
	"time"

	"github.com/urfave/cli/v2"
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
	app.Name = "AdventOfCode CLI"
	app.Usage = "Tool to generate solutions files"
	app.Version = "0.0.1"
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
	}
}

func get_available_languages() []string {
	var result []string

	folders, err := os.ReadDir("../Solutions")

	if err != nil {
		log.Fatal(err)
		return result
	}

	for _, folder := range folders {
		fmt.Printf("Folder found: %s\n", folder.Name())
		result = append(result, folder.Name())
	}

	return result
}
