package adventofcode

import (
	"fmt"
	"io/ioutil"
	"os"
)

func Solve() {
	filename := os.Args[1]
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
