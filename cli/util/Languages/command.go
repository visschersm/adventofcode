package languages

import "os/exec"

type Command struct {
	Print   bool
	Command *exec.Cmd
}