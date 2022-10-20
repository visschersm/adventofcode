<?php

function getSolution($year, $day) {
    $path = "/php/y$year";
    $solutionName = "solution$day";
    $filename = "$solutionName.php";

    $fullpath = $path."/".$filename;

    include dirname(__DIR__)."$fullpath";
    $solution = new $solutionName;

    return $solution;
}

?>