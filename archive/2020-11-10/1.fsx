type Picture = Printfn of char | Vertical of Picture * Picture | Beside of Picture * Picture | Char of char | Blank

let rec draw picture =
    match picture with
    |Blank -> printf " "
    |Char a -> printf "%c" a
    |Beside (a, b) ->
        draw a
        draw b
    |Vertical (a, b) ->
        draw a
        printfn ""
        draw b

let xo = Beside (Char 'x', Char 'o')

let ox = Beside (Char 'o', Char 'x')

let square = Vertical (xo, ox)

draw (Beside (square, square))

let rec print picture =
    match picture with
    |Blank -> " "
    |Char a -> string a
    |Beside (a, b) -> (print a) + (print b)
    |Vertical (a, b) -> ""

print (Char 'x')

print (Beside (xo, xo))

let rec prunt picture =
    match picture with
    |Blank -> [" "]
    |Char a -> [string a]
    |Beside (a, b) -> List.map2 (fun x y -> x + y) (prunt a) (prunt b)
    |Vertical (a, b) -> List.append (prunt a) (prunt b)

prunt (Char 'e')
let xo = (Vertical (Char 'x', Char 'o'))

let ox = (Vertical (Char 'o', Char 'x'))


let f x y = x + y

List.map2 f ["a";"c"] ["b";"d"]

prunt (Beside (xo, ox))