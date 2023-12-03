function Part1() {
    local inputfile=$1
    local result=0

    while read -r line; do
        for (( i=0; i<${#line}; i++ )); do
            char="${line:$i:1}"
            if [[ $char =~ [0-9] ]]; then
                first_digit=$char
                break
            fi
        done

        # Find the last digit
        for (( i=${#line}-1; i>=0; i-- )); do
            char="${line:$i:1}"
            if [[ $char =~ [0-9] ]]; then
            last_digit=$char
            break
            fi
        done

        local concat="$first_digit$last_digit"
        result=$(($result + $concat))
    done < "$inputfile"
    echo $result

    printf "Santa is on the %dth floor.\n" $result
}

function Part2() {
    printf "Part2 not yet implemented.\n"
}
