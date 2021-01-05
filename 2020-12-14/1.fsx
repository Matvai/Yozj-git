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

let rec prunt picture =
    match picture with
    |Blank -> [""]
    |Char a -> [string a]
    |Beside (a, b) -> equalizer (List.map2safe (fun x y -> x + y) (prunt a) (prunt b))
    |Vertical (a, b) -> equalizer (List.append (prunt a) (prunt b))
    |FlipVertical a -> List.rev (prunt a)
    |FlipHorizontal a -> List.map String.rev (prunt a)
    |Rotate a -> 
        let resultLength = String.length (List.head (prunt a))
        List.map (fun x -> listToString (List.map (fun (y: string) -> y.[x-1]) (prunt a))) [1..resultLength]

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


let pic =
    Vertical (
        stringPicture "Matvey",
        stringPicture "Hedgehog"
    )
    |> Rotate
    |> Rotate
   
print pic