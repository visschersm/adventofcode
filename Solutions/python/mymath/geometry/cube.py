from operator import mul
from functools import reduce

def surface(x, y, z):
    return 2*x*y + 2*y*z + 2*z*x

def cubed(x, y, z):
    return x*y*z

def smallest_surface(x, y, z):
    sorted_sides = sorted([x, y, z])
    return reduce(mul, sorted_sides[0:2])