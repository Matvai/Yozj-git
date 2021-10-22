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

filterNeighbors (42, 42)

[for x in [1..5] do for y in [1..3] -> (x, y)]

let gol cell cells =
    let n = countNeighbors cell cells
    if n < 2 then Dies
    else if n > 3 then Dies 
    else if n = 2 then AsItWas 
    else Lives

let createCell cell cells =
    filterNeighbors cell
    |> List.choose (fun x -> 
        match gol x cells with
        |Lives -> Some x
        |AsItWas -> if List.contains x cells then Some x else None /// ?????
        |_ -> None
        )

let createAllCells cells =
    cells
    |> List.collect (fun x -> createCell x cells)
    |> List.distinct

let printGOL cells =
    let rec printGOL2 cells x y =
        if y < 10 then
            match List.contains (x, y) cells, x with
            |true, 10 -> printfn "1"; printGOL2 cells  1 (y + 1)
            |false, 10 -> printfn "0"; printGOL2 cells  1 (y + 1)
            |true, _ -> printf "1"; printGOL2 cells  (x + 1) y
            |false, _ -> printf "0"; printGOL2 cells  (x + 1) y
    printGOL2 cells 1 1
    ()

let printGOLBetter cells =
    let rec printGOL2 cells x y =
        if y < 10 then
            printf (if List.contains (x, y) cells then "1" else "0")
            if x >= 10 then printfn ""; printGOL2 cells 1 (y + 1)
            else printGOL2 cells  (x + 1) y
    printGOL2 cells 1 1
    ()


let printGOLEvenBetter cells =
    for y in 1..10 do
        for x in 1..10 do
            printf (if List.contains (x, y) cells then "1" else "0")
        printfn ""

printGOLEvenBetter [1,1;1,3;1,6;2,5;4,4;7,6]

let cc = [42,42; 41,42; 44,42; 42,41; 41,43]

createAllCells cc

createCell (42,42) cc

filterNeighbors (42,42)

List.choose (fun x -> if x < 3 then Some x else None) [1;2;3;4;5;6]

let f x = x + 10
let y = f 5