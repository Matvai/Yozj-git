let number = 5
let anotherNumber = 3.1415
let string = "foo"
let array = [1,2,"foo","bar",42.5]
let object = { one: "1", two: "2", three: [1,2,3] }
let n = null
let u = undefined

function findNeighbors (cell) {
    let arr = [-1, 0, 1]
    return ( 
        arr.map(a =>
        arr.map(b =>
            ({x: cell.x + a, y: cell.y + b})
            )
        ).flat()
    )
}

function deduplicate (list) {
    return list.filter((e, index) => {
        let i = (list.findIndex ((a) => a.x == e.x && a.y == e.y))
        return i === index;
    });
}

function findAllNeighbors (cells) {
    return deduplicate (cells.map (x => findNeighbors (x)).flat())
}

function countNeighbors(cell, cells) {
    let n = 0
    for(let a = -1; a < 2; a = a + 1) {
        for(let b = -1; b < 2; b = b + 1) {
            if (!(a == 0 && b == 0) && cells.some( function(r) { return r.x == cell.x + a && r.y == cell.y + b }))
                n = n + 1
        }
    }
    return n
}

exports.nextStep = function (cells) {
    return findAllNeighbors(cells).map (function(x) {
        switch (countNeighbors (x, cells)) {
            case 2: { 
                if ((cells).findIndex ((a) => a.x == x.x && a.y == x.y) >= 0)
                    return x
                else
                    return null
            }
            case 3: return x
            default: return null
        }
    }
    ).filter ((z) => z != null)
}