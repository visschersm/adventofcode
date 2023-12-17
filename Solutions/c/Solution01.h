#ifndef SOLUTION01_H
#define SOLUTION01_H

#include "Solution.h"

typedef struct Solution01 Solution01;

struct Solution01
{
    Solution interface;
};

void createSolution01(Solution01 *this);

void year2023Solution01Part1(Solution *this);
void year2023Solution01Part2(Solution *this);

#endif