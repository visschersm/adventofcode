Function Solve {
    Param (
        [Parameter(mandatory)][string][Alias("d")]$date
    )
    Process {
        dotnet run --project .\Solutions\csharp\adventofcode.csproj -- $date 
    }
}