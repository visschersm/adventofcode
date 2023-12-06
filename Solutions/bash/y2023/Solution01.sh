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
    echo "Result: $result"
}

function Part2() {
    local inputfile=$1
    local result=0

    while read -r line; do
        # first=${first/Suzy/$second}
        line="${line//one/one1one}"
        line="${line//two/two2two}"
        line="${line//three/three3three}"
        line="${line//four/four4four}"
        line="${line//five/five5five}"
        line="${line//six/six6six}"
        line="${line//seven/seven7seven}"
        line="${line//eight/eight8eight}"
        line="${line//nine/nine9nine}"

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
    echo "Result: $result"
}
