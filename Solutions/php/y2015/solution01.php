<?php 

include dirname(__DIR__)."\Solution.php";

class Solution01 implements Solution {
    public function Part1($inputFile) {
        $result = 0;
        if ($file = fopen($inputFile, "r")) {
            while(!feof($file)) {
                $textperline = fgets($file);
                $chars = str_split($textperline);
                foreach ($chars as $char) {
                    $result += ($char === '(' ? 1 : -1);
                }    
            }
            fclose($file);
        }

        echo "Santa is on the ".$result."th floor.\n";
    }
    
    public function Part2($inputFile) {
        $result = 0;
        $counter = 0;

        if ($file = fopen($inputFile, "r")) {
            while(!feof($file)) {
                $textperline = fgets($file);
                $chars = str_split($textperline);
                foreach ($chars as $char) {
                    $result += ($char === '(' ? 1 : -1);
                    $counter += 1;

                    if($result < 0) {
                        break;
                    }
                }    
                // echo "Break while\n";
            }
            fclose($file);
        }

        echo "Santa found the basement after $counter tries\n";
    }
}
?>