package languages

import (
	"fmt"
	"log"
	"math/rand"
	"os"
	"time"
)

type Language struct {
	Name         string
	Ext          string
	Commands     []Command
	SourceFolder string
}

func GetAvailableLanguages() []string {
	log.Print("[GetAvailableLanguages]")

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

func GetRandomLanguage() Language {
	log.Print("[GetRandomLanguage]")

	availableLanguages := GetAvailableLanguages()
	rand.Seed(time.Now().UnixNano())
	min := 1
	max := len(availableLanguages)
	randomValue := rand.Intn(max-min) + min
	randomLanguage := availableLanguages[randomValue-1]
	language, err := GetLanguage(randomLanguage)
	if err != nil {
		fmt.Printf("An available language was not supported: %s\n", randomLanguage)
	}
	return *language
}

func GetRegisteredLanguages() []Language {
	result := make([]Language, 0, len(Languages))

	for _, value := range Languages {
		result = append(result, value)
	}

	return result
}

func GetRegisteredLanguageNames() []string {
	var supportedLanguages = GetRegisteredLanguages()
	var result = make([]string, len(supportedLanguages))

	for index, element := range supportedLanguages {
		result[index] = element.Name
	}

	return result
}

func GetLanguage(languageName string) (*Language, error) {
	language, exists := Languages[languageName]

	if !exists {
		return nil, fmt.Errorf("language not added to converter: %s", languageName)
	}

	return &language, nil
}
