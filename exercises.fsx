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
List.collect (fun x -> [x;42;x]) [43;44;23]
// let printGOL cells =
//     let rec printGOL2 cells x y =
//         if y < 10 then
//             match List.contains (x, y) cells, x with
//             |true, 10 -> printfn "1"; printGOL2 cells  1 (y + 1)
//             |false, 10 -> printfn "0"; printGOL2 cells  1 (y + 1)
//             |true, _ -> printf "1"; printGOL2 cells  (x + 1) y
//             |false, _ -> printf "0"; printGOL2 cells  (x + 1) y
//     printGOL2 cells 1 1
//     ()

// let printGOLBetter cells =
//     let rec printGOL2 cells x y =
//         if y < 10 then
//             printf (if List.contains (x, y) cells then "1" else "0")
//             if x >= 10 then printfn ""; printGOL2 cells 1 (y + 1)
//             else printGOL2 cells  (x + 1) y
//     printGOL2 cells 1 1
//     ()

let printGOL cells area =
    for y in 1..area do
        for x in 1..area do
            printf (if List.contains (x, y) cells then "\x1B[38;2;255;0;0m#\x1B[39m" else " ")
        printfn ""

let shape1 = 
    [
        2,6;2,7;3,6;3,7;
        14,4;15,4;13,5;12,6;12,7;12,8;13,9;14,10;15,10;
        16,7;17,5;18,6;18,7;18,8;19,7;18,9;
        22,4;22,5;22,6;23,4;23,5;23,6;
        24,3;24,7;26,2;26,3;26,7;26,8;
        36,4;36,5;37,4;37,5
    ] 
    |> List.map (fun (x, y) -> x, y+12)

// printfn "\x1B[38;2;200;100;200mhello\x1B[39m goodbye"

let rec gameOfLifeExe cells area pauseTime:unit =
    let x = aliveCellsList cells
    printGOL x area
    printfn "__________"
    System.Threading.Thread.Sleep (pauseTime * 100)
    gameOfLifeExe x area pauseTime


let shape2 = [15,15; 16,15; 14,16; 15,16; 15,17]

let shape3 = [15,15; 16,15; 14,15; 17,15; 13,15; 16,16; 14,16; 16,14; 14,14; 15,16; 15,14; 15,17; 15,13]

gameOfLifeExe shape2 15 20