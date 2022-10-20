<?php

$shortOpts = "d:";
$longOpts = array(
        "date:"
);
$options = getopt($shortOpts, $longOpts);

include dirname(__DIR__)."\php\Date.php";
$date = getDateFromArguments($options);

$inputFile = getInputFile($date->year, $date->day);

include dirname(__DIR__)."\php\SolutionResolver.php";
$solution = getSolution($date->year, $date->day);

Solve($solution, $inputFile);

function getInputFile($year, $day) {
    return "Inputs/$year/$day.txt";
}

?>