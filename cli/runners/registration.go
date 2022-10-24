package runners

import "aoc_cli/util"

var r = make(map[string]Runner)

func GetRunner(language util.Language) Runner {
	return r[language.Name]
}

func init() {
	r["csharp"] = &CSharpRunner{}
	r["java"] = &JavaRunner{}
	r["kotlin"] = &KotlinRunner{}
	r["php"] = &PHPRunner{}
}
