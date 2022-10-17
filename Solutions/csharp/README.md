# Adding a new solution
Chose the year for which you want to add a solution.
Add a csharp code file with the name: 'Solutiondd.cs' where the 'dd' is replaced with the day for which you want to add the solution.

In the code file include the following:

```
    using AdventOfCode;

    namespace Solutions.yYYYYdDD;

    [Solution(YYYY, DD)]
    public class SolutionDD
    {
        [Part1]
        public void Part1(string filename)
        {
            // Here you will write part1 of the solution
        }

        [Part2]
        public void Part2(string filename)
        {
            // Here you will write part2 of the solution
        }
    }
```

In the code file replace YYYY with the year and DD with the day for which the solution is.