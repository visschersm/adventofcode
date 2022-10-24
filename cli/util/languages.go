package util

import (
	"log"
	"os"
)

type Language struct {
	Name string
	Ext  string
}

func ConvertLanguage(languageName string) Language {
	switch languageName {

	case "csharp":
		return Language{
			Name: "csharp",
			Ext:  ".cs",
		}

	case "go":
		return Language{
			Name: "go",
			Ext:  ".go",
		}

	case "php":
		return Language{
			Name: "php",
			Ext:  ".php",
		}

	case "kotlin":
		return Language{
			Name: "kotlin",
			Ext:  ".kt",
		}

	case "java":
		return Language{
			Name: "java",
			Ext:  "java",
		}

	default:
		log.Fatal("language not supported: ", languageName)
	}

	return Language{}
}

func GetAvailableLanguages() []string {
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
