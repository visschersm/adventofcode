package runners

import "aoc_cli/util"

var r = make(map[string]Runner)

func GetRunner(language util.Language) Runner {
	return r[language.Name]
}

func GetRegisteredLanguages() []util.Language {
	var result = make([]util.Language, len(r))

	index := 0
	for languageName := range r {
		result[index] = util.ConvertLanguage(languageName)
		index++
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

func init() {
	r["csharp"] = &CSharpRunner{}
	r["java"] = &JavaRunner{}
	r["kotlin"] = &KotlinRunner{}
	r["php"] = &PHPRunner{}
}
