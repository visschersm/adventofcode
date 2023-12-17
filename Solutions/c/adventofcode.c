#include <stdio.h>
#include <stdlib.h>

#include "date.h"
#include "Solution.h"
#include "Solution01.h"
#include "registration.h"

void solve(Solution *solution);

int main(int argc, char *argv[])
{
    if (argc <= 1)
    {
        fprintf(stderr, "No date provided.\n");
        exit(EXIT_FAILURE);
    }

    int date = getDateFromArgs(argv[1]);

    Solution *solution = createSolution(date);

    if (solution == NULL)
    {
        fprintf(stderr, "Could not find solution for date: %d\n", date);
        exit(EXIT_FAILURE);
    }

    solve(solution);

    return 0;
}

void solve(Solution *solution)
{
    solution->part1(solution);
    solution->part2(solution);
}
