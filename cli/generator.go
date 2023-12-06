package main

import (
	"aoc/util"
	"aoc/util/languages"
	"bufio"
	"fmt"
	"log"
	"os"
	"strings"

	"github.com/urfave/cli/v2"
	"golang.org/x/exp/slices"
)

func GenerateRandomCodeFile(c *cli.Context) error {
	language := languages.GetRandomLanguage()

	date := getDate(c, language)

	if date == nil {
		returnValue := util.GetNextDate(language.Name)
		date = &returnValue
	}

	fmt.Printf("Generating code file for: %s in %s\n", date.Format(), language.Name)
	createCodeFile(language, *date)

	return nil
}

func GenerateCodeFile(c *cli.Context) error {
	language := getLanguage(c)

	if language == nil {
		fmt.Println("Provide a language that is supported for which to generate a Solution file.")
		return nil
	}

	date := getDate(c, *language)

	fmt.Printf("Generating code file for: %s in %s\n", date.Format(), language.Name)
	createCodeFile(*language, *date)

	return nil
}

func getYearsFolder(language languages.Language, date util.Date) string {
	source := language.SourceFolder

	if source == "" {
		return fmt.Sprintf("Solutions/%s/y%d", language.Name, date.Year)
	}

	return fmt.Sprintf("Solutions/%s/%s/y%d", language.Name, source, date.Year)
}

func createCodeFile(language languages.Language, date util.Date) error {

	if language.Ext == "" {
		log.Fatal("Language extension missing")
	}

	templateFile := fmt.Sprintf("Templates/%s.tmpl", language.Name)
	if !fileExists(templateFile) {
		log.Fatal("Template file does not exist: ", templateFile)
	}

	yearFolderPath := getYearsFolder(language, date)
	generatePath(yearFolderPath)

	fullpath := fmt.Sprintf("%s/Solution%02d%s", yearFolderPath, date.Day, language.Ext)

	if fileExists(fullpath) {
		fmt.Println("file already exists")
		return fmt.Errorf("file already exists: %s", fullpath)
	}

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

func getDate(c *cli.Context, language languages.Language) *util.Date {
	var nextDate util.Date

	datestr := c.String("date")

	if datestr != "" {
		nextDate = util.GetDate(datestr)
	} else {
		nextDate = util.GetNextDate(language.Name)
	}

	return &nextDate
}

func getLanguage(c *cli.Context) *languages.Language {
	languageName := c.String("language")

	if languageName == "" {
		return nil
	}

	availableLanguages := languages.GetAvailableLanguages()
	if !slices.Contains(availableLanguages, languageName) {
		return nil
	}

	language, _ := languages.GetLanguage(languageName)

	return language
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

func generateCode(language languages.Language, date util.Date) string {
	var code string

	templatePath := fmt.Sprintf("Templates/%s.tmpl", language.Name)
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
		code += line + "\n"
	}

	code = strings.Replace(code, "<iyear>", fmt.Sprintf("%d", date.Year), -1)
	code = strings.Replace(code, "<iday>", fmt.Sprintf("%d", date.Day), -1)
	code = strings.Replace(code, "<year>", fmt.Sprintf("%d", date.Year), -1)
	code = strings.Replace(code, "<day>", fmt.Sprintf("%02d", date.Day), -1)

	return code
}
