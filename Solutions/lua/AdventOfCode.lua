local util = require("Solutions.lua.util")

local function getdate(arg)
    local dateStr = arg[1]
    local date = util.split(dateStr, "/")
    return date
end

local function getsolution(date)
    local solutionName = "Solutions.lua.y" .. date[1] .. ".solution" .. date[2]
    return solutionName
end

local function getinputfile(date)
    local inputfile = "Inputs/" .. date[1] .. "/" .. date[2] .. ".txt"
    return inputfile
end

local function solve(solutionName, inputfile)
    local solution = require(solutionName)
    solution.part1(inputfile)
    solution.part2(inputfile)
end

local date = getdate(arg)
local solutionName = getsolution(date)
local inputfile = arg[2]

if (inputfile == nil) then
    inputfile = getinputfile(date)
end

solve(solutionName, inputfile)

