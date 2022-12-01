local solution01 = {}

local file = require("Solutions.lua.file")

function solution01.part1 (inputfile)
    local maxElf = 0
    local currentElf = 0

    local content = file.lines(inputfile)
    for i = 1, #content do
        local line = content[i]
        if (line == "") then
            if (maxElf < currentElf)
            then
                maxElf = currentElf
            end
            currentElf = 0
        else
            currentElf = currentElf + line
        end
    end

    print("Maximum calories carried by an elf: "..maxElf)
end

function solution01.part2 (inputfile)
    local elves = {}
    local currentElf = 0
    local index = 0

    local content = file.lines(inputfile)
    for i = 1, #content do
        local line = content[i]
        if (line == "") then
            elves[index] = currentElf
            index = index + 1
            currentElf = 0
        else
            currentElf = currentElf + line
        end
    end

    table.sort(elves, function (x, y) return x > y end)

    local sum = 0
    for k,v in pairs({table.unpack(elves, 1, 3)}) do
        sum = sum + v
    end

    print("Sum of Top 3 calories: "..sum)

end

return solution01
