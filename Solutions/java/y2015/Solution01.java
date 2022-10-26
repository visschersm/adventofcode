package Solutions.java.y2015;

import java.io.File;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;

import Solutions.java.Solution;

public class Solution01 implements Solution {

    @Override
    public void Part1(String inputFile) {
        Path filename = Path.of(inputFile);
        var result = 0;

        try {
            var data = Files.readString(filename);
            for (int i =0; i < data.length(); ++i) {
                result += data.charAt(i) == '(' ? 1 : -1;
            }
        }
        catch(IOException exception) {

        }

        System.out.printf("Santa is on the %dth floor\n", result);
    }

    @Override
    public void Part2(String inputFile) {
        Path filename = Path.of(inputFile);
        var result = 0;
        var tryCounter = 0;

        try {
            var data = Files.readString(filename);
            for (int i =0; i < data.length(); ++i) {
                result += data.charAt(i) == '(' ? 1 : -1;
                tryCounter++;

                if(result < 0) {
                    break;
                }
            }
        }
        catch(IOException exception) {

        }

        System.out.printf("Santa found the basement after %d tries", tryCounter);
    }
    
}
