#!/bin/bash

function usage {
    echo "Usage: $(basename $0) [-d DATE]" 2>&1
    echo "Solve challenge using bash for given date."
    echo "  -d DATE Specify date for which to solve the challenge."
    exit 1
}

declare DATE

optstring="d:"
while getopts ${optstring} arg; do
    case ${arg} in
        d)
            DATE="${OPTARG}"
            ;;
        ?)
            echo "Invalid option: -${OPTARG}."
            echo 
            usage
            ;;
    esac
done

function get_solution() {
    local date=$1
    IFS="/"
    read -ra newarr <<< "$DATE"
    YEAR=${newarr[0]}
    DAY=${newarr[1]}
    echo "Solutions/bash/y$YEAR/Solution`printf %02d $DAY`.sh"
}

SOLUTION=$(get_solution $DATE)

source "./$SOLUTION"
Part1
Part2
