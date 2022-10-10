Function Solve {
    Param (
        [Parameter(mandatory)][string][Alias("d")]$date
    )
    Process {
        python .\Solutions\python\adventofcode.python -- $date 
    }
}