#include <stdio.h>
#include <stdlib.h>

#include "Solution.h"
#include "date.h"

int main(int argc, char *argv[])
{
#ifdef MY_VARIABLE
    const char *my_variable_value = MY_VARIABLE;
    printf("MY_VARIABLE is defined with value: %s\n", MY_VARIABLE);
    printf("hello date:\n");
    Date date = parseDate(my_variable_value);
    printf("Date: %d/%d\n", date.day, date.year);
#else
    printf("MY_VARIABLE is not defined\n");
#endif

    part1();
    part2();

    return 0;
}
