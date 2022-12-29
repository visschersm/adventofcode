import sys
import getopt
import importlib.util

def main(argumentList):
    date = ""
    overrideFile = None

    options = "hid"
    long_options = ["help", "date=", "input_file="]

    try:
        arguments, values = getopt.getopt(argumentList, options, long_options)
    
        # checking each argument
        for currentArgument, currentValue in arguments:
    
            if currentArgument in ("-h", "--Help"):
                print ("Displaying Help")
                
            elif currentArgument in ("-d", "--date"):
                date = currentValue
            
            elif currentArgument in ("-i", "--input_file"):
                overrideFile = currentValue
            
    except getopt.error as err:
        print (str(err))

    splittedDate = date.split("/")

    year = int(splittedDate[0])
    day = int(splittedDate[1])

    strNum = str(day)
    strNum = strNum.rjust(2, '0')

    input = "Inputs/" + str(year) + "/" + strNum + ".txt"
    if overrideFile is not None:
        input = overrideFile

    module_name = "y" + str(year) + ".solution" + strNum

    if importlib.util.find_spec(module_name) is None:
        print(module_name + " was not yet created.")
        return

    __import__(module_name)
    mymodule = sys.modules[module_name]

    with open(input, 'r') as input_file:
        mymodule.part1(input_file)
        input_file.seek(0,0)
        mymodule.part2(input_file)

if __name__ == "__main__":
   main(sys.argv[1:])
