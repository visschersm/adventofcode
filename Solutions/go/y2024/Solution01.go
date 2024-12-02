package y2024

import (
	"bufio"
	"fmt"
	"math"
	"os"
	"strconv"
	"strings"
)

type Solution01 struct{}

func (s *Solution01) Part1(filename string) {
	file, err := os.Open(filename)

	if err != nil {
		fmt.Println("Error encountered reading file")
		return
	}

	fileScanner := bufio.NewScanner(file)
	fileScanner.Split(bufio.ScanLines)

	var fileLines []string

	for fileScanner.Scan() {
		fileLines = append(fileLines, fileScanner.Text())
	}

	file.Close()

	firstList := []int{}
	secondList := []int{}

	for _, line := range fileLines {
		numbers := strings.Split(line, "   ")
		firstNumber, _ := strconv.Atoi(numbers[0])
		secondNumber, _ := strconv.Atoi(numbers[1])

		firstList = append(firstList, firstNumber)
		secondList = append(secondList, secondNumber)
	}

	var result int = 0

	for len(firstList) >= 1 {
		var firstNumber int = math.MaxInt32
		var firstNumberIndex int = 0
		var secondNumber int = math.MaxInt32
		var secondNumberIndex int = 0

		for i := 0; i < len(firstList); i++ {
			if firstList[i] < firstNumber {
				firstNumber = firstList[i]
				firstNumberIndex = i
			}
			if secondList[i] < secondNumber {
				secondNumber = secondList[i]
				secondNumberIndex = i
			}
		}

		// fmt.Printf("FirstNumber: %d, SecondNumber: %d\n", firstNumber, secondNumber)

		var delta = firstNumber - secondNumber
		if delta < 0 {
			delta = -delta
		}
		result += delta
		firstList = append(firstList[:firstNumberIndex], firstList[firstNumberIndex+1:]...)
		secondList = append(secondList[:secondNumberIndex], secondList[secondNumberIndex+1:]...)

	}

	fmt.Printf("Total delta: %d\n", result)
}

func (s *Solution01) Part2(filename string) {
	file, err := os.Open(filename)

	if err != nil {
		fmt.Println("Error encountered reading file")
		return
	}

	fileScanner := bufio.NewScanner(file)
	fileScanner.Split(bufio.ScanLines)

	var fileLines []string

	for fileScanner.Scan() {
		fileLines = append(fileLines, fileScanner.Text())
	}

	file.Close()

	firstList := []int{}
	secondList := []int{}

	for _, line := range fileLines {
		numbers := strings.Split(line, "   ")
		firstNumber, _ := strconv.Atoi(numbers[0])
		secondNumber, _ := strconv.Atoi(numbers[1])

		firstList = append(firstList, firstNumber)
		secondList = append(secondList, secondNumber)
	}

	result := 0

	for _, firstNumber := range firstList {
		var counter = 0
		for i := 0; i < len(secondList); i++ {
			if firstNumber == secondList[i] {
				counter++
			}
		}

		result += firstNumber * counter
	}

	fmt.Printf("Total encounter score: %d\n", result)
}
