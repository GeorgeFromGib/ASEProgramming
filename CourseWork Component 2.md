# CourseWork Component 2

## Programming commands
### Variables
Implemented via a Dictionary in the ProgramIntepreter Class

Variables can be composed of any length of alphanumeric characters that do not start with a digit  

Parser class will evaluate expressions e.g. a-10*2 containing variables and values.

Supported operands are +, -, /, *, ==, !=, >=, <=, >, <

Expressions are only allowed on the let, if and while commands
### Loop Commands
Implemented via WhileCmd and endloop command pair. Does not allow for loop statement nesting

Boolean AND and OR are not supported

If statement can be nested within loop
### If Statement
Implemented via IfCommand and endif command pair. Does not allow for if statement nesting

Boolean AND and OR are not supported.

Alphanumeric boolean expressions are not supported.
### Syntax Checking
Done in Interpreter class via the ProgramSyntaxCheck function
### Method with parameters
Implemented via the FunctionCmd and EndFunctionCmd classes. 
### Flashing colors
Flash colors using threads (half second alternate)
- redgreen
- blueyellow
- blackwhite

Implemented by new AESColor class which will alternate the colors each time the color is requested on those instances where a flashing color is specified in the constructor

The Canvas class implements a timed thread which runs every 500 milliseconds and executes all stored rendering commands stored in its _drawActions buffer. Some of these commends request the colour for the pen or brush from the AES Color instance stored within the recorded action and thus flashing colours are achieved. 
## Design & implementation
### Design Patterns - factory class
Implemented via the CommandFactory class
### Additional design patterns
Using command pattern (for commands) via CommandFactory class
### XML Tags
To be done - **MEGHAN - yes you have to do it!!!!!!**

### Exception handling
Defined Custom Exceptions
ProgramException

Exception handlers are located in the Interpreter class to catch ApplicationExceptions and throw the Custom ProgramException which contains the program Line No wher the error occured.
All exceptions ar bubbled up the to the try catch pair in the Forms class CommandEntered method.
## Additional functionality
Polygon and triangle shapes Implemented by PolygonCmd and TriangleCmd respective classes

###Test program
####Program to draw house

    function picketFences(x,y, range, col)
        pen col
        var r=0
        var px=0
        while r < range
            let px=x+r
            moveto px,y
            rectangle 10,70
            let r=r+20
        endloop
        var ly=y+10
        moveto x,ly
        rectangle 150,10
    endfunction
    
    function window(x,y,w,h,col)
        pen col
        moveto x,y
        rectangle w,h
        var mh=h/2 + y
        var dx=x+w
        var mw=w/2 + x
        var dy=y+h
        pen 'black'
        moveto x,mh
        drawto dx,mh
        moveto mw,y
        drawto mw, dy
    endfunction
    
    function beacon(x,y,w,h,flashcol,postcol)
        pen flashcol
        moveto x,y
        rectangle w,10
        pen postcol
        var py=y+10
        moveto x,py
        rectangle 10,100
    endfunction
    
    rem set daytime flag
    var daytime=1
    if daytime == 1
        var sunCol='orange'
        var skyCol='lightblue'
        var grassCol='green'
        var fenceCol='white'
        var pathCol='chocolate'
        var wallCol='cornflowerblue'
        var roofCol='indianRed'
        var windowCol='azure'
        var doorCol='white'
    endif
    if daytime != 1
        var sunCol='lightyellow'
        var skyCol='darkblue'
        var grassCol='darkgreen'
        var fenceCol='lightgray'
        var pathCol='brown'
        var wallCol='blue'
        var roofCol='darkRed'
        var windowCol='yellow'
        var doorCol='lightgray'
    endif
    
    fill 'on'
    reset
    
    rem Sky
    pen skyCol
    rectangle 400,400
    
    rem Sun or Moon
    pen sunCol
    moveto 50,50
    circle 60
    if daytime != 1
        pen skyCol
        moveto 70,50
        circle 60
    endif
    
    rem Grass
    moveto 0,220
    pen grassCol
    rectangle 400,160
    
    rem Path
    pen pathCol
    triangle 95,600,305,600,195,165
    
    
    rem House
    pen wallCol
    moveto 70,150
    rectangle 250,100
    pen roofCol
    triangle 70,150,320,150,195,100
    
    rem Windows
    call window(90,170, 60,40, windowCol)
    call window(240,170, 60,40, windowCol)
    
    rem Door
    pen doorCol
    moveto 175,189
    rectangle 40,60
    
    rem Picket Fences
    call picketFences(0,300,150,fenceCol)
    call picketFences(245,300,150, fenceCol)
    
    rem Beacons
    call beacon(140,230,10,100,'redgreen',fenceCol)
    call beacon(245,230,10,100,'blueyellow',fenceCol)
