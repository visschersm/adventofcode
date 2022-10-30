function Part1() {
    local inputfile=$1
    local result=0

    while read -n1 c; do
        if [ "$c" = '(' ]; then
            let result=result+1
        else
            let result=result-1
        fi
    done < $inputfile

    printf "Santa is on the %dth floor.\n" $result
}

function Part2() {
    local inputfile=$1
    local result=0
    local trycounter=0

    while read -n1 c; do
        if [ "$c" = '(' ]; then
            let result=result+1
        else
            let result=result-1
        fi

        let trycounter=trycounter+1

        if [ $result -eq -1 ]; then
            break
        fi
    
    done < $inputfile

    printf "Santa found the basement after %d tries.\n" $trycounter
}