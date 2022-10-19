package main

import (
	"bufio"
	"fmt"
	"log"
	"os"
	"strings"
)

func CreateCodeFile(language Language, date Date) error {
	templateFile := fmt.Sprintf("Templates/%s.tmpl", language.name)
	if !fileExists(templateFile) {
		log.Fatal("Template file does not exist: ", templateFile)
	}

	yearFolderPath := fmt.Sprintf("Solutions/%s/y%d", language.name, date.year)
	generate_path(yearFolderPath)

	fullpath := fmt.Sprintf("%s/solution%02d%s", yearFolderPath, date.day, language.ext)
	fmt.Printf("Fullpath: %s\n", fullpath)

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

	code := generate_code(language, date)

	_, err = textWriter.WriteString(code)

	if err != nil {
		return err
	}

	textWriter.Flush()

	return nil
}

func generate_path(yearFolder string) {
	fmt.Printf("Creating path: %s\n", yearFolder)
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

func generate_code(language Language, date Date) string {
	fmt.Printf("Generating code for: %s\n", language.name)
	var code string

	templatePath := fmt.Sprintf("Templates/%s.tmpl", language.name)
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

	code = strings.Replace(code, "<iyear>", fmt.Sprintf("%d", date.year), -1)
	code = strings.Replace(code, "<iday>", fmt.Sprintf("%d", date.day), -1)
	code = strings.Replace(code, "<year>", fmt.Sprintf("%d", date.year), -1)
	code = strings.Replace(code, "<day>", fmt.Sprintf("%02d", date.day), -1)

	fmt.Printf("Generated code:\n%s\n", code)
	return code
}
