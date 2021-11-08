module TheBrain
let filterLivingNeighbors (x, y) cells =
    cells |> List.filter (fun (a, b) -> 
        a >= x - 1 
        && a <= x + 1 
        && b >= y - 1 
        && b <= y + 1 
        && (a, b) <> (x, y)
        ) 

let rec countNeighbors (x, y) cells =
    cells |> filterLivingNeighbors (x, y) |> List.length

type Survival = Dies | Lives | AsItWas

let filterNeighbors (x, y) =
    [ for a in (x - 1)..(x + 1) do 
        for b in (y - 1)..(y + 1) -> (a, b)
    ]

let gol cell cells =
    let n = countNeighbors cell cells
    if n < 2 then Dies
    else if n > 3 then Dies 
    else if n = 2 then AsItWas 
    else Lives

/// Возвращает список всех соседей клетки cell, которые будут живы на следующем ходу
let livingNeighbors cell cells =
    filterNeighbors cell
    |> List.choose (fun x -> 
        match gol x cells with
        |Lives -> Some x
        |AsItWas -> if List.contains x cells then Some x else None /// ?????
        |_ -> None
        )

/// Возвращает список всех живых клеток в следующем ходу
let aliveCellsList cells =
    cells
    |> List.collect (fun x -> livingNeighbors x cells)
    |> List.distinct