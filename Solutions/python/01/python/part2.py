import sys

filename = "input.txt"
floor = 0
charCounter = 0

with open(sys.argv[1], 'r') as input_file:
    for i, line in enumerate(input_file):
        for char in line:
            charCounter += 1
            floor = floor + 1 if char == '(' else floor - 1
            if floor == -1:
                print("Santa found the basement after %d tries" % charCounter)
                break
