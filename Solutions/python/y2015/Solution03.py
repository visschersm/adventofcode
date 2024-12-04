from util.filereader import *


def move(position, c):
   x, y = position
   match c:
      case "^":
         return (x, y + 1)
      case ">":
         return (x + 1, y)
      case "v":
         return (x, y - 1)
      case "<":
         return (x - 1, y)


def part1(input_file):
   result = []
   previousPosition = (0, 0)
   result.append(previousPosition)

   for c in read_by_character(input_file):
      previousPosition = move(previousPosition, c)
      result.append(previousPosition)

   count = len(set(result))
   print("Number of houses that receive a present: " + str(count))


def part2(input_file):
   result = []
   previousPosition = (0, 0)
   previousPositionSanta = (0, 0)
   result.append(previousPosition)

   count = 0

   for c in read_by_character(input_file):
      if count % 2 == 0:
         previousPosition = move(previousPosition, c)
         result.append(previousPosition)
      else:
         previousPositionSanta = move(previousPositionSanta, c)
         result.append(previousPositionSanta)

      count = count + 1

   count = len(set(result))
   print("Number of houses that receive a present: " + str(count))
