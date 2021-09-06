type Picture =
    |Rotate of Picture
    |FlipVertical of Picture
    |FlipHorizontal of Picture
    |Vertical of Picture * Picture 
    |Beside of Picture * Picture 
    |Char of char 
    |Blank

module List =
    let rec map2safe x y z =
        match y, z with
        |[], _ -> z
        |_, [] -> y
        |a::b, c::d -> (x a c)::map2safe x b d

let equalizer list =
    let max = List.max (List.map (fun x -> String.length x) list) 
    List.map (fun x -> x + (String.replicate (max - String.length x) " ")) list

let stringToList (s: string) = 
    List.ofArray (s.ToCharArray())

let listToString list =
    new string(List.toArray list)

module String =
    let rev x =
        listToString (List.rev (stringToList x))

let equaloiser list1 list2 =
    if List.length list1 > List.length list2 
        then List.append list2 (List.replicate (List.length list1 - List.length list2) " ") 
        else List.append list1 (List.replicate (List.length list2 - List.length list1) " ")

let rec prunt picture =
    match picture with
    |Blank -> [""]
    |Char a -> [string a]
    |Beside (a, b) -> 
        let pa' = prunt a
        let pb' = prunt b
        let pa = if List.length pa' > List.length pb' then pa' else equaloiser pa' pb'
        let pb = if List.length pb' > List.length pa' then pb' else equaloiser pb' pa'
        equalizer (List.map2safe (fun x y -> x + y) pa pb)
    |Vertical (a, b) -> equalizer (List.append (prunt a) (prunt b))
    |FlipVertical a -> List.rev (prunt a)
    |FlipHorizontal a -> List.map String.rev (prunt a)
    |Rotate a -> 
        let resultLength = String.length (List.head (prunt a))
        List.rev (List.map (fun x -> listToString (List.map (fun (y: string) -> y.[x-1]) (prunt a))) [1..resultLength])

let rec print picture = 
    let list = prunt picture
    List.iter (fun x -> printfn "%s" x) list

let stringPicture x =
    let rec stringPicture2 picturelist =
        match picturelist with
        |[] -> Blank
        |a::b -> 
            Beside (Char a, stringPicture2 b)
    stringPicture2 (stringToList x)

let square = 
    Vertical(
        stringPicture "---",
        Vertical(stringPicture "xxx", stringPicture "---")
    )

let pic = Beside(square, Beside(Rotate square, Rotate (Rotate square)))
let pic2 = Beside(Rotate square, Beside(square, Rotate square))

let p2 = Vertical(pic, pic2)
let p3 = Beside (p2, Rotate p2)

print p3

let a = Rotate (Vertical(stringPicture "987", stringPicture "987"))
let b = Rotate (stringPicture "12345")
let c = Beside (a, b)
print a
print b
print c

let a = ["7";"8";"9";"e";"y";"u"]
let b = ["5";"4";"3";"2";"1"]
c = ["75";"84";"93";" 2";" 1"]

c = ["75";"84";"93";"2";"1"]



f a b 
