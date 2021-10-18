let filterNeighbors (x, y) cells =
    cells |> List.filter (fun (a, b) -> 
        a >= x - 1 
        && a <= x + 1 
        && b >= y - 1 
        && b <= y + 1 
        ) 

let rec countNeighbors (x, y) cells =
    cells |> filterNeighbors (x, y) |> List.length

let gol cell cells =
    let n = countNeighbors cell cells
    if n < 2 then dies 
    else if n > 3 then dies 
    else if n = 2 then lives 
    else lives