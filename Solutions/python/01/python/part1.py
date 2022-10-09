import sys

filename = "input.txt"
floor = 0

with open(sys.argv[1], 'r') as input_file:
    for i, line in enumerate(input_file):
        for char in line:
            print("Char found:", char)
            print("Current floor:", floor)
            floor = floor + 1 if char == '(' else floor - 1

print("Santa is on the %dth floor" % floor)