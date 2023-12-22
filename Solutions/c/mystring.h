#ifndef MY_STRING_H
#define MY_STRING_H

char **splitString(const char *input, const char *delimiter, int *count);
void freeTokens(char **tokens, int count);

#endif
