let rec max l =
    match l with
    |a::b::c -> if a < b then max (b::c) else max (a::c)
    |[a] -> Some a
    |[] -> None

let rec min l =
    match l with
    |a::b::c -> if a > b then min (b::c) else min (a::c)
    |[a] -> Some a
    |[] -> None

let rec remove e l =
    match l with
    |a::b -> if a = e then remove e b else a::remove e b 
    |[] -> []

let rec sort l =
    match min l with
    |Some x -> x::sort (remove x l)
    |None -> []

let rec minBy f l =
    match l with
    |a::b::c -> if f a > f b then minBy f (b::c) else minBy f (a::c)
    |[a] -> Some a
    |[] -> None

let rec sortBy f l =
    match minBy f l with
    |Some x -> x::sortBy f (remove x l)
    |None -> []


max (["мурч крокодил";"Ёж крокодил"] : string list)

min (["мурч крокодил";"ёж крокодил"] : string list)

sort [3.1415926;21.0;42.0;5.25;10.5;84.0;168.0;2.7]

String.length "4242424242424242"

sortBy String.length ["a"; "b"; "abc"; "ab"; "x"] 

remove 36 [42;42;36;42;36;36;42]

minBy f [42;42;42;50]