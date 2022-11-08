param(
    [Parameter(Mandatory)][String]$date,
    [Parameter(Mandatory)][String]$input_file
)

$split = $date -Split "/"
$year = $split[0]
$day = $split[1]
clj "Solutions/clojure/y$year/Solution$day.clj" $input_file