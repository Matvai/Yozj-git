let s = 1 :: 2 :: 42 :: []

let rec l i k =
    match k with
    | hd :: tl -> (i hd) :: (l i tl)
    | [] -> []

let x2 q = q*2

let t5 r = r+5

let V3 v = v/3

l V3 s

l (fun g -> g * 3) s

List.map V3 s
