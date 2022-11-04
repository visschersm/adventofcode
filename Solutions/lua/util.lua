local util = {}

function util.split (inputString, seperator)
    local t = {}
    for str in string.gmatch(inputString, "([^"..seperator.."]+)") do
        table.insert(t, str)
    end
    return t
end

return util