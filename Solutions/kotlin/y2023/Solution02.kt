package Solutions.kotlin.y2023

import Solutions.kotlin.Solution
import java.io.File

// The Elf would first like to know which games would have been possible
// if the bag contained only 12 red cubes, 13 green cubes, and 14 blue cubes?

class Solution02 : Solution {
    override fun Part1(inputFile: String) {
        val maxValues = mapOf("red" to 12, "green" to 13, "blue" to 14)

        // Game 1: 4 red, 18 green, 15 blue; 17 green, 18 blue, 9 red; 8 red, 14 green, 6 blue; 14
        // green, 12 blue, 2 red
        val result =
                File(inputFile).readLines().sumOf() { line ->
                    var gameNumberRegexPattern = Regex("Game (?<game>\\d+):")
                    val gameNumberMatch = gameNumberRegexPattern.find(line) ?: throw Exception()
                    val matchNumber =
                            gameNumberMatch.groups["game"]?.value?.toInt() ?: throw Exception()
                    val regexPattern = Regex("(?<count>\\d+) (?<color>\\b\\w+\\b)(,|;)?")
                    var matches = regexPattern.findAll(line)
                    val possible =
                            matches.all { match ->
                                val count =
                                        match.groups["count"]?.value?.toInt() ?: throw Exception()
                                val color = match.groups["color"]?.value ?: throw Exception()
                                maxValues[color]!! > count
                            }
                    if (possible) matchNumber else 0
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
