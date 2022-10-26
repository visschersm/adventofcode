package main

import (
	"aoc_cli/util"
	"bufio"
	"fmt"
	"log"
	"os"
	"strings"

	"github.com/urfave/cli/v2"
	"golang.org/x/exp/slices"
)

func GenerateRandomCodeFile(c *cli.Context) error {
	language := util.RandomLanguage()
	date := util.GetNextDate(language)

	createCodeFile(language, date)

	return nil
}

func GenerateCodeFile(c *cli.Context) error {
	language := getLanguage(c)

	if language == nil {
		fmt.Println("Provide a language that is supported for which to generate a Solution file.")
		return nil
	}

	date := getDate(c, *language)

	var formattedDate = fmt.Sprintf("%d/%02d", date.Year, date.Day)
	fmt.Printf("Generating code file for: %s, %s\n", language.Name, formattedDate)

	createCodeFile(*language, *date)

	return nil
}

func createCodeFile(language util.Language, date util.Date) error {
	templateFile := fmt.Sprintf("Templates/%s.tmpl", language.Name)
	if !fileExists(templateFile) {
		log.Fatal("Template file does not exist: ", templateFile)
	}

	yearFolderPath := fmt.Sprintf("Solutions/%s/y%d", language.Name, date.Year)
	generatePath(yearFolderPath)

	fullpath := fmt.Sprintf("%s/solution%02d%s", yearFolderPath, date.Day, language.Ext)

	if fileExists(fullpath) {
		fmt.Println("file already exists")
		return fmt.Errorf("file already exists: %s", fullpath)
	}

	fmt.Printf("Creating file: %s\n", fullpath)
	file, err := os.OpenFile(fullpath, os.O_CREATE|os.O_WRONLY, 0644)
	if err != nil {
		return err
	}

	textWriter := bufio.NewWriter(file)

	code := generateCode(language, date)

	_, err = textWriter.WriteString(code)

	if err != nil {
		return err
	}

	textWriter.Flush()

	return nil
}

func getDate(c *cli.Context, language util.Language) *util.Date {
	var nextDate util.Date

	datestr := c.String("date")

	if datestr != "" {
		nextDate = util.GetDate(datestr)
	} else {
		nextDate = util.GetNextDate(language)
	}

	return &nextDate
}

func getLanguage(c *cli.Context) *util.Language {
	languageName := c.String("language")

	if languageName == "" {
		return nil
	}

	availableLanguages := util.GetAvailableLanguages()
	if !slices.Contains(availableLanguages, languageName) {
		return nil
	}

	language := util.ConvertLanguage(languageName)

	return &language
}

func generatePath(yearFolder string) {
	if err := os.MkdirAll(yearFolder, 0770); err != nil {
		log.Fatal(err)
	}
}

func fileExists(filename string) bool {
	info, err := os.Stat(filename)
	if os.IsNotExist(err) {
		return false
	}
	return !info.IsDir()
}

func generateCode(language util.Language, date util.Date) string {
	fmt.Printf("Generating code for: %s\n", language.Name)
	var code string

	templatePath := fmt.Sprintf("Templates/%s.tmpl", language.Name)
	fmt.Printf("Opening template: %s\n", templatePath)
	template, err := os.Open(templatePath)

	if err != nil {
		log.Fatal(err)
	}

	fileScanner := bufio.NewScanner(template)
	fileScanner.Split(bufio.ScanLines)

	var lines []string
	for fileScanner.Scan() {
		lines = append(lines, fileScanner.Text())
	}

	for _, line := range lines {
		fmt.Printf("Template line: %s\n", line)

		code += line + "\n"
	}

	code = strings.Replace(code, "<iyear>", fmt.Sprintf("%d", date.Year), -1)
	code = strings.Replace(code, "<iday>", fmt.Sprintf("%d", date.Day), -1)
	code = strings.Replace(code, "<year>", fmt.Sprintf("%d", date.Year), -1)
	code = strings.Replace(code, "<day>", fmt.Sprintf("%02d", date.Day), -1)

	fmt.Printf("Generated code:\n%s\n", code)
	return code
}
