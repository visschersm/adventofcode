Function Solve {
    Param (
        [Parameter(mandatory)][string][Alias("d")]$date,
        [string][Alias("i")]$input_file
    )
    Process {
        if($input_file)
        {
            dotnet run --project .\Solutions\csharp\adventofcode.csproj -- $date $input_file
        }
        else 
        {
            dotnet run --project .\Solutions\csharp\adventofcode.csproj -- $date 
        }
    }
}