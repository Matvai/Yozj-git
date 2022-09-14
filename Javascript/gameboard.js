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
            c.toggleClass("alive")
            t.text(allCells.filter (x => x.cell.hasClass("alive")).length)
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
x.square.appendTo(document.body)

function getLiveCells(cells) {
    // TODO: implement this correctly
    return cells.map (x => {
        if (x.cell.hasClass('alive'))
            return  x.coords
        else 
            return null
    } )
        .filter (x => x != null)
        // return [{ x: 1, y: 2 }, { x: 3, y: 4 }]
}
    
function drawLiveCells(coordsList) {
    allCells.map (x => {
        if (coordsList.findIndex(z => z.x == x.coords.x && z.y == x.coords.y) > -1) {
            x.cell.addClass('alive')
        }
        else {
            x.cell.removeClass('alive')    
        }
    })
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
    let oldCells = getLiveCells(allCells)
    let newCells = theBrain.nextStep(oldCells)
    drawLiveCells(newCells)
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
        drawLiveCells([])
})