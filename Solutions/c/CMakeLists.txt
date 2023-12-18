cmake_minimum_required(VERSION 3.27)
project(interfaces C)

set(CMAKE_C_STANDARD 11)

include_directories(y2023)

add_executable(adventofcode 
    date.c
    mystring.c
    registration.c
    y2023/Solution01.c
adventofcode.c)
