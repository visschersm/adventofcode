param (
    [Parameter(mandatory)][string][Alias("l")]$language, 
    [Parameter(mandatory)][string][Alias("d")]$date
    )


enum SupportedLanguages
{
    csharp
    python
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

Write-Host "Solve for $date in `"$language`", call `"solve_$language.ps1`""
. scripts\solve_$language.ps1
Solve -d $date
