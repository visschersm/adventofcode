import sys

def read_by_character(input_file):
    for i, line in enumerate(input_file):
        for char in line:
            yield char

def read_by_line(input_file):
    for i, line in enumerate(input_file):
        yield line