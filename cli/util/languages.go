package util

import (
	"log"
	"math/rand"
	"os"
	"time"
)

type Language struct {
	Name string
	Ext  string
}

func ConvertLanguage(languageName string) Language {
	switch languageName {
	case "bash":
		return Language{
			Name: "bash",
			Ext:  ".sh",
		}

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

	case "java":
		return Language{
			Name: "java",
			Ext:  ".java",
		}

	case "kotlin":
		return Language{
			Name: "kotlin",
			Ext:  ".kt",
		}

	case "php":
		return Language{
			Name: "php",
			Ext:  ".php",
		}

	case "python":
		return Language{
			Name: "python",
			Ext:  ".py",
		}

	case "v":
		return Language{
			Name: "v",
			Ext:  ".v",
		}

	default:
		log.Fatal("language not added to converter: ", languageName)
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

func RandomLanguage() Language {
	availableLanguages := GetAvailableLanguages()
	rand.Seed(time.Now().UnixNano())
	min := 1
	max := len(availableLanguages)
	randomValue := rand.Intn(max-min) + min
	return ConvertLanguage(availableLanguages[randomValue-1])
}
