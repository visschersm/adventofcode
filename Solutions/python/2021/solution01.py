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

def part2(filename):
    increased = 0
    previous = 0

    for i = 0, i < len(lines); i += 1:
        current = sum(lines[i..i + 3])
        increased += 1 if current > previous
        previous = current

    print(increased)

def solve(filename):
    print("Part1:")
    part1(filename)
    print("Part2:")
    print("No answer yet")