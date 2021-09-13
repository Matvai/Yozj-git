let f spisok =
    let rec g spisok a =
        match spisok with
        |h::t -> g t (h + a)
        |[] -> a
    let b = g spisok 0
    b

let rec f' lst = 
    match lst with
    | [] -> 0
    | h::t -> h + f t

let fu spisok =
    let rec g spisok a =
        match spisok with
        |h::t -> g t (h * a)
        |[] -> a
    g spisok 1

let rec g spisk =
    match spisk with
    |[] -> []
    |h::t -> h + 5::g t
    
let rec i spsok =
    match spsok with
    |[] -> []
    |h::t -> h * 2::i t

i [21; 210; 42] 