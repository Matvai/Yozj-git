// :o :P >:D *u* '_' '-' ;) >:( :() =) :): ~(:{) 8-)

let i j =
    j/42

let rec f g h =
    match h with
    |a::b -> g a::f g b
    |[] -> []

f (fun j -> j/42) [42; 84; 126; 1764; 42*4]
