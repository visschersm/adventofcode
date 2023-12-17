#ifndef MY_STRING_H
#define MY_STRING_H

char **splitString(const char *input, char delimiter, int *tokenCount);
void freeTokens(char **tokens, int tokenCount);

#endif