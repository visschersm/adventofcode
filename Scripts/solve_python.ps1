Function Solve {
    Param (
        [Parameter(mandatory)][string][Alias("d")]$date,
        [string][Alias("i")]$input_file
    )
    Process {
        if($input_file) 
        { 
            python .\Solutions\python\adventofcode.py --date $date --input_file $input_file
        }
        else 
        {
            python .\Solutions\python\adventofcode.py --date $date
        }
    }
}