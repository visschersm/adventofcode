get_date <- function(args) {
    date <- strsplit(args[1], split = "/")
    return(date)
}

get_solution <- function(year, day) {
    result <- paste("Solutions/r/y", year, "/Solution", day, ".r", sep = "")
    return(result)
}

get_inputfile <- function(year, day) {
    result <- paste("Inputs/", year, "/", day, ".txt", sep = "")
    return(result)
}

solve <- function(solution, input_file) {
    source(solution)
    part1(input_file)  # nolint
    part2(input_file)  # nolint
}

args <- commandArgs(trailingOnly = TRUE)
date <- get_date(args)
year <- date[[1]][1]
day <- date[[1]][2]
input_file <- get_inputfile(year, day)
solution <- get_solution(year, day)

solve(solution, input_file)