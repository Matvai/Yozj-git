let even x = x % 2 = 0


// let f x =
//     let rec x3 input output =
//         match input with
//         |[] -> output
//         |a::b -> if even a then x3 b (List.append output [a*3]) else x3 b output
//     x3 x []

f [1;2;3;4]
f [1;1;3;5]
f [3;2;2]
f [4;6;8;20]

List.map (fun x -> x*3) [1;2;3;4]
List.filter even [1;2;3;4]

let rec f input =
    input
    |> List.filter even
    |> List.map (fun x -> x*3)

[14;43;45;47;41;25] |> f


let f input =
    input
    |> List.filter (fun x -> not (even x))
    |> List.sum


f [1;2;3]
f [5;7]
f [2;4;6]
f [3;5;2;4;7]

let g h =
    h
    |> List.filter (fun x -> not (even x))
    |> List.map (fun x -> x * 2)
    |> List.rev

g [1;2;3;4;5]
g [5;6;7]
g [2;4]
g [1;3]
