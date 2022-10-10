import sys

def part1(filename):
    floor = 0

    with open(filename, 'r') as input_file:
        for i, line in enumerate(input_file):
            for char in line:
                print("Char found:", char)
                print("Current floor:", floor)
                floor = floor + 1 if char == '(' else floor - 1

    print("Santa is on the %dth floor" % floor)

def part2(filename):
    floor = 0
    charCounter = 0

    with open(filename, 'r') as input_file:
        for i, line in enumerate(input_file):
            for char in line:
                charCounter += 1
                floor = floor + 1 if char == '(' else floor - 1
                if floor == -1:
                    print("Santa found the basement after %d tries" % charCounter)
                    break

def solve(filename):
    print("Part1:")
    part1(filename)
    
    print("Part2:")
    part2(filename)

