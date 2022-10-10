
def part1(filename):
    file = open(filename, "r")

    increased = 0
    firstLine = file.readline()
    previousValue = int(firstLine)

    lines = file.readlines()
    for line in lines:
        lineValue = int(line)
        if lineValue > previousValue:
            increased = increased + 1
        previousValue = lineValue

    print(increased)

def solve(filename):
    print("Part1:")
    part1(filename)
    print("Part2:")
    print("No answer yet")