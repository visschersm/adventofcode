package Solutions.java;

public class AdventOfCode {
    public static void main(String[] args) {
        if(args.length == 0) {
            System.out.println("Please provide a date for which to solve a challenge");
            return;
        }

        var date = getDate(args[0]);

        var inputFile = getInputFile(args, date);
        solve(date, inputFile);
    }

    public static Date getDate(String dateStr) {
        var splittedDate = dateStr.split("/");
        return new Date(Integer.parseInt(splittedDate[0]), Integer.parseInt(splittedDate[1]));
    }

    public static void solve(Date date, String inputFile) {
        var className = String.format("Solutions.java.y%d.Solution%02d", date.year, date.day);
        try
        {
            var classObject = Class.forName(className);
            var instance = classObject.getDeclaredConstructor().newInstance();
            
            var part1 = classObject.getMethod("Part1", String.class);
            var part2 = classObject.getMethod("Part2", String.class);
            
            part1.invoke(instance, inputFile);
            part2.invoke(instance, inputFile);
        }
        catch(ClassNotFoundException e)
        {
            System.out.printf("Solution not found for %d/%02d\n", date.year, date.day);
        }
        catch(Exception e)
        {
            System.out.println("Something went wrong");
        }
    }

    public static String getInputFile(String[] args, Date date) {
        if (args.length >= 2)
            return args[1]; 
        else 
            return String.format("Inputs/%d/%02d.txt", date.year, date.day);
    }
}