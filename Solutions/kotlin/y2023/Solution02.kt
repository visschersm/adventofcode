package Solutions.kotlin.y2023

import Solutions.kotlin.Solution
import java.io.File

// The Elf would first like to know which games would have been possible
// if the bag contained only 12 red cubes, 13 green cubes, and 14 blue cubes?

class Solution02 : Solution {
    override fun Part1(inputFile: String) {
        val maxRedValue = 12
        val maxGreenValue = 13
        val maxBlueValue = 14

        // Game 1: 4 red, 18 green, 15 blue; 17 green, 18 blue, 9 red; 8 red, 14 green, 6 blue; 14
        // green, 12 blue, 2 red
        val result =
                File(inputFile)
                        .readLines()
                        .filter() { line ->
                            val dices: String = line.split(":")[1]
                            dices.split(";").all() { dice ->
                                dice.split(",").all() { die ->
                                    // Define a regular expression pattern to match numbers
                                    val regexPattern = Regex("\\d+")

                                    // Find the first match in the string
                                    val matchResult = regexPattern.find(die)

                                    // Extract the matched number as a string
                                    val matchedNumberString: String? = matchResult?.value

                                    if (matchedNumberString == null) {
                                        false
                                    } else {
                                        // Convert the string to an actual number (if needed)
                                        val matchedNumber: Int = matchedNumberString.toInt()

                                        if (die.contains("red") && matchedNumber > maxRedValue) {
                                            false
                                        } else if (die.contains("green") &&
                                                        matchedNumber > maxGreenValue
                                        ) {
                                            false
                                        } else if (die.contains("blue") &&
                                                        matchedNumber > maxBlueValue
                                        ) {
                                            false
                                        } else {
                                            true
                                        }
                                    }
                                }
                            }
                        }
                        .sumOf() { game ->
                            val gameNumber: String = game.split(":")[0]
                            // Define a regular expression pattern to match numbers
                            val regexPattern = Regex("\\d+")

                            // Find the first match in the string
                            val matchResult = regexPattern.find(gameNumber)

                            // Extract the matched number as a string
                            val matchedNumberString: String? = matchResult?.value

                            val matchedNumber: Int = matchedNumberString?.toInt() ?: 0

                            matchedNumber
                        }

        println("Result: $result")
    }

    override fun Part2(inputFile: String) {
        val result =
                File(inputFile).readLines().sumOf() { line ->
                    val myMap = mutableMapOf<String?, Int>()
                    val regexPattern = Regex("(?<count>\\d+) (?<color>\\b\\w+\\b)(,|;)?")
                    regexPattern.findAll(line).forEach { match ->
                        val count = match.groups["count"]?.value?.toInt() ?: throw Exception()
                        val color = match.groups["color"]?.value ?: throw Exception()
                        myMap[color] = maxOf((myMap[color] ?: 0), count)
                    }

                    (myMap["red"] ?: 1) * (myMap["green"] ?: 1) * (myMap["blue"] ?: 1)
                }
        println("Result: $result")
    }
}
