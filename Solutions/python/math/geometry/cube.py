def surface(x, y, z):
    return 2*x*y + 2*y*z + 2*z*x

def cubed(x, y, z):
    return x*y*z

def smallest_side_surface(x, y, z):
    order([x, y, z])[..2]