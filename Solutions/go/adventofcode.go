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
		fmt.Println("Provide a date for which to generate the solutions: -date yyyy/dd")
		return
	}

	inputFile := getInputFile(*date)

	solveForDate(*date, inputFile)
}

func getInputFile(date string) string {
	inputFile := flag.String("inputFile", "", "")
	flag.Parse()

	if inputFile == nil || *inputFile == "" {
		*inputFile = fmt.Sprintf("Inputs/%d/%02d.txt", getYear(date), getDay(date))
	}

	return *inputFile
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
	s := registration.GetSolution(getYear(date), getDay(date))
	solve(s, filename)
}

func getYear(date string) int {
	splittedDate := strings.Split(date, "/")
	year, _ := strconv.Atoi(splittedDate[0])
	return year
}

func getDay(date string) int {
	splittedDate := strings.Split(date, "/")
	day, _ := strconv.Atoi(splittedDate[1])
	return day
}
