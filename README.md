

# Building the CLI tool
- Make sure that you have golang installed by running 
```pwsh
go version
```
- From the root folder: 
```pwsh
go install .\cli\
```
- This will install the adventofcode cli to $GOPATH/bin.
- Now you can run the cli and use it to generate and solve the challenges of the adventofcode.


# Solve a challenge
```pwsh
aoc solve -l <language> -d <date:yyyy/dd>
```

## Bash
> Currently not working. Windows does not support bash (by default)
> **To-Do:** Describe how to build and run this

## C
> Currently not working. THe make files / build does not work.
> **To-Do:** When it builds again, write a description on how to build it.

## clojure
> Currently not working.
> **To-Do:** Describe how to build and run this

## csharp
```pwsh
aoc solve -l csharp -d 2015/01
```
> **To-Do:** Describe how to build and run this
> **To-Do:** The current runner has a lot of logging. Fix this

## elixir
> Currently not working.
> **To-Do:** Describe how to build and run this

## fsharp
> Currently not working.
> **To-Do:** Describe how to build and run this

## go
> **To-Do:** Describe how to build and run this
```pwsh
aoc solve -l go -d 2015/01
```

## haskell
> **To-Do:** Describe how to build and run this
```pwsh
aoc solve -l haskell -d 2015/01
```

## java
> **To-Do:** Describe how to build and run this
```pwsh
aoc solve -l java -d 2015/01
```

## kotlin
> Currently not working.
> **To-Do:** Describe how to build and run this

How it should work:
```pwsh
choco install kotlinc
aoc solve -l kotlin -d 2015/01
```

## lua
> Currently not working.
> **To-Do:** Describe how to build and run this

## php
> Currently not working.
> **To-Do:** Describe how to build and run this

## python
> **To-Do:** Describe how to build and run this
```pwsh
aoc solve -l python -d 2015/01
```

## r
> Currently not working.
> **To-Do:** Describe how to build and run this

## rust
> Currently not working.
> **To-Do:** Describe how to build and run this

## v
> Currently not working.
> **To-Do:** Describe how to build and run this