#include <stdio.h>
#include <stdlib.h>

#include "date.h"
#include "mystring.h"

Date parseDate(const char *dateString)
{
    printf("[Date] Parsing: %s\n", dateString);
    int tokenCount;
    char **tokens = splitString(dateString, "/", &tokenCount);

    printf("[Date] splitted string. tokencount: %d\n", tokenCount);

    if (tokenCount != 2)
    {
        fprintf(stderr, "Could not parse date");
        exit(EXIT_FAILURE);
    }

    char *substring = malloc(4);
    strncpy(substring, tokens[0] + 1, 4);
    int year = atoi(substring);
    free(substring);

    substring = malloc(2);
    strncpy(substring, tokens[1] + 8, 2);
    int day = atoi(substring);
    free(substring);

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

    Date date;
    date.day = day;
    date.year = year;

    freeTokens(tokens, tokenCount);

    return date;
}