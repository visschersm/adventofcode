import sys, getopt

def main(argumentList):
    date = ""
    overrideFile = None

    options = "hid"
    long_options = ["help", "date=", "input_file="]

    try:
        arguments, values = getopt.getopt(argumentList, options, long_options)
    
        # checking each argument
        for currentArgument, currentValue in arguments:
    
            print("CurrentArgument:", currentArgument, "CurrentValue:", currentValue)

            if currentArgument in ("-h", "--Help"):
                print ("Displaying Help")
                
            elif currentArgument in ("-d", "--date"):
                print ("Date provided:", currentValue)
                date = currentValue
            
            elif currentArgument in ("-i", "--input_file"):
                print ("File provided:", currentValue)
                overrideFile = currentValue
            
    except getopt.error as err:
        print (str(err))

    splittedDate = date.split("/")

    year = int(splittedDate[0])
    day = int(splittedDate[1])

    strNum = str(day)
    strNum = strNum.rjust(2, '0')

    print("Date parsed:", str(year), strNum)

    input = "Inputs/" + str(year) + "/" + strNum + ".txt"
    if overrideFile is not None:
        input = overrideFile

    print("InputFile:", input)

    module_name = str(year) + ".solution" + strNum
    print("ModuleName:", module_name)

    __import__(module_name)
    mymodule = sys.modules[module_name]

    with open(input, 'r') as input_file:
        print("Answer part1:")
        mymodule.part1(input_file)
        input_file.seek(0,0)
        print("Answer part2:")
        mymodule.part2(input_file)

if __name__ == "__main__":
   main(sys.argv[1:])
