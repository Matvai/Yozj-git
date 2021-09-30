// :o :P >:D *u* '_' '-' ;) >:( :() =) :): ~(:{) 8-)

let i j =
    j * j

let rec map g h =
    match h with
    |a::b -> g a::map g b
    |[] -> []

map (fun j -> j * j) [42; 84; 126; 1764; 42*4]

let rec filter h i =
    match i with
    |a::b -> match h a with
                |true -> a::filter h b
                |false -> filter h b
    |[] -> []

filter (fun i -> i > 5) [1;2;3;4;5;6;7;8;9;6;3;2;1] = [6;7;8;9;6]

let rec take n l =
    match n with
    |0 -> []
    |_ -> match l with
            |a::b -> a::take (n - 1) b
            |[] -> []

let rec length l =
    match l with
    |a::b -> 1 + length b
    |[] -> 0

take 4 [1;2;4;7;11;16;22;29;37;46;56;67]

let rec drop n l =
    match n, l with
    |0, _ -> l
    |_, [] -> []
    |_, _::b -> drop (n - 1) b

drop 3 [1;2;3;4;5;6;7;8]

let trytake n l =
    let x = take n l
    if length x < n then None else Some x

trytake 42 [1;2;3;4;5;6;7;8;9;10;11;12;13;14]

/// This function returns the first element of the list
let head l =
    match l with
    |a::b -> Some a
    |[] -> None

head [1;2;3]

head (drop 3 [1;2;3])

head ["a";"b"]

let rec onlyPresents l =
    match l with
    |Some x :: b -> x::onlyPresents b
    |None :: b -> onlyPresents b
    |[] -> []

onlyPresents [Some 1; None; Some 5; Some 42; None] 

let rec index n l =
    head (drop n l)

index 3 [1;2;3;42;5;6;7] 

let add5 n =
    match n with
    |Some x -> Some (x + 5)
    |None -> None

add5 (Some 2) = Some 7
add5 None = None
add5 (Some 37) = Some 42

let mult2 n =
    match n with
    |Some x -> Some (x * 2)
    |None -> None

mult2 (Some 2) 
mult2 None
mult2 (Some 21)

let optionMap f n =
    match n with
    |Some x -> Some (f x)
    |None -> None

optionMap (fun x -> x * 2) (Some 42)

optionMap add5 (Some (Some 42))

let optionToList o =
    match o with
    |Some x -> [x]
    |None -> []


let listToOption l =
    match l with
    |[a] -> Some a
    |_ -> None

listToOption [42;21]

optionToList (Some 42)