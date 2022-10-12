from cgitb import small
from mymath.geometry import cube
from util.filereader import *


def part1(input_file):
    total = 0

    for line in read_by_line(input_file):
        dimensions = [int(numeric_string) for numeric_string in str.split(line, "x")]
        gift_surface = cube.surface(dimensions[0], dimensions[1], dimensions[2])
        #print("gift surface:", gift_surface)
        smallest_surface = cube.smallest_surface(dimensions[0], dimensions[1], dimensions[2])
        #print("smallest surface", smallest_surface)
        total += gift_surface + smallest_surface
    
    print("The elves need to order", total, "square feet of wrapping paper") 

def part2(input_file):
    print("Part2 not yet done")

