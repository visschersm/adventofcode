<?php
    interface Solution {
        public function Part1($inputFile);
        public function Part2($inputFile);
    }

    function solve($solution, $inputFile) {
        $solution->Part1($inputFile);
        $solution->Part2($inputFile);
    }
?>