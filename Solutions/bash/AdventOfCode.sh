#!/bin/bash

function usage {
    echo "Usage: $(basename $0) [-d DATE]" 2>&1
    echo "Solve challenge using bash for given date."
    echo "  -d DATE Specify date for which to solve the challenge."
    exit 1
}

declare DATE
declare INPUT_FILE

optstring="d:i:"
while getopts ${optstring} arg; do
    case ${arg} in
        d)
            DATE="${OPTARG}"
            ;;
        i)
            INPUT_FILE="${OPTARG}"
            ;;
        ?)
            echo "Invalid option: -${OPTARG}."
            echo 
            usage
            ;;
    esac
done

if test -z "$DATE"; then
    echo "Provide a date for which to solve a challenge."
    usage
fi

function get_solution() {
    local date=$1
    IFS="/"
    read -ra newarr <<< "$DATE"
    YEAR=${newarr[0]}
    DAY=${newarr[1]}
    echo "Solutions/bash/y$YEAR/Solution`printf %02d $DAY`.sh"
}

function get_inputfile() {
    local date=$1
    IFS="/"
    read -ra newarr <<< "$DATE"
    YEAR=${newarr[0]}
    DAY=${newarr[1]}
    echo "Inputs/$YEAR/`printf %02d $DAY`.txt"
}

SOLUTION=$(get_solution $DATE)

if test -z "$INPUT_FILE"; then
    INPUT_FILE=$(get_inputfile $DATE)
fi

source "./$SOLUTION"
Part1 $INPUT_FILE
Part2 $INPUT_FILE