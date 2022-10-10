import sys

def part1(input_file):
    floor = 0
    for c in read_by_character(input_file):
        floor += 1 if c == '(' else -1

    print("Santa is on the %dth floor" % floor)

def part2(input_file):
    floor = 0
    charCounter = 0

    for c in read_by_character(input_file):
        charCounter += 1
        floor += 1 if c == '(' else - 1
        if floor == -1:
            break
    
    print("Santa found the basement after %d tries" % charCounter)

def read_by_character(input_file):
    for i, line in enumerate(input_file):
        for char in line:
            yield char

def solve(filename):
    with open(filename, 'r') as input_file:
        print("Part1:")
        part1(input_file)
    
        input_file.seek(0,0)
        print("Part2:")
        part2(input_file)

