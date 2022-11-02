package util

import (
	"fmt"
	"io/fs"
	"log"
	"os"
	"strconv"
	"strings"
)

type Date struct {
	Year, Day int
}

var minYear = 2015
var maxYear = 2022
var minDay = 1
var maxDay = 25

var supportedYears []int
var supportedDays []int

func init() {
	for i := minYear; i <= maxYear; i++ {
		supportedYears = append(supportedYears, i)
	}

	for i := minDay; i <= maxDay; i++ {
		supportedDays = append(supportedDays, i)
	}
}

func (date Date) Format() string {
	return fmt.Sprintf("%d/%02d", date.Year, date.Day)
}

func GetNextDate(languageName string) Date {
	languageFolder := fmt.Sprintf("Solutions/%s", languageName)
	fmt.Printf("Language folder: %s\n", languageFolder)
	folders, err := os.ReadDir(languageFolder)

	if err != nil {
		log.Fatal(err)
	}

	years := GetYears(folders)

	if len(years) == 0 {
		return Date{
			Year: minYear,
			Day:  minDay,
		}
	}

	for _, year := range years {
		yearFolder := fmt.Sprintf("%s/y%d", languageFolder, year)
		days := GetDays(yearFolder)

		if len(days) == 0 {
			return Date{
				Year: year,
				Day:  minDay,
			}
		}

		nextDay := days[len(days)-1] + 1
		if nextDay > maxDay {
			continue
		}

		return Date{
			Year: year,
			Day:  nextDay,
		}
	}

	nextYear := years[len(years)-1] + 1
	if nextYear > maxYear {
		log.Fatal("No valid date found")
	}

	return Date{
		Year: nextYear,
		Day:  1,
	}
}

func GetYears(folders []fs.DirEntry) []int {
	var years []int

	fmt.Println("Getting the year folders already there")

	for _, folder := range folders {
		if !folder.IsDir() {
			continue
		}

		if !strings.HasPrefix(folder.Name(), "y") {
			continue
		}

		year, convErr := strconv.Atoi(folder.Name()[1:])

		if convErr != nil {
			log.Fatal(convErr)
		}

		years = append(years, year)
	}

	return years
}

func GetDays(yearFolderPath string) []int {
	var result []int

	fmt.Printf("Getting the days in %s\n", yearFolderPath)
	solutionFiles, err := os.ReadDir(yearFolderPath)

	if err != nil {
		log.Fatal(err)
	}

	for _, solution := range solutionFiles {
		day, convErr := strconv.Atoi(solution.Name()[8:10])

		if convErr != nil {
			log.Fatal(convErr)
		}

		result = append(result, day)
	}

	return result
}

func GetDate(datestr string) Date {
	splittedDate := strings.Split(datestr, "/")

	year, _ := strconv.Atoi(splittedDate[0])
	day, _ := strconv.Atoi(splittedDate[1])

	return Date{
		Year: year,
		Day:  day,
	}
}
