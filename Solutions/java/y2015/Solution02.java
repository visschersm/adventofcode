package Solutions.java.y2015;

import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import java.util.stream.Collectors;

import Solutions.java.Solution;

public class Solution02 implements Solution {

    @Override
    public void Part1(String inputFile) {
        Path filename = Path.of(inputFile);

        try {
            var total = Files.readAllLines(filename)
            .stream()
            .map(line -> { return new Dimensions(line); })
            .mapToInt(gift -> { return gift.Surface() + gift.MinSurface(); })
            .sum();

            System.out.printf("The elves need to order %d square feet of wrapping paper\n", total);
        }
        catch(IOException exception) {

        }
    }

    @Override
    public void Part2(String inputFile) {
        Path filename = Path.of(inputFile);

        List<Dimensions> gifts = new ArrayList<Dimensions>();

        try {
            gifts = Files.readAllLines(filename)
                .stream()
                .map(line -> { return new Dimensions(line); })
                .collect(Collectors.toList());
        }
        catch(IOException exception) {

        }
        
        var total = 0;
        for (Dimensions gift : gifts) {
            int[] arr = { gift.l, gift.w, gift.h };

            var sum = Arrays.stream(arr)
                .sorted()
                .map(x -> { return x * 2; })
                .limit(2)
                .sum();
               
            total += gift.Cubed() + sum;
        }
        System.out.printf("The elves need to order %d feet of ribbon.\n", total);
    }

    public class Dimensions
    {
        public int l;
        public int w;
        public int h;

        public Dimensions(String dimensions)
        {
            var splitted = dimensions.split("x");
            this.l = Integer.parseInt(splitted[0]);
            this.w = Integer.parseInt(splitted[1]);
            this.h = Integer.parseInt(splitted[2]);
        }

        public int MinSurface() {
            return Math.min(Math.min(l * w, w * h), h * l);
        }
        public int Surface() {
            return 2 * l * w + 2 * w * h + 2 * h * l;
        }
        public int Cubed() {
            return l * w * h;
        }
        // public (int, int) ShortestPerimeter()
        // {
        //     //new[] {l, w, h}.Order().Take(2);
        //     var v1 = l * w;
        //     var v2 = w * h;
        //     var v3 = h * l;

        //     var faces = new[] { v1, v2, v3 };
        //     return (faces.Order().Take(1).Single(), faces.Order().Skip(1).Take(1).Single());
        // }
    }    
}
