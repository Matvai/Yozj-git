List.exists (fun x -> x > 3) [1;2;3;4]
List.exists (fun x -> x > 3) [4;7;7;7]

let h i j =
    let rec c a t = 
        match t with
        |[] -> false
        |x::y -> if a x then true else c a y
    c i j

h (fun x -> x = 42) [0024;0042;204;0240;402;0420;2004;2400;4002;4200]

let f x =
    match x with
    |[] -> None
    |a::b -> Some a

f ["Мурч крокодил";"Мурч не крокодил"]

let rec g y =
    match y with
    |[] -> None
    |a::[] -> Some a
    |a::b -> g b

g [50;49;48;47;46;45;44;43;42]

List.tryPick (fun x -> if x < 42 then Some (x*10) else None) [42;43;39;40]


let rec d o g =
    match g with
    |[] -> None
    |a::b -> match o a with
                |Some z -> o a
                |None -> d o b

d (fun x -> if x < 43 then Some (x*10) else None) [47;46;-42;38;42]