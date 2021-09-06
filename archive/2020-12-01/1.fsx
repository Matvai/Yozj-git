type Picture = 
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

let rec prunt picture =
    match picture with
    |Blank -> [""]
    |Char a -> [string a]
    |Beside (a, b) -> equalizer (List.map2safe (fun x y -> x + y) (prunt a) (prunt b))
    |Vertical (a, b) -> equalizer (List.append (prunt a) (prunt b))

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

let razdvatri = stringPicture "123"

let chetiripyatshest = stringPicture "456"

let tsifra = Vertical (razdvatri, chetiripyatshest)

prunt razdvatri
prunt chetiripyatshest
prunt tsifra
prunt (Beside (tsifra, tsifra))

let tsifra2 = Vertical (tsifra, razdvatri)
print tsifra2
let imya = (Beside (tsifra, tsifra2)) 
let doubleImya = Beside (imya, imya)
print imya
print doubleImya
