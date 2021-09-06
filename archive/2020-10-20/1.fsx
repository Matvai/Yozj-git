let f x y =
    match y with
    |None -> None
    |Some z -> Some (x z)


f (fun x -> x + 5) (Some 5) = Some 10
f (fun x -> x * 2) (Some 8) = Some 16
f (fun x -> x - 10) (Some 0) = Some (-10)
f (fun x -> "boo!") (Some 10)
f (fun x -> [x;x]) (Some 42)

Option.map (fun x -> [x;x]) (Some 42)
List.map (fun x -> [x;x]) [42]

List.filter (fun x -> x < 5) [1;2;3;4;5;6;7;8]
Option.filter (fun x -> x < 5) (Some 4)

let c a t = match t with
            |None -> None
            |Some x -> if a x then Some x else None

c (fun x -> x > 41) (Some 420)


// let catOptions x =
//     let rec a b c =
//         match b with
//         |(Some f)::g -> a g (List.append c [f])
//         |None::g -> a g c
//         |[] -> c
//     a x []


catOptions [Some 1; None; None; Some 2; Some 4] = [1;2;4]
catOptions [Some true; None; Some false] = [true; false]


let rec catOptions b =
        match b with
        |(Some f)::g -> f::catOptions g
        |None::g -> catOptions g
        |[] -> []

catOptions [Some 3;None;Some 10;Some 42]