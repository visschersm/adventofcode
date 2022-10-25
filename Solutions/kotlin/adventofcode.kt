package Solutions.kotlin;

import Solutions.kotlin.Date;

fun main(args: Array<String>) {
    if (args.size == 0) {
        println("Please provide a date for which to solve a challenge")
        return
    }

    val dateStr = args[0]
    val date = getDate(dateStr)
    print(date.Format())
    // val solution = getSolution(date)
    // solve(solution)
}

fun getDate(date: String): Date {
    return Date(2015, 1);
}

// fun getSolution(date: Date): Solution {
//     return Solution01();
// }

// fun solve(solution: Solution) {
//     solution.Part1("hello world");
//     solution.Part2("hello world");
// }