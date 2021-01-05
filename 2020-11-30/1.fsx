type Picture = 
    |Vertical of Picture * Picture 
    |Beside of Picture * Picture 
    |Char of char 
    |Blank

let rec prunt picture =
    match picture with
    |Blank -> [""]
    |Char a -> [string a]
    |Beside (a, b) -> List.map2 (fun x y -> x + y) (prunt a) (prunt b)
    |Vertical (a, b) -> List.append (prunt a) (prunt b)

let rec print picture = 
    let list = prunt picture
    List.iter (fun x -> printfn "%s" x) list

let stringToList (s: string) = 
    List.ofArray (s.ToCharArray())

let stringPicture x =
    let rec stringPicture2 picturelist =
        match picturelist with
        |[] -> Blank
        |a::b -> 
            Beside (Char a, stringPicture2 b)
    stringPicture2 (stringToList x)

let mnumbertwo = stringPicture "Murch"


stringToList "foobar"

print m

stringPicture "Murch is a crocodile" |> print

let f x y = x + y
let z = (f 5) 6
let g = f 5
let w = g 7