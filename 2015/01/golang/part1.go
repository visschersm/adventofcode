package main

import (
	"bufio"
	"fmt"
	"os"
)

func main() {
	filename := os.Args[1]
	file, err := os.Open(filename)

	if err != nil {
		fmt.Println("Error encountered")
		return
	}

	fileScanner := bufio.NewScanner(file)
	fileScanner.Split(bufio.ScanLines)

	var floor int

	for fileScanner.Scan() {
		line := fileScanner.Text()
		for i := 0; i < len(line); i++ {
			if line[i] == '(' {
				floor += 1
			} else {
				floor -= 1
			}
		}
	}

	fmt.Printf("Santa is on the %dth floor.\n", floor)

	file.Close()
}
