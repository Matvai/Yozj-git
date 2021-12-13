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

let rules cell cells =
    let n = countNeighbors cell cells
    if n < 2 then Dies
    else if n > 3 then Dies 
    else if n = 2 then AsItWas 
    else Lives

/// Возвращает список всех соседей клетки cell, которые будут живы на следующем ходу
let livingNeighbors cell cells =
    filterNeighbors cell
    |> List.choose (fun x -> 
        match rules x cells with
        |Lives -> Some x
        |AsItWas -> if List.contains x cells then Some x else None /// ?????
        |_ -> None
        )

/// Возвращает список всех живых клеток в следующем ходу
let aliveCellsList cells =
    cells
    |> List.collect (fun x -> livingNeighbors x cells)
    |> List.distinct

let rec safeTake n list =
    if n <= 0 then []
    else
        match list with
        | [] -> []
        | x::rest -> x :: safeTake (n-1) rest


type History = {cells: (int * int) list; score: int}
type GameState = {cells: (int * int) list; score: int; history: History list}

let listAndIntToHistory list int =
    {cells = list; score = int}

let cellsOnly (list: History list) = list |> List.map (fun x -> x.cells)

let nextStep {cells = x; score = y; history = z} =
    let newCells = aliveCellsList x
    let newScore = 
        if List.contains newCells (safeTake 42 (cellsOnly z))
        then 0
        else y + 1
    let newHistory = {cells = newCells; score = newScore} :: safeTake 1000 z
    {cells = newCells; score = newScore; history = newHistory}

