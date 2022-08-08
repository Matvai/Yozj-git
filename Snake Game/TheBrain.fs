module TheBrain

type direction = Up | Down | Left | Right

let rec safeTake n list =
    if n <= 0 then []
    else
        match list with
        | [] -> []
        | x::rest -> x :: safeTake (n-1) rest

type gameState = { snake: (int * int) list; apples: Map<int * int, int>; direction: direction }

let random w h = ((new System.Random()).Next(0, w), (new System.Random()).Next(0, h))

let appleState () = 
    match (new System.Random()).Next(0, 9) with
    |0 -> 1
    |1 -> 3
    |_ -> 2

let newApples w h rottingApples apples =
    let changeState x =
        match x with
        |1 ->
            match (new System.Random()).Next(0, 299) with
            |0 -> 0
            |_ -> 1
        |3 -> 
            match (new System.Random()).Next(0, 199) with
            |0 -> 2
            |_ -> 3
        |_ -> 
            match (new System.Random()).Next(0, 399) with
            |0 -> 1
            |_ -> 2
    let rec rightAmount m =
        if Map.count m < 8
            then rightAmount (Map.add (random w h) (appleState()) m)
            else m
    let z = 
        if rottingApples
            then 
                apples
                |> Map.map (fun _ b -> changeState b)
                |> Map.filter (fun _ x -> x <> 0)
            else apples
    rightAmount z

let initialState bw bh = 
    { 
        snake = [(3,1);(2,1);(1,1)]
        apples = [1..6] |> List.map (fun _ -> (random bw bh), 2) |> Map.ofList
        direction = Right
    }

let nextStep s boardwidth boardheight rottingApples =
    match s.snake with
    |[] -> s
    |(a, b)::c -> 
        if List.contains (a, b) c
        then
            { s with
               snake = []
               direction = Right
            }
        else
            let head =
                match s.direction with
                |Up -> if b > 0 then (a, b - 1) else (a, boardheight)
                |Down -> if b <= boardheight then (a, b + 1) else (a, 0)
                |Left -> if a > 0 then (a - 1, b) else (boardwidth, b)
                |Right -> if a <= boardwidth then (a + 1, b) else (0, b)
            
            { s with
                snake = 
                    match Map.tryFind (a, b) s.apples with
                    |Some _ when not rottingApples -> head::s.snake
                    |Some 2 -> head::s.snake
                    |Some 1 -> head::safeTake ((List.length s.snake) - 2) s.snake
                    |Some 3 ->
                        match List.tryLast s.snake with
                        |Some e -> List.append s.snake [e] |> List.append [head]
                        |_ -> head::s.snake
                    |_ -> head::List.take ((List.length s.snake) - 1) s.snake
                apples = s.apples |> Map.remove (a, b) |> newApples boardwidth boardheight rottingApples
            }

let getTo d (x1, y1) (x2, y2) =
    if x1 - x2 = 0
    then 
        if y1 < y2 && d <> Up
        then Down
        else 
            if y1 > y2 && d <> Down
            then Up
            else d
    else
        if x1 < x2 && d <> Left
        then Right
        else
            if x1 > x2 && d <> Right
            then Left
            else d

// let avoid d (x1, y1) (x2, y2) =
//     if x1 - x2 = 0
//     then 
//         if y1 < y2
//         then Up
//         else Down
//     else
//         if x1 < x2
//         then Left
//         else Right

let clockwise = function
    | Right -> Down
    | Down -> Left
    | Left -> Up
    | Up -> Right

let counterClockwise = function
    | Right -> Up
    | Up -> Left
    | Left -> Down
    | Down -> Right

// let f x = match x with
//   то же самое:
// let f = function

let changeDirection s boardwidth boardheight =
    match s.snake with
    |[] -> s.direction
    |(x, y)::_ ->
        let rec go gs n = 
            if n = 0 then 0
            else 
                let gs1 = nextStep gs boardwidth boardheight false
                if List.isEmpty gs1.snake
                    then 0
                    else (go gs1 (n-1)) + 1
        
        let bestDirection where =
            [where; clockwise where; counterClockwise where]
            |> List.sortByDescending (fun x -> go { s with direction = x } 42) 
            |> List.head

        let nearestApple = 
                s.apples |> Map.tryFindKey (fun (a, b) c -> 
                    c > 1
                    && a >= x - 4
                    && a <= x + 4
                    && b >= y - 4
                    && b <= y + 4
                    && (a, b) <> (x, y)
                ) 

        let tryFirst = 
            match nearestApple with
            |Some h ->
                getTo s.direction (x, y) h
            |None ->
                match ((new System.Random()).Next(0, 16)) with
                |15 -> clockwise s.direction
                |10 -> counterClockwise s.direction
                |_ -> s.direction

        // printfn "%s" (snake |> List.map string |> String.concat ",")
        // printfn $"Going: {d}, Trying: {tryInOrder}"
        
        if go { s with direction = tryFirst } 42 > 10
            then tryFirst
            else bestDirection s.direction
