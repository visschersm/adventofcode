from cgitb import small
from mymath.geometry import cube
from util.filereader import *
from operator import mul
from functools import reduce


def part1(input_file):
    total = 0

    for line in read_by_line(input_file):
        dimensions = [int(numeric_string) for numeric_string in str.split(line, "x")]
        gift_surface = cube.surface(dimensions[0], dimensions[1], dimensions[2])
        smallest_surface = cube.smallest_surface(dimensions[0], dimensions[1], dimensions[2])
        total += gift_surface + smallest_surface
    
    print("The elves need to order", total, "square feet of wrapping paper") 

def part2(input_file):
    total = 0

    for line in read_by_line(input_file):
        dimensions = [int(numeric_string) for numeric_string in str.split(line, "x")]
        sorted_dimensions = sorted(dimensions)[0:2]
        total += sorted_dimensions[0] * 2 + sorted_dimensions[1] * 2 + cube.cubed(dimensions[0], dimensions[1], dimensions[2])

    print("The elves need to order", total, "feet of ribbon.");


