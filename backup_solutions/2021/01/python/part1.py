file = open("input.txt", "r")

increased = 0
firstLine = file.readline()
previousValue = int(firstLine)

lines = file.readlines()
for line in lines:
    lineValue = int(line)
    if lineValue > previousValue:
        increased = increased + 1
    previousValue = lineValue

print(increased)