# import numpy as np

def split_str(s):
   return [c for c in s]

def compare_compartment(first_compartment, second_compartment):
   for char in first_compartment:
      if char in second_compartment:
         return char

def calculate_priority(char):
   intVal = ord(char) # 65 - 90 || 97 - 122
   if intVal <= 90:
      intVal -= 38
   else:
      intVal -= 96
   return intVal

def duplicates(rugsack1, rugsack2):
   result = []
   
   for char in rugsack1:
      if char in rugsack2:
         result.append(char)

   return result

def part1(input_file):
   # total = 0

   # for line in input_file.readlines():
   #    halfLine = int((len(line)-1)/2)
   #    characters = split_str(line)
   #    first_compartment = characters[:halfLine]
   #    second_compartment = characters[halfLine:-1]
      
   #    char = compare_compartment(first_compartment, second_compartment)
      
   #    intVal = calculate_priority(char)
   #    total += intVal

   # print("Sum of types:", total)
   print("ERROR")

def part2(input_file):
   total = 0

   groupCounter = 0
   lines = input_file.readlines()
   while (3* groupCounter) < len(lines):
      rugSack1 = split_str(lines[3 * groupCounter + 0])
      rugSack2 = split_str(lines[3 * groupCounter + 1])
      rugSack3 = split_str(lines[3 * groupCounter + 2])
      groupCounter += 1

      dups = duplicates(rugSack1, rugSack2)
      dups = duplicates(dups, rugSack3)

      # print(dups)
      intVal = calculate_priority(dups[0])
      total += intVal

   print("Sum of types:", total)

