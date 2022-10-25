package Solutions.kotlin;

import kotlin.reflect.cast

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
    val splittedDate = date.split("/")
    return Date(splittedDate[0].toInt(), splittedDate[1].toInt())
}

fun solve(date: Date) {
    val className = "Solutions.kotlin.y%d.Solution%02d".format(date.year, date.day)
    val classObject = Class.forName(className).kotlin
    val ctor = classObject.constructors.first()
    val obj = ctor.call() as Solution
    obj.Part1("inputfile")
    obj.Part2("inputfile")
}