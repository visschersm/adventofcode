package y2015

import (
	"bufio"
	"fmt"
	"io/ioutil"
	"os"
)

type Solution01 struct{}

func (s *Solution01) Part1(filename string) {
	data, err := ioutil.ReadFile(filename)

	if err != nil {
		fmt.Println("Error encountered")
		return
	}

	dict := make(map[byte]int)

	for _, c := range data {
		dict[c] = dict[c] + 1
	}

	fmt.Printf("Santa is on the %dth floor.\n", dict['(']-dict[')'])
}

func (s *Solution01) Part2(filename string) {
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

	fmt.Printf("Santa found the basement after %d tries\n", tryCounter)

	file.Close()
}
