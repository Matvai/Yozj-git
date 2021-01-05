type Picture = 
    |Vertical of Picture * Picture 
    |Beside of Picture * Picture 
    |Char of char 
    |Blank

let rec prunt picture =
    match picture with
    |Blank -> [" "]
    |Char a -> [string a]
    |Beside (a, b) -> List.map2 (fun x y -> x + y) (prunt a) (prunt b)
    |Vertical (a, b) -> List.append (prunt a) (prunt b)

let rec preent list = 
    match list with
    |[] -> printf ""
    |a::b -> 
        printfn "%s" a
        preent b

let rec print picture = 
    let list = prunt picture
    List.iter (fun x -> printfn "%s" x) list

let x = Char 'x'
let o = Char 'o'
let xo = Beside (x, o)
let ox = Beside (o, x)
let xo'ox = Vertical (xo, ox)
let bv = Beside (xo'ox, xo'ox)

print bv

preent ["a";"b";"c"]

List.iter (fun x -> printfn "%A" x ; printfn "%A" x) [42;420]