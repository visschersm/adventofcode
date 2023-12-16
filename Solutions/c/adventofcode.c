#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include "Solution.h"

void solve_solution01_y2023_part1()
{
    printf("Part1 not yet implemented.\n");
}

void solve_solution01_y2023_part2()
{
    printf("Part2 not yet implemented.\n");
}

void solve_solution01_y2023()
{
    solve_solution01_y2023_part1();
    solve_solution01_y2023_part2();
}

int solutionName = 202301;

#define MAX_TOKENS 10
#define MAX_TOKEN_LENGTH 100

char **splitString(const char *input, char delimiter, int *tokenCount)
{
    char **tokens = (char **)malloc(MAX_TOKENS * sizeof(char *));
    if (tokens == NULL)
    {
        fprintf(stderr, "Memory allocation error.\n");
        exit(EXIT_FAILURE);
    }

    *tokenCount = 0;
    char *token;
    char *context = NULL;

    token = strtok_s((char *)input, &delimiter, &context);

    while (token != NULL && *tokenCount < MAX_TOKENS)
    {
        tokens[*tokenCount] = (char *)malloc((strlen(token) + 1) * sizeof(char));
        if (tokens[*tokenCount] == NULL)
        {
            fprintf(stderr, "Memory allocation error.\n");
            exit(EXIT_FAILURE);
        }

        if (strcpy_s(tokens[*tokenCount], MAX_TOKEN_LENGTH, token) != 0)
        {
            fprintf(stderr, "String copying error.\n");
            exit(EXIT_FAILURE);
        };

        (*tokenCount)++;

        token = strtok_s(NULL, &delimiter, &context);
    }

    return tokens;
}

void freeTokens(char **tokens, int tokenCount)
{
    for (int i = 0; i < tokenCount; ++i)
    {
        free(tokens[i]);
    }
    free(tokens);
}

int getDateFromArgs(int argc, char *argv[])
{
    int result = 0;
    if (argc > 1)
    {
        int tokenCount;
        char **tokens = splitString(argv[1], '/', &tokenCount);

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
    }
    else
    {
        printf("No date provided.\n");
    }

    return result;
}

typedef void (*Solution)();

Solution getSolutionForDate(int date)
{
    switch (date)
    {
    case 202301:
        return solve_solution01_y2023;
    }

    return NULL;
}

int main(int argc, char *argv[])
{
    printf("Program name: %s\n", argv[0]);

    int date = getDateFromArgs(argc, argv);

    printf("Dated parsed: %d\n", date);

    Solution solution = getSolutionForDate(date);

    if (solution == NULL)
    {
        fprintf(stderr, "Could not find solution for date: %d\n", date);
        exit(EXIT_FAILURE);
    }
    solution();

    return 0;
}
