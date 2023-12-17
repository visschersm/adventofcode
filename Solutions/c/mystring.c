#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#include "mystring.h"

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