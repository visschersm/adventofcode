#ifndef SOLUTION_H
#define SOLUTION_H

typedef struct Solution Solution;

struct Solution
{
    void (*part1)(Solution *this);
    void (*part2)(Solution *this);
};

#endif