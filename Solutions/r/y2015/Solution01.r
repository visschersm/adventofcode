part1 <- function(input_file) {
    data <- paste(readLines(input_file), collapse = "\n")
    result <- 0

    characters <- strsplit(data, split = "")
    for (i in characters[[1]]) {
        result <- result + if (i == "(") 1 else -1
    }

    print(paste("Santa is on the ", result, "th floor", sep = ""))
}

part2 <- function(input_file) {
    data <- paste(readLines(input_file), collapse = "\n")
    floor <- 0
    trycounter <- 0

    characters <- strsplit(data, split = "")
    for (i in characters[[1]]) {
        trycounter <- trycounter + 1
        floor <- floor + if (i == "(") 1 else -1
        if (floor < 0) {
            break
        }
    }

    print(paste("Santa found the basement after ", trycounter, " tries", sep = "")) # nolint
}