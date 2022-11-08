package languages

import (
	"os/exec"
)

var Languages = map[string]Language{
	"bash": {
		Name: "bash",
		Ext:  ".sh",
		Commands: []Command{
			{
				Print: true,
				Command: exec.Command(
					"bash",
					"Solutions/bash/AdventOfCode.sh",
					"-d",
					"<date>"),
			},
		},
	},
	"c": {
		Name: "c",
		Ext:  ".c",
		Commands: []Command{
			{
				Print:   true,
				Command: exec.Command("echo", "c not yet implemented"),
			},
		},
	},
	"carbon": {
		Name: "carbon",
		Commands: []Command{
			{
				Print:   true,
				Command: exec.Command("echo", "carbon not yet implemented"),
			},
		},
	},
	"clojure": {
		Name:         "clojure",
		Ext:          ".clj",
		Commands: []Command{
			{
				Print: true,
				Command: exec.Command(
					"powershell",
					"Solutions/clojure/run.ps1",
					"<date>",
					"<input_file>"),
			},
		},
	},
	"cpp": {
		Name: "cpp",
		Ext:  ".cpp",
		Commands: []Command{
			{
				Print:   true,
				Command: exec.Command("echo", "cpp not yet implemented"),
			},
		},
	},
	"csharp": {
		Name: "csharp",
		Ext:  ".cs",
		Commands: []Command{
			{
				Print: true,
				Command: exec.Command(
					"dotnet",
					"run",
					"--project",
					"Solutions/csharp/adventofcode.csproj",
					"<date>",
					"<input_file>"),
			},
		},
	},
	"dart": {
		Name: "dart",
		Ext:  ".dart",
		Commands: []Command{
			{
				Print:   true,
				Command: exec.Command("echo", "dart not yet implemented"),
			},
		},
	},
	"fsharp": {
		Name: "fsharp",
		Ext:  ".fs",
		Commands: []Command{
			{
				Print:   true,
				Command: exec.Command("echo", "fsharp not yet implemented"),
			},
		},
	},
	"go": {
		Name: "go",
		Ext:  ".go",
		Commands: []Command{
			{
				Print:   false,
				Command: exec.Command("go", "generate", `.\Solutions\go\adventofcode.go`),
			},
			{
				Print: true,
				Command: exec.Command("go",
					"run",
					`.\Solutions\go\adventofcode.go`,
					"-date",
					"<date>"),
			},
		},
	},
	"haskell": {
		Name: "haskell",
		Ext:  ".hs",
		Commands: []Command{
			{
				Print:   true,
				Command: exec.Command("runhaskell", "Solutions/haskell/AdventOfCode.hs", "<date>"),
			},
		},
	},
	"java": {
		Name: "java",
		Ext:  ".java",
		Commands: []Command{
			{
				Print:   false,
				Command: exec.Command("CMD", "/C", `cli\runners\java_runner.bat`),
			},
			{
				Print: true,
				Command: exec.Command(
					"java",
					"-cp",
					".",
					"Solutions/java/AdventOfCode",
					"<date>"),
			},
		},
	},
	"javascript": {
		Name: "javascript",
		Ext:  ".js",
		Commands: []Command{
			{
				Print:   true,
				Command: exec.Command("echo", "javascript not yet implemented"),
			},
		},
	},
	"kotlin": {
		Name: "kotlin",
		Ext:  ".kt",
		Commands: []Command{
			{
				Print: false,
				Command: exec.Command(
					"kotlinc",
					"-cp",
					".",
					"-d",
					"Solutions/kotlin/build",
					"Solutions/kotlin"),
			},
			{
				Print: true,
				Command: exec.Command(
					"kotlin",
					"-cp",
					"Solutions/kotlin/build",
					"Solutions.kotlin.AdventOfCodeKt",
					"<date>"),
			},
		},
	},
	"lua": {
		Name: "lua",
		Ext:  ".lua",
		Commands: []Command{
			{
				Print:   true,
				Command: exec.Command("lua", "Solutions/lua/AdventOfCode.lua", "<date>"),
			},
		},
	},
	"matlab": {
		Name: "matlab",
		Commands: []Command{
			{
				Print:   true,
				Command: exec.Command("echo", "matlab not yet implemented"),
			},
		},
	},
	"perl": {
		Name: "perl",
		Commands: []Command{
			{
				Print:   true,
				Command: exec.Command("echo", "perl not yet implemented"),
			},
		},
	},
	"php": {
		Name: "php",
		Ext:  ".php",
		Commands: []Command{
			{
				Print: true,
				Command: exec.Command(
					"php",
					"Solutions/php/adventofcode.php",
					"-d",
					"<date>"),
			},
		},
	},
	"powershell": {
		Name: "powershell",
		Ext:  ".ps1",
		Commands: []Command{
			{
				Print:   true,
				Command: exec.Command("echo", "powershell not yet implemented"),
			},
		},
	},
	"python": {
		Name: "python",
		Ext:  ".py",
		Commands: []Command{
			{
				Print: true,
				Command: exec.Command(
					"python",
					`.\Solutions\python\adventofcode.py`,
					"--date",
					"<date>"),
			},
		},
	},
	"r": {
		Name: "r",
		Ext:  ".r",
		Commands: []Command{
			{
				Print:   true,
				Command: exec.Command("rscript", "Solutions/r/AdventOfCode.r", "<date>"),
			},
		},
	},
	"ruby": {
		Name: "ruby",
		Commands: []Command{
			{
				Print:   true,
				Command: exec.Command("echo", "ruby not yet implemented"),
			},
		},
	},
	"rust": {
		Name: "rust",
		Commands: []Command{
			{
				Print:   true,
				Command: exec.Command("echo", "rust not yet implemented"),
			},
		},
	},
	"scala": {
		Name: "scala",
		Commands: []Command{
			{
				Print:   true,
				Command: exec.Command("echo", "scala not yet implemented"),
			},
		},
	},
	"sql": {
		Name: "sql",
		Ext:  ".sql",
		Commands: []Command{
			{
				Print:   true,
				Command: exec.Command("echo", "sql not yet implemented"),
			},
		},
	},
	"swift": {
		Name: "swift",
		Ext:  ".swift",
		Commands: []Command{
			{
				Print:   true,
				Command: exec.Command("echo", "cswiftnot yet implemented"),
			},
		},
	},
	"typescript": {
		Name: "typescript",
		Ext:  ".ts",
		Commands: []Command{
			{
				Print:   true,
				Command: exec.Command("echo", "typescript not yet implemented"),
			},
		},
	},
	"v": {
		Name: "v",
		Ext:  ".v",
		Commands: []Command{
			{
				Print: true,
				Command: exec.Command(
					"v",
					"run",
					"Solutions/v",
					"<date>"),
			},
		},
	},
	"vb": {
		Name: "visualbasic",
		Ext:  ".vb",
		Commands: []Command{
			{
				Print:   true,
				Command: exec.Command("echo", "visual basic not yet implemented"),
			},
		},
	},
	"zig": {
		Name: "zig",
		Commands: []Command{
			{
				Print:   true,
				Command: exec.Command("echo", "zig not yet implemented"),
			},
		},
	},
}
