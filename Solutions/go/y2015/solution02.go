package y2015

import (
	"bufio"
	"fmt"
	"os"
	"sort"
	"strconv"
	"strings"
)

type Solution02 struct{}

func (s *Solution02) Part1(filename string) {
	file, err := os.Open(filename)

	if err != nil {
		fmt.Println("Error encountered")
		return
	}

	fileScanner := bufio.NewScanner(file)
	fileScanner.Split(bufio.ScanLines)

	var result int

	for fileScanner.Scan() {
		line := fileScanner.Text()

		dimensions := strings.Split(line, "x")
		length, _ := strconv.Atoi(dimensions[0])
		width, _ := strconv.Atoi(dimensions[1])
		height, _ := strconv.Atoi(dimensions[2])

		gift := Gift{
			l: length,
			w: width,
			h: height,
		}

		result += gift.surface() + gift.smallest_side()
	}

	fmt.Printf("The elves need %d sqare feet of wrapping paper\n", result)
}

func (s *Solution02) Part2(filename string) {
	file, err := os.Open(filename)

	if err != nil {
		fmt.Println("Error encountered")
		return
	}

	fileScanner := bufio.NewScanner(file)
	fileScanner.Split(bufio.ScanLines)

	var result int

	for fileScanner.Scan() {
		line := fileScanner.Text()

		dimensions := strings.Split(line, "x")
		length, _ := strconv.Atoi(dimensions[0])
		width, _ := strconv.Atoi(dimensions[1])
		height, _ := strconv.Atoi(dimensions[2])

		gift := Gift{
			l: length,
			w: width,
			h: height,
		}

		sides := []int{gift.l, gift.w, gift.h}
		sort.Ints(sides)

		result += gift.cubed() + sides[0]*2 + sides[1]*2
	}

	fmt.Printf("The elves need %d feet of ribbon\n", result)
}

type Gift struct {
	l, w, h int
}

func (gift Gift) surface() int {
	return 2*gift.l*gift.w + 2*gift.w*gift.h + 2*gift.h*gift.l
}

func (gift Gift) smallest_side() int {
	return min(min(gift.l*gift.w, gift.w*gift.h), gift.h*gift.l)
}

func (gift Gift) cubed() int {
	return gift.l * gift.w * gift.h
}

func min(a, b int) int {
	if a < b {
		return a
	}
	return b
}
