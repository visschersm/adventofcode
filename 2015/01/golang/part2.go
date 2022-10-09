package adventofcode

import (
	"bufio"
	"fmt"
	"os"
)

func Solve() {
	filename := os.Args[1]
	file, err := os.Open(filename)

	if err != nil {
		fmt.Println("Error encountered")
		return
	}

	fileScanner := bufio.NewScanner(file)
	fileScanner.Split(bufio.ScanLines)

	var floor int
	var tryCounter int

	for fileScanner.Scan() {
		line := fileScanner.Text()
		for i := 0; i < len(line); i++ {
			tryCounter++

			if line[i] == '(' {
				floor += 1
			} else {
				floor -= 1
			}

			if floor == -1 {
				break
			}
		}
	}

	fmt.Printf("Santa found the basement after %d tries", tryCounter)

	file.Close()
}
