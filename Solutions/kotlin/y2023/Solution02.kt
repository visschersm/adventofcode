package Solutions.kotlin.y2023

import Solutions.kotlin.Solution
import java.io.InputStream
import java.io.File
import java.io.BufferedReader

// The Elf would first like to know which games would have been possible 
// if the bag contained only 12 red cubes, 13 green cubes, and 14 blue cubes?

class Solution02 : Solution
{
    override fun Part1(inputFile: String) {
        val maxRedValue = 12
        val maxGreenValue = 13
        val maxBlueValue = 14

        File(inputFile).forEachLine().filter {
            line ->
                val gameNumber: String = line.split(":")[0]
                println(gameNumber)
                val dices: String = line.split(":")[1] 
                dices.split(";").forEach() {
                    dice -> dice.split(",").forEach() {
                        die -> 
                            // Define a regular expression pattern to match numbers
                            val regexPattern = Regex("\\d+")

                            // Find the first match in the string
                            val matchResult = regexPattern.find(die)

                            // Extract the matched number as a string
                            val matchedNumberString = matchResult?.value

                            // Convert the string to an actual number (if needed)
                            val matchedNumber = matchedNumberString?.toInt()
                            if(die.contains("red") && matchedNumber > maxRedValue) {
                                println("%s was not possible", gameNumber)
                                return false;
                            } else if(die.contains("green") && matchedNumber > maxGreenValue) {
                                println("%s was not possible", gameNumber)
                                return false;
                            } else if(die.contains("blue")&& matchedNumber > maxBlueValue) {
                                println("%s was not possible", gameNumber)
                                return false;
                            }
                    }
                }
        };
    }
    
    override fun Part2(inputFile: String) {
        
    }
}
