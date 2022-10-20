<?php

class Date {
    public $year;
    public $day;

    function __construct($year, $day) {
        $this->year = $year;
        $this->day = $day;
    }

    function getDate() {
        return $this->year."/".$this->day;
    }
}

function getDateFromArguments($options) {
    $date = explode("/", $options["d"]);

    $year = $date[0];
    $day = $date[1];

    $date = new Date($year, $day);

    return $date;
}
?>