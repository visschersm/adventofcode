package main

import (
	"adventofcode/lib"
	"adventofcode/registration"
	"flag"
	"fmt"
	"regexp"
	"strconv"
	"strings"
)

//go:generate go run gen/gen.go
func main() {
	date := getDate()

	if date == nil {
		fmt.Println("Provide a date for which to generate the solutions in format yyyy/dd")
		return
	}

	input_file := getInputFile(*date)

	solveForDate(*date, input_file)
}

func getInputFile(date string) string {
	input_file := flag.String("input_file", "", "")
	flag.Parse()

	if input_file == nil || *input_file == "" {
		fmt.Println("No input file provided, use default")
		*input_file = fmt.Sprintf("Inputs/%d/%02d.txt", get_year(date), get_day(date))
	}

	fmt.Printf("Input file: %s\n", *input_file)

	return *input_file
}

func getDate() *string {
	date := flag.String("date", "", "")
	flag.Parse()

	if date == nil {
		fmt.Println("No date provided")
		return nil
	}

	dateValidation := regexp.MustCompile(`((19|20)\d\d)/(0?[1-9]|1[012])`)

	if !dateValidation.MatchString(*date) {
		fmt.Println("Provided date not valid")
		return nil
	}

	return date
}

func solve(s lib.Solution, filename string) {
	s.Part1(filename)
	s.Part2(filename)
}

func solveForDate(date string, filename string) {
	s := registration.GetSolution(get_year(date), get_day(date))
	solve(s, filename)
}

func get_year(date string) int {
	splittedDate := strings.Split(date, "/")
	year, _ := strconv.Atoi(splittedDate[0])
	fmt.Printf("year: %d\n", year)
	return year
}

func get_day(date string) int {
	splittedDate := strings.Split(date, "/")
	day, _ := strconv.Atoi(splittedDate[1])
	fmt.Printf("day: %d\n", day)
	return day
}
