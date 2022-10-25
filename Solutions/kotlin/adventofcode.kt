package Solutions.kotlin;

fun main(args: Array<String>) {
    if (args.size == 0) {
        println("Please provide a date for which to solve a challenge")
        return
    }

    val dateStr = args[0]
    val date = getDate(dateStr)
    
    solve(date)
}

fun getDate(date: String): Date {
    return Date(2015, 1);
}

fun solve(date: Date) {
    print("Solve for %d/%02d\n".format(date.year, date.day))
}