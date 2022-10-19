from util.filereader import *

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

