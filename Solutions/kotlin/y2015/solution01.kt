package Solutions.kotlin.y2015

import Solutions.kotlin.Solution
import java.io.InputStream
import java.io.File
import java.io.BufferedReader

class Solution01 : Solution
{
    override fun Part1(inputFile: String) {
        val data = File(inputFile).readText()
        var result = data.count { it == '(' } - data.count { it == ')' }

        print("Santa is on the %dth floor\n".format(result));
    }
    
    override fun Part2(inputFile: String) {
        var result = 0
        var tryCounter = 0

        File(inputFile).useLines() { 
            lines -> lines.forEach { 
                for (c in it) {
                    result += if(c == '(') 1 else -1
                    tryCounter++

                    if(result < 0) {
                        break
                    }
                } 
            } 
        }

        print("Santa found the basement after %d tries\n".format(tryCounter));
    }
}