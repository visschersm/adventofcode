param (
    [Parameter(mandatory)][string][Alias("l")]$language, 
    [Parameter(mandatory)][string][Alias("d")]$date,
    [string][Alias("i")]$input_file
    )


enum SupportedLanguages
{
    csharp
    python
}

enum NotSupportedLanguages
{
    go
    rust
    fsharp
    javascript
    c
    java
}

if ([enum]::isDefined(([SupportedLanguages]), $language)) 
{
    Write-Host "Language `"$language`" is supported"
}

if ([enum]::isDefined(([NotSupportedLanguages]), $language)) 
{
    Write-Host "Language `"$language`" is not (yet) supported"
    return
}

Write-Host "Solve for $date in `"$language`", call `"solve_$language.ps1`""
. scripts\solve_$language.ps1
Solve -d $date -i $input_file
