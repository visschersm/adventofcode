#include <stdio.h>
#include <stdlib.h>
#include "Solution.h"

void part1(const struct Solution *obj)
{
    printf("Part1 not yet implemented.\n");
}

void part2(const struct Solution *obj)
{
    printf("Part2 not yet implemented.\n");
}

struct Solution *createSolution()
{
    struct Solution *obj = (struct Solution *)malloc(sizeof(struct Solution));
    if (obj != NULL)
    {
        obj->part1 = part1;
        obj->part2 = part2;
    }
    return obj;
}