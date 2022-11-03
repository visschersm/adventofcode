# To run haskell 
From root:
```bash
runhaskell Solutions/haskell/AdventOfCode.hs <date>
```

# To add solution manually
## Add solution file
Add file to `Solutions/haskell/y<year>` folder
with name: `Solution<day>.hs`
full path would be: `Solutions/haskell/y<year>/Solution<day>.hs`

## Register solution
In the `Registration.hs` add import
`import Solutions.Haskell.Y<year>.Solution<day>`

Add to solutions map:
solutions = Map.fromList [
    ...
    ("y<year>/Solution<day>, Solutions.Haskell.Y<year>.Solution<day>.solution<day>)
]