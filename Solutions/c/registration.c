#include <stdlib.h>
#include "Solution01.h"

Solution *createSolution(int date)
{
    Solution *solution = NULL;

    switch (date)
    {
    case 202301:
        solution = (Solution *)malloc(sizeof(Solution01));
        createSolution01((Solution01 *)solution);
    default:
        break;
    }

    return solution;
}

void createSolution01(Solution01 *this)
{
    this->interface.part1 = year2023Solution01Part1;
    this->interface.part2 = year2023Solution01Part2;
}