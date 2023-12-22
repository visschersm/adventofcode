#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#include "mystring.h"

#define MAX_TOKENS 10
#define MAX_TOKEN_LENGTH 100

char **splitString(const char *input, const char *delimiter, int *count)
{
    printf("[String::splitString] initialize variables.\n");
    char *inputCopy = strdup(input);
    if (inputCopy == NULL)
    {
        // Handle memory allocation failure
        perror("Memory allocation failed");
        exit(EXIT_FAILURE);
    }

    char **result = NULL;
    *count = 0;

    printf("[String::splitString] inputCopy: %s\n", inputCopy);
    printf("[String::splitString] Tokenize the string using strtok\n");
    char *token = strtok(inputCopy, delimiter);
    printf("Token null? %s\n", token);
    while (token != NULL)
    {
        printf("[String::splitString] Allocate memory for the new string\n");
        result = realloc(result, (*count + 1) * sizeof(char *));
        if (result == NULL)
        {
            printf("[String::splitString] Handle memory allocation failure\n");
            perror("Memory allocation failed");
            exit(EXIT_FAILURE);
        }

        printf("[String::splitString] Allocate memory for the token and copy it\n");
        result[*count] = strdup(token);

        printf("[String::splitString] Move to the next token\n");
        token = strtok(NULL, delimiter);
        (*count)++;
    }

    printf("[String::splitString] Free the temporary copy of the input string\n");
    free(inputCopy);

    return result;
}

void freeTokens(char **tokens, int tokenCount)
{
    for (int i = 0; i < tokenCount; ++i)
    {
        free(tokens[i]);
    }
    free(tokens);
}
