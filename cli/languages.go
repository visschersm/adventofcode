package main

import (
	"log"
	"os"
)

type Language struct {
	name string
	ext  string
}

func ConvertLanguage(languageName string) Language {
	switch languageName {

	case "csharp":
		return Language{
			name: "csharp",
			ext:  ".cs",
		}

	default:
		log.Fatal("language not supported: ", languageName)
	}

	return Language{}
}

func get_available_languages() []string {
	var result []string

	folders, err := os.ReadDir("Solutions")

	if err != nil {
		log.Fatal(err)
		return result
	}

	for _, folder := range folders {
		result = append(result, folder.Name())
	}

	return result
}
