echo "Solve now"

Function Solve {
    Param (
        [Parameter(mandatory)][string][Alias("d")]$date,
        [string][Alias("i")]$input_file
    )
    Process {
        $workdir = Get-Location
        Write-Host "Current directory: $workdir"
        Set-Location "Solutions/go"
        $curDir = Get-Location
        Write-Host "Current directory: $curDir"
        go generate
        Set-Location $workdir
        $curDir = Get-Location
        Write-Host "Current directory: $curDir"

        if($input_file) 
        { 
            go run .\Solutions\go\main.go -date $date -input_file $input_file
        }
        else 
        {
            # go run .\Solutions\go\main.go -date $date
            go run adventofcode -date $date
        }
    }
}