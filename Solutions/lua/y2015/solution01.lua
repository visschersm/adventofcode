local solution01 = {}

local file = require("Solutions.lua.file")

function solution01.part1 (inputfile)
    local floor = 0

    local content = file.characters(inputfile)
    for i = 1, #content do
        local c = content[i]
        if (c == "(") then floor = floor + 1
        elseif (c == ")") then floor = floor - 1
        end
    end
    
    print("Santa is on the "..floor.."th floor")
end

function solution01.part2 (inputfile)
    local floor = 0
    local trycounter = 0

    local content = file.characters(inputfile)
    for i = 1, #content do
        trycounter = trycounter + 1
        local c = content[i]
        
        if (c == "(") then floor = floor + 1
        elseif (c == ")") then floor = floor - 1
        end

        if floor < 0 then break end
    end
    
    print("Santa found the basement after "..trycounter.." tries")
end

return solution01