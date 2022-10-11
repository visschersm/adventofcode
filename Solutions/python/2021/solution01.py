from util.filereader import *

def part1(input_file):
    increased = 0
    lines = [int(numeric_string) for numeric_string in input_file.readlines()]

    for index, line in enumerate(lines[1:]):
        increased += 1 if line > lines[index] else 0

    print(increased)

def part2(input_file):
    increased = 0
    lines = [int(numeric_string) for numeric_string in input_file.readlines()]
    
    for index in range(0, len(lines) - 3):
        increased += 1 if sum(lines[index + 1:index + 4]) > sum(lines[index:index+3]) else 0

    print(increased)

def solve(filename):
    print("Part1:")
    part1(filename)
    print("Part2:")
    print("No answer yet")