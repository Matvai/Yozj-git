type Picture =
    |Rotate of Picture
    |FlipVertical of Picture
    |FlipHorizontal of Picture
    |Vertical of Picture * Picture 
    |Beside of Picture * Picture 
    |Char of char 
    |Blank
    |Overlay of Picture * Picture

module List =
    let rec map2safe x y z =
        match y, z with
        |[], _ -> z
        |_, [] -> y
        |a::b, c::d -> (x a c)::map2safe x b d

let equalizer list =
    let max = List.max (List.map String.length list) 
    List.map (fun x -> x + (String.replicate (max - String.length x) " ")) list

let stringToList (s: string) = 
    List.ofArray (s.ToCharArray())

let listToString list =
    new string(List.toArray list)

module String =
    let rev x =
        listToString (List.rev (stringToList x))

let equaloiser list1 list2 =
    let len1 = List.length list1
    let len2 = List.length list2
    if len1 > len2  
        then
            let width = String.length (List.head list2)
            let extraLines = List.replicate (len1 - len2) (String.replicate width " ")
            List.append list2 extraLines
        else 
            let width = String.length (List.head list1)
            let extraLines = List.replicate (len2 - len1) (String.replicate width " ")
            List.append list1 extraLines

let stringOverlay s1 s2 =
    let [x; y] = equalizer [s1; s2]
    listToString (List.map2 (fun a b -> if a = ' ' then b else a) (stringToList x) (stringToList y))

stringOverlay "murch is a murch    " "murch is a crocodile"

equalizer ["abc"; "xyz123"]

let [x; y] = equalizer ["abc"; "xyz123"]

let rec prunt picture =
    match picture with
    |Blank -> [""]
    |Char a -> [string a]
    |Beside (a, b) -> 
        let pa' = prunt a
        let pb' = prunt b
        let pa = if List.length pa' > List.length pb' then pa' else equaloiser pa' pb'
        let pb = if List.length pb' > List.length pa' then pb' else equaloiser pb' pa'
        equalizer (List.map2safe (+) pa pb)
    |Vertical (a, b) -> equalizer (List.append (prunt a) (prunt b))
    |FlipVertical a -> List.rev (prunt a)
    |FlipHorizontal a -> List.map String.rev (prunt a)
    |Rotate a -> 
        let resultLength = String.length (List.head (prunt a))
        List.rev (List.map (fun x -> listToString (List.map (fun (y: string) -> y.[x-1]) (prunt a))) [1..resultLength])
    |Overlay (a, b) -> List.map2 stringOverlay (prunt a) (prunt b)

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

let mic = (Vertical (stringPicture "murch", (Vertical (stringPicture "is__", stringPicture "a      dile"))))
let minc = (Vertical (stringPicture "murch", (Vertical (stringPicture "isnt", stringPicture "a croc"))))
let zz = Vertical (Blank, Vertical (Blank, stringPicture "      O"))

print (Overlay (Overlay (mic, minc), zz))

let printThis x = printfn "%d" x

let printThis_x2 x = 
    printThis x
    printThis x

printThis_x2 42