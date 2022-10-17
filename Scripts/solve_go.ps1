Function Solve {
    Param (
        [Parameter(mandatory)][string][Alias("d")]$date,
        [string][Alias("i")]$input_file
    )
    Process {
        $workdir = Get-Location
        Set-Location "Solutions/go"
        go generate
        Set-Location $workdir

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