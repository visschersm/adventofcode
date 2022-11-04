local file = {}

function file.exists(filename)
    if filename == nil then return false end

    local f = io.open(filename, "rb")
    if f then f:close() end
    return f ~= nil
end

function file.lines(filename)
    if not file.exists(filename) then return {} end

    local lines = {}
    for line in io.lines(filename) do
        lines[#lines + 1] = line
    end

    return lines
end

function file.string(filename)
    if not file.exists(filename) then return "" end

    local f = io.open(filename, "rb")
    if f == nil then return "" end

    return f:read("a")
end

function file.characters(filename)
    if not file.exists(filename) then return {} end

    local characters = {}
    local f = io.open(filename, "rb")
    if f == nil then return characters end

    local content = f:read("a")
    for i = 1, #content do
        local c = content:sub(i, i)
        characters[#characters+1] = c
    end
    
    return characters
end

return file