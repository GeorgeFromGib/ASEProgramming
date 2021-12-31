# CourseWork Component 2

## Programming commands
### Variables
implemented via a Dictionary in the ProgramIntepreter Class
Variables can be composed of any length of alphnuremic characters that do not start with a digit  
Parser class will evaluate expressions e.g. a-10*2 containing variables and values.
supported operands are + - / * == != >= <= > <
Expressions are only allowed on the let, if and while commands
### Loop Commands
Implemented via WhileCmd and endloop command pair. Does not allow for loop statement nesting
Boolean AND and OR are not supported
If statement can be nested within loop
### If Statement
Implemented via IfCommand and endif command pair. Does not allow for if statement nesting
Boolean AND and OR are not supported
### Syntax Checking
Done in Interpreter class via the ProgramSyntaxCheck function
### Method with parameters
### Flashing colors
Flash colors using threads (half second alternate)
- redgreen
- blueyellow
- blackwhite
## Design & implementation
### Design Patterns - factory class
Implemented via the CommandFactory class
### Additional design patterns
Using command pattern (for commands) via CommandFactory class
### Exception handling
Defined Custom Exception handlers :
ProgramException & VariableNotFoundException
## Additional functionality
e.g. transform rotate

###Test program
####Program to draw house

    fill on
    reset
    pen lightblue
    rectangle 400 400
    position pen 50 50
    pen orange
    circle 60
    position pen 0 297
    pen green
    rectangle 400 120
    pen brown
    triangle 100 600 300 600 195 200
    pen blue
    position pen 70 200
    rectangle 250 100
    pen red
    triangle 70 200 320 200 195 150
    pen yellow
    position pen 90 210
    rectangle 60 40
    position pen 240 210
    rectangle 60 40
    position pen 175 240
    rectangle 40 60
    pen black
    position pen 300 150
    pen drawto 300 200