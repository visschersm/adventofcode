//go:build ignore
// +build ignore

package main

import (
	"bufio"
	"fmt"
	"log"
	"os"
	"strconv"
	"strings"
)

func main() {
	solutionDays := search_directories()

	generate_code(solutionDays)
}

type Date struct {
	year, day int
}

func search_directories() []Date {
	var result []Date

	folders, err := os.ReadDir(".")
	if err != nil {
		log.Fatal(err)
		return result
	}

	for _, folder := range folders {

		if !folder.IsDir() {
			continue
		}

		if !strings.HasPrefix(folder.Name(), "y") {
			continue
		}

		year, convErr := strconv.Atoi(folder.Name()[1:])

		if convErr != nil {
			log.Fatal("Could not convert year: ", folder.Name())
			return result
		}

		solutionFolder, err2 := os.ReadDir(folder.Name())

		if err2 != nil {
			log.Fatal(err2)
			return result
		}

		for _, file := range solutionFolder {
			day, convErr := strconv.Atoi(file.Name()[8:10])

			if convErr != nil {
				log.Fatal("Could not convert day: ", file.Name()[8:10])
				return result
			}

			result = append(result, Date{year: year, day: day})
		}
	}

	return result
}

func generate_code(dates []Date) {
	directory := "registration"
	filename := directory
	fullpath := directory + "/" + filename + ".go"

	create_folder(directory)
	remove_old_generation(fullpath)

	file, ok := create_new_generation_file(fullpath)
	if !ok {
		return
	}
	defer file.Close()

	write_code(file, dates)
}

func create_folder(directory string) {
	if err := os.MkdirAll(directory, 0770); err != nil {
		log.Fatal(err)
	}
}

func remove_old_generation(fullpath string) {
	os.Remove(fullpath)
}

func create_new_generation_file(fullpath string) (*os.File, bool) {
	file, err := os.OpenFile(fullpath, os.O_CREATE|os.O_WRONLY, 0644)
	if err != nil {
		log.Fatal(err)
		return nil, false
	}

	return file, true
}

func get_years(dates []Date) []int {
	result := []int{}

	for _, date := range dates {
		if !isElementExist(result, date.year) {
			result = append(result, date.year)
		}
	}

	return result
}

func isElementExist(s []int, str int) bool {
	for _, v := range s {
		if v == str {
			return true
		}
	}
	return false
}

func write_code(file *os.File, dates []Date) {
	textWriter := bufio.NewWriter(file)

	code := get_code_template()

	years := get_years(dates)
	code = replace_years(code, years)

	code = replace_solutions(code, dates)

	_, err := textWriter.WriteString(code)

	if err != nil {
		log.Fatal(err)
	}
	textWriter.Flush()
}

func get_code_template() string {
	return `package registration

import (
	"adventofcode/lib"
	<years>
	"fmt"
)

var r = make(map[string]lib.Solution)

func GetSolution(year, day int) lib.Solution {
	return r[fmt.Sprintf("%d/%02d", year, day)]
}

func init() {
<solutions>}
	`
}

func replace_years(code string, years []int) string {
	var yearImports string

	for _, element := range years {
		yearImports += "\"adventofcode/y" + strconv.Itoa(element) + "\""
	}

	return strings.Replace(code, "<years>", yearImports, -1)
}

func replace_solutions(code string, dates []Date) string {
	var result string

	for _, date := range dates {
		year := date.year
		day := date.day
		result += fmt.Sprintf("\tr[\"%d/%02d\"] = &y%d.Solution%02d{}\n", year, day, year, day)
	}

	code = strings.Replace(code, "<solutions>", result, -1)

	return code
}
