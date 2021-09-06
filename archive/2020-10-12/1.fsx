List.rev [1;2;3]
List.rev [6;9;42]

// let rec q r s =
//     match r with
//     |[] -> s
//     |a::b -> a::q b s

// let rec f x = 
//     match x with
//     |[] -> []
//     |a::b -> q (f b) [a]

let reverse d =
    let rec f z x = 
        match x with
        |[] -> z
        |a::b -> f (a::z) b

    f [] d

reverse ["крокодил";"мурч"]

printfn "%s" (f [] [1..10000] |> List.map string |> String.concat ", ")


let sum x = 
    let rec m s n = match n with
                    | [] -> s
                    | a::b -> m (s + a) b

    m 0 x

sum [3;4;3] 