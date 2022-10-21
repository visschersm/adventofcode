import Date
import Solutions.*

fun main(args: Array<String>) {
    val dateStr = args[0]
    val date = getDate(dateStr)
    val solution = getSolution(date)
    solve(solution)
}

fun getDate(date: String): Date {
    return Date(2015, 1);
}

fun getSolution(date: Date): Solution {
    return Solution01();
}

fun solve(solution: Solution) {
    solution.Part1("hello world");
    solution.Part2("hello world");
}