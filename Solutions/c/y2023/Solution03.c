#include <stdio.h>
#include "../Solution.h"

void part1()
{
    // Replace "example.txt" with the path to your file
    const char *filename = "Inputs/2023/03-test01.txt";

    // Open the file for reading
    FILE *file = fopen(filename, "r");
    if (file == NULL)
    {
        perror("Error opening file");
        return 1;
    }

    // Read and print the contents of the file
    char buffer[1024];
    size_t bytesRead;
    while ((bytesRead = fread(buffer, 1, sizeof(buffer), file)) > 0)
    {
        fwrite(buffer, 1, bytesRead, stdout);
    }

    // .....
    // .123.
    // .....
    // Close the file
    fclose(file);

    return 0;
}

void part2()
{
    printf("Part2 not yet implemented.\n");
}
