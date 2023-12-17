#include <stdlib.h>
#include <stdio.h>

#include "mystring.h"

int getDateFromArgs(char *dateString)
{
    int result = 0;

    int tokenCount;
    char **tokens = splitString(dateString, '/', &tokenCount);

    if (tokenCount != 2)
    {
        fprintf(stderr, "Could not parse date");
        exit(EXIT_FAILURE);
    }

    int year = atoi(tokens[0]);
    int day = atoi(tokens[1]);

    if (year == 0)
    {
        fprintf(stderr, "Invalid number: %s\n", tokens[0]);
        exit(EXIT_FAILURE);
    }

    if (day == 0)
    {
        fprintf(stderr, "Invalid number: %s\n", tokens[1]);
        exit(EXIT_FAILURE);
    }

    char buffer[7];
    int snprintfResult = snprintf(buffer, sizeof(buffer), "%d%0*d", year, 2, day);
    if (snprintfResult < 0 || snprintfResult >= sizeof(buffer))
    {
        fprintf(stderr, "Error formatting number into a zero-padded string.\n");
        exit(EXIT_FAILURE);
    }

    result = atoi(buffer);

    freeTokens(tokens, tokenCount);

    return result;
}