List.forall (fun x -> x > 2) [3;4;5;6]

List.forall (fun s -> s) [false; false]

List.forall (fun s -> not s) [false; false]



// let rec c a t = 
//     match t with
//     |[] -> true
//     |x::y -> a x && c a y


let h i j =
    let rec c a t s = 
        match t with
        |[] -> s
        |x::y -> if a x then c a y (s && a x) else false
    c i j true

h (fun x -> x = 21 + 21) [42;42;42]

