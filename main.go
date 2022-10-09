package main

import (
	"fmt"
	"os"
	"reflect"
	"regexp"
)

var dateValidation *regexp.Regexp = nil

func main() {
	dateValidation = regexp.MustCompile(`((19|20)\d\d)/(0?[1-9]|1[012])`)

	date := getDate()

	if date == nil {
		fmt.Println("Provide a date for which to generate the solutions in format yyyy/dd")
		return
	}

	fmt.Printf("Provided date: %s", *date)
}

func getDate() *string {
	argumentsAmount := len(os.Args)
	if argumentsAmount <= 1 {
		return nil
	}

	date := os.Args[1]

	if !dateValidation.MatchString(date) {
		fmt.Println("Provided date not valid")
		return nil
	}

	return &date
}

func solveForDate(string date) {
	method := reflect.MethodByName("Solve")
	answer = method.Call()
	fmt.Println(answer)
}
