let theBrain = require ("./theBrain")

let $ = require ("jquery")

$("<div>")
    .text("fluff crocodil")
    .css({ color: "red" })
    .toggleClass("alive")
    .appendTo(document.body)

let t = $("<div>").text("0").appendTo(document.body)


function createCell (x, y) {
    console.log("p*tin crocodil")
    let c = $("<div>")
        .addClass("cell")
        .on("click", function() { 
            setState(theBrain.toggleCell({ x, y }, aliveCells))
        })
    return { cell: c, coords: { x: x, y: y } }
}

function createRow (size, y) {
    let r = $("<div>").addClass("hocol")
    let l = []
    for(let a = 0; a < size; a = a + 1) {
        let x = createCell(a, y)
        r.append(x.cell)
        l.push(x)
    }
    return { row: r, cells: l }
}

function createSquare (size) {
    let s = $("<div>").addClass("kvadratik m-2")
    let l = []
    for(let a = 0; a < size; a = a + 1) {
        let x = createRow(size, a)
        s.append(x.row)
        l = l.concat(x.cells)
    }
    return { square: s, cells: l }
}

let x = createSquare(15)
let allCells = x.cells
let aliveCells = []
x.square.appendTo(document.body)

function drawLiveCells(coordsList) {
    console.log(coordsList)
    allCells.map (x => {
        if (coordsList.findIndex(z => z.x == x.coords.x && z.y == x.coords.y) > -1) {
            x.cell.addClass('alive')
        }
        else {
            x.cell.removeClass('alive')    
        }
    })
}

function setState(newAliveCells) {
    aliveCells = newAliveCells
    drawLiveCells(aliveCells)
    t.text(aliveCells.length)
}

let timer

function button(text, onClick) {
    $("<button>")
        .text(text)
        .on("click", onClick)
        .appendTo(document.body)
        .addClass("btn btn-primary m-2")
}

function nextStep() {
    setState(theBrain.nextStep(aliveCells))
}

button("Get live cells", function() { 
    console.log(getLiveCells(allCells))
})

button("Next Step", nextStep)

button("Stop", function() { 
    clearInterval(timer) 
})

button("Start", function() {
    clearInterval(timer) 
    timer = setInterval( 
        nextStep, 
        420
    ) 
})

button("Clear", function() {
        setState([])
})