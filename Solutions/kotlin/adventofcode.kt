package Solutions.kotlin;

import kotlin.reflect.cast

fun main(args: Array<String>) {
    if(dateProvided(args)) {
        println("Please provide a date for which to solve a challenge")
        return
    }

    val date = getDate(args)
    
    val solution = getSolution(date)
    val inputFile = getInputFile(date, args)

    solve(solution, inputFile)
}

fun dateProvided(args: Array<String>): Boolean {
    return args.size == 0
}

fun getDate(args: Array<String>): Date {
    val splittedDate = args[0].split("/")
    return Date(splittedDate[0].toInt(), splittedDate[1].toInt())
}

fun getSolution(date: Date): Solution {
    val className = getClassName(date)

    val classObject = Class.forName(className).kotlin
    val ctor = classObject.constructors.first()
    val obj = ctor.call() as Solution

    return obj
}

fun getClassName(date: Date): String {
    return "Solutions.kotlin.y%d.Solution%02d".format(date.year, date.day)
}

fun getInputFile(date: Date, args: Array<String>): String {
    if (args.size >= 2)
        return args[1] 
    else 
        return "Inputs/%d/%02d.txt".format(date.year, date.day)
}

fun solve(solution: Solution, inputFile: String) {
    solution.Part1(inputFile);
    solution.Part2(inputFile);
}