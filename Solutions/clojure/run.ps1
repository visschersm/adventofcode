$oldDir = Get-Location
Set-Location .\Solutions\clojure
clojure -M -m AdventOfCode
Set-Location $oldDir