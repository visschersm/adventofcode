package Solutions.java;

public class AdventOfCode {
    public static void main(String[] args) {
        if(args.length == 0) {
            System.out.println("Please provide a date for which to solve a challenge");
            return;
        }

        var date = getDate(args[0]);
        System.out.printf("Provided date: %d/%02d\n", date.year, date.day);

        solve(date);
    }

    public static Date getDate(String dateStr) {
        var splittedDate = dateStr.split("/");
        return new Date(Integer.parseInt(splittedDate[0]), Integer.parseInt(splittedDate[1]));
    }

    public static void solve(Date date) {
        var className = String.format("Solutions.java.y%d.Solution%02d", date.year, date.day);
        try
        {

            var classObject = Class.forName(className);
            var instance = classObject.getDeclaredConstructor().newInstance();
            
            var part1 = classObject.getMethod("Part1", String.class);
            var part2 = classObject.getMethod("Part2", String.class);
            
            part1.invoke(instance, "");
            part2.invoke(instance, "");
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
}